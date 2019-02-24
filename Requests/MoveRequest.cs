using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class MoveRequestExecutor {
    public static bool Execute(SSContext context, int gameId, Location destination) {
      var game = context.root.GetGame(gameId);
      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }

      var liveUnitByLocationMap = PreRequest.Do(game);

      var player = game.player;

      if (!game.executionState.actingUnit.NullableIs(game.player)) {
        throw new Exception("Player isn't the state's acting unit!");
      }
      if (!game.player.NullableIs(Utils.GetNextActingUnit(game))) {
        throw new Exception("Player isn't the next acting unit!");
      }

      if (player.GetDirectiveOrNull().Exists()) {
        player.GetDirectiveOrNull().Destruct();
      }

      if (destination == player.location) {
        context.logger.Error("Already there!");
        return false;
      }

      var steps = AStarExplorer.Go(game.level.terrain, game.player.location, destination, game.level.considerCornersAdjacent);
      if (steps.Count == 0) {
        context.logger.Error("No path!");
        return false;
      }

      var path = context.root.EffectLocationMutListCreate(steps);
      var directive = context.root.EffectMoveDirectiveUCCreate(path);
      player.ReplaceDirective(directive.AsIDirectiveUC());

      if (!PlayerAI.FollowMoveDirective(game, liveUnitByLocationMap, game.player)) {
        return false;
      }

      GameLoop.NoteUnitActed(game, game.player);

      return true;
    }
  }
}
