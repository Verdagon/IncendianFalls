using System;
using Atharia.Model;

namespace IncendianFalls {
  public class InteractRequestExecutor {
    public static bool Execute(
        SSContext context,
        Superstate superstate,
        InteractRequest request) {
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

      superstate.previousTurns.Add(context.root.Snapshot());

      game.lastPlayerRequest = request.AsIRequest();

      player.ClearDirective();

      bool success = Actions.Interact(context, game, superstate, game.player);

      GameLoop.NoteUnitActed(game, game.player);

      return success;
    }
  }
}

