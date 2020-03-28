using Atharia.Model;
using Gauntlet;
using EmberDeep;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  public static class CommActionRequestExecutor {
    public static string Execute(
        SSContext context,
        Superstate superstate,
        CommActionRequest request) {
      int gameId = request.gameId;
      int commId = request.commId;
      int actionIndex = request.actionIndex;

      var game = context.root.GetGame(gameId);
      var comm = context.root.GetComm(commId);
      if (actionIndex < 0 || actionIndex >= comm.actions.Count) {
        return "Action index out of range!";
      }
      var action = comm.actions[actionIndex];
      var triggerName = action.triggerName;

      if (triggerName == "") {
        return "";
      }

      game.level.controller.SimpleTrigger(context, game, superstate, triggerName);

      return "";
    }
  }
}
