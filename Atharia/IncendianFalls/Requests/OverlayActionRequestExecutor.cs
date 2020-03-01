using Atharia.Model;
using Gauntlet;
using EmberDeep;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  public static class OverlayActionRequestExecutor {
    public static string Execute(
        SSContext context,
        Superstate superstate,
        OverlayActionRequest request) {

      int gameId = request.gameId;
      var game = context.root.GetGame(gameId);

      EventsClearer.Clear(game);

      game.root.logger.Error("overlay action! " + request.buttonIndex);

      if (!game.overlay.Exists()) {
        game.root.logger.Error("no overlay");
        return "No overlay!";
      }
      if (game.overlay.fadeOutEndMs == 0) {
        game.root.logger.Error("manual dismiss");
        if (request.buttonIndex >= game.overlay.buttons.Count) {
          game.root.logger.Error("button index out of range!");
          return "Button index out of range!";
        }
        var buttonTriggerName = game.overlay.buttons[request.buttonIndex].triggerName;
        game.level.controller.SimpleTrigger(game, superstate, buttonTriggerName);
      } else {
        game.root.logger.Error("auto dismiss");
        var buttonTriggerName = game.overlay.automaticActionTriggerName;
        game.level.controller.SimpleTrigger(game, superstate, buttonTriggerName);
      }

      return "";
    }
  }
}
