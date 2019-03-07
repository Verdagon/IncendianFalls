using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class CounterRequestExecutor {
    public static string Execute(
        SSContext context,
        Superstate superstate,
        CounterRequest request) {
      int gameId = request.gameId;
      var game = context.root.GetGame(gameId);

      EventsClearer.Clear(game);

      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }
      var player = game.player;

      Asserts.Assert(game.executionState.actingUnit.Is(game.player));
      Asserts.Assert(game.player.Is(Utils.GetNextActingUnit(game)));
      Asserts.Assert(superstate.timeShiftingState == null);

      if (player.mp < 3) {
        return "Can't counter, requires 1mp up-front, and 2mp more if attacked!";
      }

      superstate.previousTurns.Add(context.root.Snapshot());

      game.lastPlayerRequest = request.AsIRequest();

      player.ClearDirective();

      Actions.Counter(game, game.player);

      GameLoop.NoteUnitActed(game, game.player);

      return "";
    }
  }
}
