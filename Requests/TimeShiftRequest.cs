using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class TimeShiftRequestExecutor {
    public static bool Execute(
        SSContext context,
        int gameId,
        RootIncarnation pastIncarnation,
        int futuremostTime) {
      var game = context.root.GetGame(gameId);

      // time shift costs 5 now, and 5 + timeDifference / 100 from your past self.

      var pastGame = pastIncarnation.incarnationsGame[gameId].incarnation;
      var pastTime = pastGame.time;
      int timeDifference = futuremostTime - pastTime;
      int mpCost = 5 + timeDifference / 100;
      game.root.logger.Info("time difference is " + timeDifference + " so mp cost " + mpCost);
      if (game.player.mp < 5) {
        game.root.logger.Info("Not enough mp now to cast the spell, need 5!");
        return false;
      }
      if (pastIncarnation.incarnationsUnit[game.player.id].incarnation.mp < mpCost) {
        game.root.logger.Info("Not enough mp back then to cast the spell!");
        return false;
      }

      PreRequest.Do(game);

      float futureTime = game.time;
      context.root.Revert(pastIncarnation);

      // We don't want to follow the player's directive from back then.
      if (game.player.GetDirectiveOrNull().Exists()) {
        game.player.GetDirectiveOrNull().Destruct();
      }

      var player = game.player;
      player.mp = player.mp - mpCost;

      //float pastTime = game.time;

      // later, drain hp proportional to this

      return true;
    }
  }
}
