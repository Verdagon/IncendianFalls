using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class FollowDirectiveRequestExecutor {
    public static bool Execute(
        SSContext context,
        Superstate superstate, 
        FollowDirectiveRequest request) {
      int gameId = request.gameId;
      var game = context.root.GetGame(gameId);

      EventsClearer.Clear(game);

      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }
      Asserts.Assert(game.GetExecutionStateType() == GameExecutionStateType.kBeforePlayerAction);

      if (!game.player.GetDirectiveOrNull().Exists()) {
        context.logger.Error("Player has no directive, can't resume!");
        return false;
      }

      superstate.turnsIncludingPresent.Add(context.root.Snapshot());
      superstate.futuremostTime = Math.Max(superstate.futuremostTime, game.time);

      game.lastPlayerRequest = request.AsIRequest();

      if (game.player.GetDirectiveOrNull() is MoveDirectiveUCAsIDirectiveUC move) {
        if (!PlayerAI.FollowMoveDirective(game, superstate, game.player)) {
          return false;
        }
      } else {
        Asserts.Assert(false);
        return false;
      }

      GameLoop.NoteUnitActed(game, game.player);

      return true;
    }
  }
}
