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

      Asserts.Assert(superstate.GetStateType() == MultiverseStateType.kBeforePlayerInput);
      Asserts.Assert(superstate.timeShiftingState == null);

      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }

      if (!game.player.GetDirectiveOrNull().Exists()) {
        context.logger.Error("Player has no directive, can't resume!");
        return false;
      }

      superstate.previousTurns.Add(context.root.Snapshot());

      game.lastPlayerRequest = request.AsIRequest();

      GameLoop.ContinueAtPlayerFollowDirective(game, superstate, new PauseCondition(false));

      return true;
    }
  }
}
