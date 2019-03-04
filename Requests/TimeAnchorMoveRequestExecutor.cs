using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class TimeAnchorMoveRequestExecutor {
    public static bool Execute(
        SSContext context,
        Superstate superstate,
        TimeAnchorMoveRequest request) {

      int gameId = request.gameId;
      Location destination = request.destination;
      var game = context.root.GetGame(gameId);

      EventsClearer.Clear(game);

      Asserts.Assert(superstate.GetStateType() == MultiverseStateType.kBeforePlayerInput);

      superstate.previousTurns.Add(context.root.Snapshot());
      var anchorTurnIndex = superstate.previousTurns.Count - 1;
      game.lastPlayerRequest = request.AsIRequest();

      var oldLocation = game.player.location;

      var moveExecutor = MoveRequestExecutor.PrepareToMove(superstate, game, destination);

      if (moveExecutor == null) {
        return false;
      }

      // Add the PREVIOUS turn to the anchorTurnIndices.
      // (Remember, the current turn hasn't yet been added to the turnsIncludingPresent list)
      superstate.anchorTurnIndices.Add(anchorTurnIndex);

      moveExecutor.Execute();

      var terrainTileAtOldLocation = game.level.terrain.tiles[oldLocation];
      terrainTileAtOldLocation.components.Add(
          context.root.EffectTimeAnchorTTCCreate(context.root.version)
              .AsITerrainTileComponent());

      return true;
    }
  }
}
