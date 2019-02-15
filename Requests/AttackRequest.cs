using System;
using Atharia.Model;

namespace IncendianFalls {
  public class AttackRequestExecutor {
    ILogger logger;
    public AttackRequestExecutor(ILogger logger) {
      this.logger = logger;
    }
    public bool Execute(Root root, int gameId, int targetUnitId) {
      var game = root.GetGame(gameId);
      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }

      var liveUnitByLocationMap = PreRequest.Do(game);

      var player = game.player;

      Unit victim = root.GetUnit(targetUnitId);
      if (!victim.alive) {
        return false;
      }

      Asserts.Assert(game.executionState.actingUnit.Is(game.player));
      Asserts.Assert(game.player.Is(Utils.GetNextActingUnit(root, game)));

      Actions.Attack(game, liveUnitByLocationMap, game.player, victim);

      GameLoop.NoteUnitActed(game, game.player);

      return true;
    }
  }
}

