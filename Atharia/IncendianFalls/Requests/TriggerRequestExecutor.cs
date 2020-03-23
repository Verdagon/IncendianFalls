using Atharia.Model;
using Gauntlet;
using EmberDeep;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  public static class TriggerRequestExecutor {
    public static string Execute(
        SSContext context,
        Superstate superstate,
        TriggerRequest request) {

      int gameId = request.gameId;
      var game = context.root.GetGame(gameId);

      EventsClearer.Clear(game);

      game.level.controller.SimpleTrigger(context, game, superstate, request.triggerName);

      return "";
    }
  }
}
