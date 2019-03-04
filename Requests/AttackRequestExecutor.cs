using System;
using Atharia.Model;

namespace IncendianFalls {
  public class AttackRequestExecutor {
    public static bool Execute(
        SSContext context,
        Superstate superstate,
        AttackRequest request) {

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
        return false;
      }

      superstate.previousTurns.Add(context.root.Snapshot());

      game.lastPlayerRequest = request.AsIRequest();

      player.ClearDirective();

      Actions.Bump(game, superstate, game.player, victim);

      GameLoop.NoteUnitActed(game, game.player);

      return true;
    }
  }
}

