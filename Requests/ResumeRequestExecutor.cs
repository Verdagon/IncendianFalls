using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class ResumeRequestExecutor {
    public static string Execute(
        SSContext context,
        Superstate superstate,
        ResumeRequest request) {
      int gameId = request.gameId;
      var game = context.root.GetGame(gameId);

      if (superstate.timeShiftingState != null) {
        return "Error: Cannot resume while timeshifting!";
      }

      EventsClearer.Clear(game);

      Asserts.Assert(game.player.Exists() && game.player.alive);

      GameLoop.Continue(game, superstate, new PauseCondition(false));

      return "";
    }
  }
}
