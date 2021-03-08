using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class FireBombRequestExecutor {
    public static string Execute(
        SSContext context,
        Superstate superstate,
        FireBombRequest request) {

      int gameId = request.gameId;
      var location = request.location;

      var game = context.root.GetGame(gameId);

      Asserts.Assert(game.WaitingOnPlayerInput());

      if (!game.actingUnit.Is(game.player)) {
        return "Error: Player not next acting unit! (a)";
      }
      //if (!game.player.Is(Utils.GetNextActingUnit(game))) {
      //  return "Error: Player not next acting unit! (b)";
      //}

      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }
      var player = game.player;

      if (player.components.GetAllBlastRod().Count == 0) {
        return "Can't fire bomb without Fire Rod!";
      }

      if (game.level.terrain.pattern.GetDistanceBetween(location, player.location) > 5) {
        return "Too far, can't fire bomb!";
      }

      var sorcerous = game.player.components.GetOnlySorcerousUCOrNull();
      Asserts.Assert(sorcerous != null, "Cant use potion, dont have sorcerous!");
      if (sorcerous.mp < Actions.FIRE_BOMB_COST) {
        return "Can't fire bomb, need " + Actions.FIRE_BOMB_COST + "mp!";
      }

      superstate.previousTurns.Add(context.root.Snapshot());
      superstate.requests.Add(request.AsIRequest());

      //player.ClearDirective();

      Actions.PlaceFireBomb(game, superstate, game.player, location);

      //GameLoop.NoteUnitActed(game, game.player);

      return "";
    }
  }
}
