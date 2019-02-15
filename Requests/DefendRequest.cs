using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class DefendRequestExecutor {
    ILogger logger;
    public DefendRequestExecutor(ILogger logger) {
      this.logger = logger;
    }
    public bool Execute(Root root, int gameId) {
      var game = root.GetGame(gameId);
      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }

      var liveUnitByLocationMap = PreRequest.Do(game);

      var player = game.player;

      Asserts.Assert(game.executionState.actingUnit.Is(game.player));
      Asserts.Assert(game.player.Is(Utils.GetNextActingUnit(root, game)));

      Actions.Defend(game, game.player);

      GameLoop.NoteUnitActed(game, game.player);

      return true;
    }
  }
}
