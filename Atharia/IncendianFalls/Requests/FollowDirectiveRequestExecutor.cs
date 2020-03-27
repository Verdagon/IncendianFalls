using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  //public class FollowDirectiveRequestExecutor {
  //  public static string Execute(
  //      SSContext context,
  //      Superstate superstate, 
  //      FollowDirectiveRequest request) {
  //    int gameId = request.gameId;
  //    var game = context.root.GetGame(gameId);

  //    EventsClearer.Clear(game);

  //    if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
  //      return "Error: Cannot follow directive, state is " + superstate.GetStateType();
  //    }
  //    if (superstate.timeShiftingState != null) {
  //      return "Error: Cannot follow directive while time shifting!";
  //    }

  //    if (!game.player.Exists()) {
  //      throw new Exception("Player is dead!");
  //    }

  //    if (superstate.navigatingState == null ||
  //        superstate.navigatingState.path.Count == 0) {
  //      return "Error: Player has no navigation, can't resume!";
  //    }

  //    if (!Actions.CanStep(game, superstate, game.player, superstate.navigatingState.path[0])) {
  //      superstate.navigatingState = null;
  //      return "Path blocked!";
  //    }

  //    MoveRequestExecutor.Continue(context, game, superstate);
  //    return "";
  //  }
  //}
}
