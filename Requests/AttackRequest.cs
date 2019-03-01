using System;
using Atharia.Model;

namespace IncendianFalls {
  public class AttackRequestExecutor {
    public static bool Execute(
        SSContext context,
        int gameId,
        LiveUnitByLocationMap liveUnitByLocationMap, 
        int targetUnitId) {
      var game = context.root.GetGame(gameId);
      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }

      PreRequest.Do(game);

      var player = game.player;

      Asserts.Assert(game.executionState.actingUnit.Is(game.player));
      Asserts.Assert(game.player.Is(Utils.GetNextActingUnit(game)));

      player.ClearDirective();

      Unit victim = context.root.GetUnit(targetUnitId);
      if (!victim.alive) {
        return false;
      }

      Actions.Bump(game, liveUnitByLocationMap, game.player, victim);

      GameLoop.NoteUnitActed(game, game.player);

      return true;
    }
  }
}

