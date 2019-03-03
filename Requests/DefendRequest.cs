using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class DefendRequestExecutor {
    public static bool Execute(
        SSContext context,
        Superstate superstate,
        int gameId) {
      var game = context.root.GetGame(gameId);
      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }

      PreRequest.Do(game);

      superstate.turnsIncludingPresent.Insert(0, context.root.Snapshot());
      superstate.futuremostTime = Math.Max(superstate.futuremostTime, game.time);

      var player = game.player;

      Asserts.Assert(game.executionState.actingUnit.Is(game.player));
      Asserts.Assert(game.player.Is(Utils.GetNextActingUnit(game)));

      player.ClearDirective();

      Actions.Defend(game, game.player);

      GameLoop.NoteUnitActed(game, game.player);

      return true;
    }
  }
}
