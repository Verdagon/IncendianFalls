using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class TimeShiftRequestExecutor {

    // Future is the futuremost turn, where the player cast timeshift.
    // Anchor is the most recent turn they cast time anchor, aka destination.
    // When shifting backwards by one incarnation, we're going from the "older"
    // turn to the "newer" turn.

    private static int GetMpCost(int timeDifference) {
      return 2 + timeDifference / 2400;
    }

    private static string CheckCanTimeshift(Game presentGame, RootIncarnation destinationIncarnation) {
      var pastGame = destinationIncarnation.incarnationsGame[presentGame.id].incarnation;
      var pastTime = pastGame.time;
      int timeDifference = presentGame.time - pastTime;
      // time shift costs 5 now, and 5 + timeDifference / 100 from your past self.
      int mpCost = GetMpCost(presentGame.time - pastGame.time);

      if (presentGame.player.mp < 2) {
        return "Not enough mp now to cast the spell, need at least 2!";
      }

      var pastPlayerId = destinationIncarnation.incarnationsGame[presentGame.id].incarnation.player;
      if (destinationIncarnation.incarnationsUnit[pastPlayerId].incarnation.mp < mpCost) {
        return "Not enough mp in past to cast the spell, need " + mpCost;
      }

      return "";
    }

    private static void RewindOneTurn(Superstate superstate, Game game) {
      var olderIncarnation =
          superstate.previousTurns[superstate.previousTurns.Count - 1];

      // At (count - 1) is the previous turn.
      superstate.previousTurns.RemoveAt(superstate.previousTurns.Count - 1);
      // Revert to the previous turn.
      game.root.Revert(olderIncarnation);
      // Reconstruct the views.
      superstate.levelSuperstate.Reconstruct(game.level);

      // Not removing from superstate.requests, we'll do that when timeshifting is done.
    }

    private static void TransitionToCloneMoving(Superstate superstate, Game game) {
      var targetAnchorTurnIndex = superstate.timeShiftingState.targetAnchorTurnIndex;
      Asserts.Assert(superstate.previousTurns.Count == targetAnchorTurnIndex);

      var script = new List<IRequest>();
      for (int i = targetAnchorTurnIndex; i < superstate.requests.Count; i++) {
        if (superstate.requests[i] is TimeAnchorMoveRequestAsIRequest tamrI) {
          script.Add(new MoveRequest(tamrI.obj.gameId, tamrI.obj.destination).AsIRequest());
        } else {
          script.Add(superstate.requests[i]);
        }
      }
      superstate.requests.RemoveRange(
          targetAnchorTurnIndex,
          superstate.requests.Count - targetAnchorTurnIndex);

      superstate.timeShiftingState.rewinding = false;
      game.player.components.Add(
          game.root.EffectTimeCloneAICapabilityUCCreate(
              game.root.EffectIRequestMutListCreate(script))
          .AsIUnitComponent());
      game.player = Unit.Null;
      Asserts.Assert(superstate.GetStateType() == MultiverseStateType.kTimeshiftingCloneMoving);
    }

    public static string Execute(
        SSContext context,
        Superstate superstate,
        TimeShiftRequest request) {
      int gameId = request.gameId;

      var game = context.root.GetGame(gameId);

      EventsClearer.Clear(game);

      switch (superstate.GetStateType()) {
        case MultiverseStateType.kBeforePlayerInput:
          if (superstate.anchorTurnIndices.Count == 0) {
            return "Can't time shift, no anchor to time shift back to!";
          }

          Asserts.Assert(superstate.timeShiftingState == null);

          int mostRecentAnchorTurnIndex =
              superstate.anchorTurnIndices[superstate.anchorTurnIndices.Count - 1];

          for (int turnIndex = mostRecentAnchorTurnIndex; turnIndex < superstate.previousTurns.Count; turnIndex++) {
            var previousLevelId = superstate.previousTurns[turnIndex].incarnationsGame[game.id].incarnation.level;
            if (game.level.id != previousLevelId) {
              return "Can't time shift to a different level!";
            }
          }

          RootIncarnation pastIncarnation =
              superstate.previousTurns[mostRecentAnchorTurnIndex];
          var pastPlayerId = pastIncarnation.incarnationsGame[game.id].incarnation.player;
          var pastPlayerLocation = pastIncarnation.incarnationsUnit[pastPlayerId].incarnation.location;

          string cantTimeshiftReason = CheckCanTimeshift(game, pastIncarnation);
          if (cantTimeshiftReason.Length > 0) {
            return cantTimeshiftReason;
          }

          // Now that we're sure we can do it, do it!

          var future = new Root(context.logger, context.root.Snapshot());
          superstate.timeShiftingState =
              new Superstate.TimeShiftingState(
                  future,
                  mostRecentAnchorTurnIndex,
                  pastPlayerLocation,
                  true);

          RewindOneTurn(superstate, game);

          if (superstate.previousTurns.Count == superstate.timeShiftingState.targetAnchorTurnIndex) {
            TransitionToCloneMoving(superstate, game);
          } else {
            Asserts.Assert(superstate.GetStateType() == MultiverseStateType.kTimeshiftingBackward);
          }

          return "";

        case MultiverseStateType.kTimeshiftingBackward:
          Asserts.Assert(superstate.timeShiftingState != null);
          Asserts.Assert(superstate.timeShiftingState.rewinding);

          RewindOneTurn(superstate, game);

          if (superstate.previousTurns.Count == superstate.timeShiftingState.targetAnchorTurnIndex) {
            TransitionToCloneMoving(superstate, game);
          }

          return "";

        case MultiverseStateType.kTimeshiftingCloneMoving:
          // We're not between units yet; the time clone should do things until we're
          // between units.
          GameLoop.Continue(game, superstate, new PauseCondition(true));

          return "";

        case MultiverseStateType.kTimeshiftingAfterCloneMoved:
          var futureGame = superstate.timeShiftingState.future.GetGame(game.id);
          var futurePlayer = futureGame.player;

          // Now place a new player down, and switch game.player to it.
          var newPlayer =
              context.root.EffectUnitCreate(
                  context.root.EffectIUnitEventMutListCreate(),
                  true,
                  0,
                  superstate.timeShiftingState.targetAnchorLocation,
                  "Chronomancer",
                  futurePlayer.hp, futurePlayer.maxHp,
                  futurePlayer.mp, futurePlayer.maxMp,
                  600,
                  game.time, // Act now
                  IUnitComponentMutBunch.New(context.root),
                  true,
                  5);
          newPlayer.mp = newPlayer.mp - GetMpCost(futureGame.time - game.time);

          foreach (var item in futurePlayer.components.GetAllIItem()) {
            var newReal = item.ClonifyAndReturnNewReal(newPlayer.root);
            if (newReal.Exists()) {
              newPlayer.components.Add(newReal.AsIUnitComponent());
            }
          }

          game.level.units.Add(newPlayer);
          game.player = newPlayer;
          superstate.levelSuperstate.Add(game.player);

          Asserts.Assert(
              superstate.anchorTurnIndices[superstate.anchorTurnIndices.Count - 1] ==
              superstate.timeShiftingState.targetAnchorTurnIndex);
          superstate.anchorTurnIndices.RemoveAt(superstate.anchorTurnIndices.Count - 1);

          superstate.timeShiftingState = null;

          // The game loop is between units. Let's simulate until it's waiting for player input.
          // There's some complexity here... see, before we timeshifted backward, all the components
          // did their pre turn actions. So to do them again might be weird.
          // However, to not do them would probably confuse their internal states.
          // Then again, confused internal states is a much more general problem that we
          // should solve elsewhere.
          // For now, fast-forward the state to waiting for user input.
          GameLoop.NoteUnitAppearedReadyToAct(game, superstate);

          Asserts.Assert(superstate.GetStateType() == MultiverseStateType.kBeforePlayerInput);

          return "";

        default:
          Asserts.Assert(false);
          return "";
      }
    }
  }
}
