using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  //public struct TimeShiftRequest : IRequest {
  //  public readonly int gameId;
  //  public readonly RootIncarnation pastIncarnation;

  //  public TimeShiftRequest(int gameId, RootIncarnation pastIncarnation) {
  //    this.gameId = gameId;
  //    this.pastIncarnation = pastIncarnation;
  //  }

  //  public void visit(IRequestVisitor visitor) {
  //    visitor.visitTimeShiftRequest(this);
  //  }
  //}

  public class TimeShiftRequestExecutor {
    ILogger logger;
    public TimeShiftRequestExecutor(ILogger logger) {
      this.logger = logger;
    }
    public bool Execute(
        Root root,
        int gameId,
        RootIncarnation pastIncarnation,
        int futuremostTime) {
      var game = root.GetGame(gameId);

      // time shift costs 5 now, and 5 + timeDifference / 100 from your past self.

      var pastGame = pastIncarnation.incarnationsGame[gameId].incarnation;
      var pastTime = pastGame.time;
      int timeDifference = futuremostTime - pastTime;
      int mpCost = 5 + timeDifference / 100;
      Console.WriteLine("time difference is " + timeDifference + " so mp cost " + mpCost);
      if (game.player.mp < 5) {
        Console.WriteLine("Not enough mp now to cast the spell, need 5!");
        return false;
      }
      if (pastIncarnation.incarnationsUnit[game.player.id].incarnation.mp < mpCost) {
        Console.WriteLine("Not enough mp back then to cast the spell!");
        return false;
      }

      PreRequest.Do(game);

      float futureTime = game.time;
      root.Revert(pastIncarnation);

      // We don't want to follow the player's directive from back then.
      if (game.player.directive.Exists()) {
        game.player.directive.Delete();
      }

      var player = game.player;
      player.mp = player.mp - mpCost;

      //float pastTime = game.time;

      // later, drain hp proportional to this

      return true;
    }
  }
}
