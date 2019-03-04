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
      return 5 + timeDifference / 200;
    }

    private static bool CheckCanTimeshift(Game presentGame, RootIncarnation destinationIncarnation) {
      var pastGame = destinationIncarnation.incarnationsGame[presentGame.id].incarnation;
      var pastTime = pastGame.time;
      int timeDifference = presentGame.time - pastTime;
      // time shift costs 5 now, and 5 + timeDifference / 100 from your past self.
      int mpCost = GetMpCost(presentGame.time - pastGame.time);

      if (presentGame.player.mp < 5) {
        presentGame.root.logger.Info("Not enough mp now to cast the spell, need 5!");
        return false;
      }

      var pastPlayerId = destinationIncarnation.incarnationsGame[presentGame.id].incarnation.player;
      if (destinationIncarnation.incarnationsUnit[pastPlayerId].incarnation.mp < mpCost) {
        presentGame.root.logger.Info("Not enough mp back then to cast the spell!");
        return false;
      }

      return true;
    }

    private static void RewindOneTurnAndAddToScript(Superstate superstate, Game game) {
      var playerRequestFromOlderToNewer = game.lastPlayerRequest;

      // First, get the old steps.
      var oldDirective = game.player.GetDirectiveOrNull();
      var previousScript = new List<IRequest>();
      if (oldDirective is TimeScriptDirectiveUCAsIDirectiveUC followScriptI) {
        var followScript = followScriptI.obj;
        previousScript.AddRange(followScript.script);
      }
      // Now, add the thing the player did to get to the "future" incarnation.
      var newScript = new List<IRequest>(previousScript);
      if (playerRequestFromOlderToNewer is TimeAnchorMoveRequestAsIRequest tamrI) {
        newScript.Insert(
            0,
            new MoveRequest(tamrI.obj.gameId, tamrI.obj.destination).AsIRequest());
      } else {
        newScript.Insert(0, playerRequestFromOlderToNewer);
      }

      var olderIncarnation =
          superstate.previousTurns[superstate.previousTurns.Count - 1];

      // At (count - 1) is the previous turn.
      superstate.previousTurns.RemoveAt(superstate.previousTurns.Count - 1);
      // Revert to the previous turn.
      game.root.Revert(olderIncarnation);
      // Reconstruct the views.
      superstate.liveUnitByLocationMap.Reconstruct(game);

      // Now, replace whatever their directive was with the new script.
      game.player.ReplaceDirective(
          game.player.root.EffectTimeScriptDirectiveUCCreate(
              game.player.root.EffectIRequestMutListCreate(
                  newScript))
          .AsIDirectiveUC());
    }

    public static bool Execute(
        SSContext context,
        Superstate superstate,
        TimeShiftRequest request) {
      int gameId = request.gameId;

      var game = context.root.GetGame(gameId);

      EventsClearer.Clear(game);

      switch (superstate.GetStateType()) {
        case MultiverseStateType.kBeforePlayerInput:
          if (superstate.anchorTurnIndices.Count == 0) {
            context.logger.Error("Can't time shift, nothing to time shift back to!");
            return false;
          }

          var playerRequestFromOlderToNewer = game.lastPlayerRequest;
          Asserts.Assert(playerRequestFromOlderToNewer != null);
          Asserts.Assert(superstate.timeShiftingState == null);

          int mostRecentAnchorTurnIndex =
              superstate.anchorTurnIndices[superstate.anchorTurnIndices.Count - 1];
          RootIncarnation pastIncarnation =
              superstate.previousTurns[mostRecentAnchorTurnIndex];
          var pastPlayerId = pastIncarnation.incarnationsGame[game.id].incarnation.player;
          var pastPlayerLocation = pastIncarnation.incarnationsUnit[pastPlayerId].incarnation.location;

          if (!CheckCanTimeshift(game, pastIncarnation)) {
            return false;
          }

          // Now that we're sure we can do it, do it!

          var future = new Root(context.logger, context.root.Snapshot());
          superstate.timeShiftingState =
              new Superstate.TimeShiftingState(
                  future,
                  mostRecentAnchorTurnIndex,
                  pastPlayerLocation,
                  true);

          RewindOneTurnAndAddToScript(superstate, game);

          if (superstate.previousTurns.Count == superstate.timeShiftingState.targetAnchorTurnIndex) {
            superstate.timeShiftingState.rewinding = false;
            Asserts.Assert(superstate.GetStateType() == MultiverseStateType.kTimeshiftingCloneMoving);
          } else {
            Asserts.Assert(superstate.GetStateType() == MultiverseStateType.kTimeshiftingBackward);
          }

          return true;

        case MultiverseStateType.kTimeshiftingBackward:
          Asserts.Assert(superstate.timeShiftingState != null);
          Asserts.Assert(superstate.timeShiftingState.rewinding);

          RewindOneTurnAndAddToScript(superstate, game);

          if (superstate.previousTurns.Count == superstate.timeShiftingState.targetAnchorTurnIndex) {
            superstate.timeShiftingState.rewinding = false;
            game.player.components.Add(
                game.root.EffectTimeCloneAICapabilityUCCreate()
                .AsIUnitComponent());
            game.player = Unit.Null;
            Asserts.Assert(superstate.GetStateType() == MultiverseStateType.kTimeshiftingCloneMoving);
          }

          return true;

        case MultiverseStateType.kTimeshiftingCloneMoving:
          // We're not between units yet; the time clone should do things until we're
          // between units.
          game.root.logger.Info("About to Continue! " + game.GetStateType());
          GameLoop.Continue(game, superstate, new PauseCondition(true));
          game.root.logger.Info("Just Continue'd! " + game.GetStateType());

          return true;

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
                  "chronomancer",
                  futurePlayer.hp, futurePlayer.maxHp,
                  futurePlayer.mp, futurePlayer.maxMp,
                  600,
                  game.time, // Act now
                  IUnitComponentMutBunch.New(context.root),
                  IItemMutBunch.New(context.root),
                  true);
          newPlayer.mp = newPlayer.mp - GetMpCost(futureGame.time - game.time);

          Console.WriteLine("Made new player! ID " + newPlayer.id);
          game.level.units.Add(newPlayer);
          game.player = newPlayer;
          superstate.liveUnitByLocationMap.Add(game.player);

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

          return true;

        default:
          Asserts.Assert(false);
          return false;
      }
    }
  }
}
