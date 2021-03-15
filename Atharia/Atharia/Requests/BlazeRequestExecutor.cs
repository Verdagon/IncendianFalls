using System;
using Atharia.Model;

namespace IncendianFalls {
  public class BlazeRequestExecutor {
    public static string Execute(
        SSContext context,
        Superstate superstate,
        BlazeRequest request) {

      int gameId = request.gameId;
      var targetLoc = request.targetLoc;

      var game = context.root.GetGame(gameId);

      Asserts.Assert(game.WaitingOnPlayerInput());

      if (!game.actingUnit.Is(game.player)) {
        return "Error: Player not next acting unit! (a)";
      }

      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }
      var player = game.player;

      if (game.level.terrain.pattern.GetDistanceBetween(targetLoc, player.location) > Actions.BLAZE_RANGE) {
        return "Too far, can't blaze there!";
      }

      var sorcerous = game.player.components.GetOnlySorcerousUCOrNull();
      Asserts.Assert(sorcerous != null, "Cant blaze, dont have sorcerous!");
      if (sorcerous.mp < Actions.BLAZE_COST) {
        return "Can't blaze, need " + Actions.BLAZE_COST + "mp!";
      }

      superstate.previousTurns.Add(context.root.Snapshot());
      superstate.requests.Add(request.AsIRequest());

      //player.ClearDirective();

      Actions.Blaze(game, superstate, game.player, targetLoc);

      //GameLoop.NoteUnitActed(game, game.player);

      return "";
    }
  }
}

