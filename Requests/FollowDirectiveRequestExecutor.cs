using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class FollowDirectiveRequestExecutor {
    public static string Execute(
        SSContext context,
        Superstate superstate, 
        FollowDirectiveRequest request) {
      int gameId = request.gameId;
      var game = context.root.GetGame(gameId);

      EventsClearer.Clear(game);

      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerResume) {
        return "Error: Cannot follow directive, state is " + superstate.GetStateType();
      }
      if (superstate.timeShiftingState != null) {
        return "Error: Cannot follow directive while time shifting!";
      }

      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }

      if (!game.player.GetDirectiveOrNull().Exists()) {
        return "Error: Player has no directive, can't resume!";
      }

      superstate.previousTurns.Add(context.root.Snapshot());

      game.lastPlayerRequest = request.AsIRequest();

      GameLoop.ContinueAtPlayerFollowDirective(game, superstate, new PauseCondition(false));

      return "";
    }
  }
}
