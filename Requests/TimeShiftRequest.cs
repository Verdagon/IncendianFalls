using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class TimeShiftRequestExecutor {
    public static bool Execute(
        SSContext context,
        Superstate superstate,
        TimeShiftRequest request) {
      int gameId = request.gameId;

      var game = context.root.GetGame(gameId);

      EventsClearer.Clear(game);

      if (superstate.turnsIncludingPresent.Count < 2) {
        context.logger.Error("Can't time shift, nothing to time shift back to!");
        return false;
      }

      RootIncarnation pastIncarnation =
          superstate.turnsIncludingPresent[superstate.turnsIncludingPresent.Count - 2];

      var pastGame = pastIncarnation.incarnationsGame[gameId].incarnation;
      var pastTime = pastGame.time;
      int timeDifference = superstate.futuremostTime - pastTime;
      int mpCost = 5 + timeDifference / 100;

      if (game.player.mp < 5) {
        game.root.logger.Info("Not enough mp now to cast the spell, need 5!");
        return false;
      }
      if (pastIncarnation.incarnationsUnit[game.player.id].incarnation.mp < mpCost) {
        game.root.logger.Info("Not enough mp back then to cast the spell!");
        return false;
      }

      // Now that we're sure we can do it, do it!


      // Since we do it everywhere else...
      superstate.turnsIncludingPresent.Add(context.root.Snapshot());
      superstate.futuremostTime = Math.Max(superstate.futuremostTime, game.time);

      superstate.turnsIncludingPresent.RemoveRange(
          superstate.turnsIncludingPresent.Count - 2,
          2);

      // time shift costs 5 now, and 5 + timeDifference / 100 from your past self.


      float futureTime = game.time;
      context.root.Revert(pastIncarnation);

      // We don't want to follow the player's directive from back then.
      game.player.ClearDirective();

      var player = game.player;
      player.mp = player.mp - mpCost;

      superstate.liveUnitByLocationMap.Reconstruct(game);

      //float pastTime = game.time;

      // later, drain hp proportional to this

      return true;
    }
  }
}
