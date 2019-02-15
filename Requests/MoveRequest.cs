using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class MoveRequestExecutor {
    ILogger logger;
    public MoveRequestExecutor(ILogger logger) {
      this.logger = logger;
    }
    public bool Execute(Root root, int gameId, Location destination) {
      var game = root.GetGame(gameId);
      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }

      var liveUnitByLocationMap = PreRequest.Do(game);

      var player = game.player;

      if (!game.executionState.actingUnit.NullableIs(game.player)) {
        throw new Exception("Player isn't the state's acting unit!");
      }
      if (!game.player.NullableIs(Utils.GetNextActingUnit(root, game))) {
        throw new Exception("Player isn't the next acting unit!");
      }

      if (player.directive.Exists()) {
        player.directive.Delete();
      }

      if (destination == player.location) {
        logger.Error("Already there!");
        return false;
      }

      var steps = AStarExplorer.Go(game.level.terrain, game.player.location, destination, game.level.considerCornersAdjacent);
      if (steps.Count == 0) {
        logger.Error("No path!");
        return false;
      }

      var path = root.EffectLocationMutListCreate(steps);
      var directive = root.EffectMoveDirectiveCreate(path);
      player.directive = new MoveDirectiveAsIDirective(directive);

      if (!PlayerAI.FollowMoveDirective(game, liveUnitByLocationMap, game.player)) {
        return false;
      }

      GameLoop.NoteUnitActed(game, game.player);

      return true;
    }
  }
}
