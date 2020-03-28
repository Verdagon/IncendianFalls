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

      Asserts.Assert(!game.WaitingOnPlayerInput());

      if (game.actingUnit.Exists()) {
        GameLoop.ContinueAfterUnitAction(context, game, superstate);
      } else {
        GameLoop.ContinueBetweenUnits(context, game, superstate);
      }

      return "";
    }
  }
}
