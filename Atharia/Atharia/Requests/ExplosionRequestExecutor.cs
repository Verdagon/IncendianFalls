using System;
using Atharia.Model;

namespace IncendianFalls {
  public class ExplosionRequestExecutor {
    public static string Execute(
        SSContext context,
        Superstate superstate,
        ExplosionRequest request) {

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

      if (game.level.terrain.pattern.GetDistanceBetween(targetLoc, player.location) > Actions.EXPLOSION_RANGE) {
        return "Too far, can't explosion there!";
      }

      var sorcerous = game.player.components.GetOnlySorcerousUCOrNull();
      Asserts.Assert(sorcerous != null, "Cant explosion, dont have sorcerous!");
      if (sorcerous.mp < Actions.EXPLOSION_COST) {
        return "Can't explosion, need " + Actions.EXPLOSION_COST + "mp!";
      }

      superstate.previousTurns.Add(context.root.Snapshot());
      superstate.requests.Add(request.AsIRequest());

      //player.ClearDirective();

      Actions.Explosion(game, superstate, game.player, targetLoc);

      //GameLoop.NoteUnitActed(game, game.player);

      return "";
    }
  }
}

