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

      superstate.previousTurns.Add(context.root.Snapshot());
      var anchorTurnIndex = superstate.previousTurns.Count - 1;
      game.lastPlayerRequest = request.AsIRequest();

      var oldLocation = game.player.location;

      var moveExecutor = MoveRequestExecutor.PrepareToMove(superstate, game, destination);

      if (moveExecutor == null) {
        return "Could not move there!";
      }

      // Add the PREVIOUS turn to the anchorTurnIndices.
      // (Remember, the current turn hasn't yet been added to the turnsIncludingPresent list)
      superstate.anchorTurnIndices.Add(anchorTurnIndex);

      moveExecutor.Execute();

      var terrainTileAtOldLocation = game.level.terrain.tiles[oldLocation];
      terrainTileAtOldLocation.components.Add(
          context.root.EffectTimeAnchorTTCCreate(context.root.version)
              .AsITerrainTileComponent());

      return "";
    }
  }
}
