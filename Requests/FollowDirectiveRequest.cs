using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class FollowDirectiveRequestExecutor {
    public static bool Execute(SSContext context, int gameId) {
      var game = context.root.GetGame(gameId);
      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }
      Asserts.Assert(game.GetExecutionStateType() == GameExecutionStateType.kBeforePlayerAction);

      if (!game.player.GetDirectiveOrNull().Exists()) {
        context.logger.Error("Player has no directive, can't resume!");
        return false;
      }

      var liveUnitByLocationMap = PreRequest.Do(game);

      if (game.player.GetDirectiveOrNull() is MoveDirectiveUCAsIDirectiveUC move) {
        if (!PlayerAI.FollowMoveDirective(game, liveUnitByLocationMap, game.player)) {
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
