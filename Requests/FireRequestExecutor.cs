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

      EventsClearer.Clear(game);

      Asserts.Assert(game.executionState.actingUnit.Is(game.player));
      Asserts.Assert(game.player.Is(Utils.GetNextActingUnit(game)));
      Asserts.Assert(superstate.timeShiftingState == null);

      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }
      var player = game.player;

      Unit victim = context.root.GetUnit(targetUnitId);
      if (!victim.alive) {
        return "Victim is already dead!";
      }

      if (victim.Is(player)) {
        return "Can't fire at self!";
      }

      if (game.level.terrain.pattern.GetDistanceBetween(victim.location, player.location) > 5) {
        return "Too far, can't fire!";
      }

      if (player.mp < 18) {
        return "Can't fire, need 18mp!";
      }

      superstate.previousTurns.Add(context.root.Snapshot());

      game.lastPlayerRequest = request.AsIRequest();

      player.ClearDirective();

      Actions.Fire(game, superstate, game.player, victim);

      GameLoop.NoteUnitActed(game, game.player);

      return "";
    }
  }
}

