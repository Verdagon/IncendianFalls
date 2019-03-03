using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class MoveRequestExecutor {
    public static bool Execute(
        SSContext context,
        Superstate superstate,
        int gameId,
        Location destination) {
      var game = context.root.GetGame(gameId);
      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }

      PreRequest.Do(game);

      superstate.turnsIncludingPresent.Insert(0, context.root.Snapshot());
      superstate.futuremostTime = Math.Max(superstate.futuremostTime, game.time);

      var player = game.player;

      if (!game.executionState.actingUnit.NullableIs(game.player)) {
        throw new Exception("Player isn't the state's acting unit!");
      }
      if (!game.player.NullableIs(Utils.GetNextActingUnit(game))) {
        throw new Exception("Player isn't the next acting unit!");
      }

      player.ClearDirective();

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
      Console.WriteLine("Made directive! " + directive.id);

      if (!PlayerAI.FollowMoveDirective(game, superstate, game.player)) {
        return false;
      }

      GameLoop.NoteUnitActed(game, game.player);

      return true;
    }
  }
}
