using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class TimeAnchorMoveRequestExecutor {
    public static string Execute(
        SSContext context,
        Superstate superstate,
        TimeAnchorMoveRequest request) {

      int gameId = request.gameId;
      Location destination = request.destination;
      var game = context.root.GetGame(gameId);

      EventsClearer.Clear(game);

      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        return "Error: Unexpected player input!";
      }
      if (!game.executionState.actingUnit.Is(game.player)) {
        return "Error: Player not next acting unit! (a)";
      }
      //if (!game.player.Is(Utils.GetNextActingUnit(game))) {
      //  return "Error: Player not next acting unit! (b)";
      //}
      if (superstate.timeShiftingState != null) {
        return "Error: Cannot anchor move while time shifting!";
      }

      if (destination == game.player.location) {
        return "Already there!";
      }

      if (!Actions.CanStep(game, superstate, game.player, destination)) {
        return "Can't step there!";
      }

      superstate.previousTurns.Add(context.root.Snapshot());
      superstate.requests.Add(request.AsIRequest());
      var anchorTurnIndex = superstate.previousTurns.Count - 1;

      var oldLocation = game.player.location;

      // Add the PREVIOUS turn to the anchorTurnIndices.
      // (Remember, the current turn hasn't yet been added to the turnsIncludingPresent list)
      superstate.anchorTurnIndices.Add(anchorTurnIndex);

      Actions.Step(game, superstate, game.player, destination, false, true);

      var terrainTileAtOldLocation = game.level.terrain.tiles[oldLocation];
      terrainTileAtOldLocation.components.Add(
          context.root.EffectTimeAnchorTTCCreate(context.root.version)
              .AsITerrainTileComponent());

      GameLoop.NoteUnitActed(game, game.player);

      //GameLoop.ContinueAfterUnitAction(
      //    game,
      //    superstate,
      //    new PauseCondition(false),
      //    new SortedSet<int>());

      return "";
    }
  }
}
