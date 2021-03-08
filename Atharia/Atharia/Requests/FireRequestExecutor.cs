using System;
using Atharia.Model;

namespace IncendianFalls {
  public class FireRequestExecutor {
    public static string Execute(
        SSContext context,
        Superstate superstate,
        FireRequest request) {

      int gameId = request.gameId;
      int targetUnitId = request.targetUnitId;

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

      Unit victim = context.root.GetUnit(targetUnitId);
      if (!victim.Alive()) {
        return "Victim is already dead!";
      }

      if (victim.Is(player)) {
        return "Can't fire at self!";
      }

      if (game.level.terrain.pattern.GetDistanceBetween(victim.location, player.location) > 5) {
        return "Too far, can't fire!";
      }

      var sorcerous = game.player.components.GetOnlySorcerousUCOrNull();
      Asserts.Assert(sorcerous != null, "Cant use potion, dont have sorcerous!");
      if (sorcerous.mp < Actions.FIRE_COST) {
        return "Can't fire, need " + Actions.FIRE_COST + "mp!";
      }

      superstate.previousTurns.Add(context.root.Snapshot());
      superstate.requests.Add(request.AsIRequest());

      //player.ClearDirective();

      Actions.Fire(game, superstate, game.player, victim);

      //GameLoop.NoteUnitActed(game, game.player);

      return "";
    }
  }
}

