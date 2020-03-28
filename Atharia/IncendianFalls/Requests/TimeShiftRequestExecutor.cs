using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class TimeShiftRequestExecutor {

    // Future is the futuremost turn, where the player cast timeshift.
    // Anchor is the most recent turn they cast time anchor, aka destination.
    // When shifting backwards by one incarnation, we're going from the "older"
    // turn to the "newer" turn.

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

    private static void TransitionToCloneMoving(Superstate superstate, int targetAnchorTurnIndex, Game game) {
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

      game.player.components.Add(
          game.root.EffectTimeCloneAICapabilityUCCreate(
              game.root.EffectIRequestMutListCreate(script))
          .AsIUnitComponent());
      game.player = Unit.Null;

      Asserts.Assert(superstate.game.actingUnit.Exists());
    }

    public static string Execute(
        SSContext context,
        Superstate superstate,
        TimeShiftRequest request) {
      int gameId = request.gameId;
      var game = context.root.GetGame(gameId);
      Asserts.Assert(game.WaitingOnPlayerInput());


      if (superstate.anchorTurnIndices.Count == 0) {
        return "Can't time shift, no anchor to time shift back to!";
      }

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

      // Now that we're sure we can do it, do it!

      // A root with the incarnation that is the future of this timeline. We maintain this
      // so that when we finish timeshifting backwards, we can read the future player and
      // bring them into the past/present.
      var future = new Root(context.logger, context.root.Snapshot());
      
      // If future non-null, this is the index of the turn we're aiming at.
      // If the last turn in turnsIncludingPresent is this index, we're done timeshifting.
      int targetAnchorTurnIndex = mostRecentAnchorTurnIndex;

      Location targetAnchorLocation = pastPlayerLocation;

      // Because how would we revert to this turn?
      Asserts.Assert(superstate.previousTurns.Count != targetAnchorTurnIndex);

      while (superstate.previousTurns.Count != targetAnchorTurnIndex) {
        RewindOneTurn(superstate, game);

        // We broadcast this so the UI knows to pause for a little bit to let things settle down.
        game.AddEvent(new RevertedEvent().AsIGameEvent());
      }

      TransitionToCloneMoving(superstate, targetAnchorTurnIndex, game);



      // We're not between units yet; the time clone should do things until we're
      // between units.
      game.pauseBeforeNextUnit = true;
      GameLoop.ContinueBeforeUnitAction(context, game, superstate);






      var futureGame = future.GetGame(game.id);
      var futurePlayer = futureGame.player;

      // Now place a new player down, and switch game.player to it.
      var newPlayer = CreateNewPlayer(context, game, targetAnchorLocation, futurePlayer);

      game.level.units.Add(newPlayer);
      game.player = newPlayer;
      superstate.levelSuperstate.AddUnit(game.player);
      game.actingUnit = game.player;

      Asserts.Assert(
          superstate.anchorTurnIndices[superstate.anchorTurnIndices.Count - 1] ==
          targetAnchorTurnIndex);
      superstate.anchorTurnIndices.RemoveAt(superstate.anchorTurnIndices.Count - 1);

      return "";
    }

    static Unit CreateNewPlayer(
        SSContext context,
        Game game,
        Location targetAnchorLocation,
        Unit futurePlayer) {
      // Now place a new player down, and switch game.player to it.
      var newPlayer =
          context.root.EffectUnitCreate(
              NullIUnitEvent.Null,
              true,
              0,
              targetAnchorLocation,
              futurePlayer.classId,
              game.time, // Act now
              futurePlayer.hp, futurePlayer.maxHp,
              IUnitComponentMutBunch.New(context.root),
              true);
      foreach (var component in futurePlayer.components.GetAllICloneableUC()) {
        var newReal = component.ClonifyAndReturnNewReal(newPlayer.root);
        if (newReal.Exists()) {
          newPlayer.components.Add(newReal.AsIUnitComponent());
        }
      }

      return newPlayer;
    }
  }
}
