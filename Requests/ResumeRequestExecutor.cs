using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class ResumeRequestExecutor {
    public static bool Execute(
        SSContext context,
        Superstate superstate,
        ResumeRequest request) {
      int gameId = request.gameId;
      var game = context.root.GetGame(gameId);

      Asserts.Assert(superstate.timeShiftingState == null);

      EventsClearer.Clear(game);

      GameLoop.Continue(game, superstate, new PauseCondition(false));

      return true;
    }
  }
}
