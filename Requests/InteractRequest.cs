using System;
using Atharia.Model;

namespace IncendianFalls {
  public class InteractRequestExecutor {
    public static bool Execute(SSContext context, Superstate superstate, int gameId) {
      var game = context.root.GetGame(gameId);
      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }

      PreRequest.Do(game);

      superstate.turnsIncludingPresent.Insert(0, context.root.Snapshot());
      superstate.futuremostTime = Math.Max(superstate.futuremostTime, game.time);

      var snapshot = context.root.Snapshot();
      superstate.turnsIncludingPresent.Add(snapshot);

      var player = game.player;

      Asserts.Assert(game.executionState.actingUnit.Is(game.player));
      Asserts.Assert(game.player.Is(Utils.GetNextActingUnit(game)));

      player.ClearDirective();

      bool success = Actions.Interact(context, game, superstate, game.player);

      GameLoop.NoteUnitActed(game, game.player);

      return success;
    }
  }
}

