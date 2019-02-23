using System;
using Atharia.Model;

namespace IncendianFalls {
  public class AttackRequestExecutor {
    public static bool Execute(SSContext context, int gameId, int targetUnitId) {
      var game = context.root.GetGame(gameId);
      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }

      var liveUnitByLocationMap = PreRequest.Do(game);

      var player = game.player;

      Unit victim = context.root.GetUnit(targetUnitId);
      if (!victim.alive) {
        return false;
      }

      Asserts.Assert(game.executionState.actingUnit.Is(game.player));
      Asserts.Assert(game.player.Is(Utils.GetNextActingUnit(game)));

      Actions.Attack(game, liveUnitByLocationMap, game.player, victim, true);

      GameLoop.NoteUnitActed(game, game.player);

      return true;
    }
  }
}

