using System;
using Atharia.Model;

namespace IncendianFalls {
  //public class CancelRequestExecutor {
  //  public static string Execute(
  //      SSContext context,
  //      Superstate superstate,
  //      CancelRequest request) {

  //    int gameId = request.gameId;

  //    var game = context.root.GetGame(gameId);

  //    EventsClearer.Clear(game);

  //    if (superstate.navigatingState != null) {
  //      superstate.navigatingState = null;
  //      game.AddEvent(
  //        new ShowOverlayEvent(
  //          "Canceled movement!",
  //          "error",
  //          "narrator",
  //          true,
  //          true,
  //          false,
  //          new OverlayButtonImmList())
  //        .AsIGameEvent());
  //    } else {
  //      game.AddEvent(
  //        new ShowOverlayEvent(
  //          "No movement to cancel!",
  //          "error",
  //          "narrator",
  //          true,
  //          true,
  //          false,
  //          new OverlayButtonImmList())
  //        .AsIGameEvent());
  //    }

  //    return "";
  //  }
  //}
}

