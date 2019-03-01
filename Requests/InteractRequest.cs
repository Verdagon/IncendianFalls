using System;
using Atharia.Model;

namespace IncendianFalls {
  public class InteractRequestExecutor {
    public static bool Execute(SSContext context, int gameId, LiveUnitByLocationMap liveUnitByLocationMap) {
      var game = context.root.GetGame(gameId);
      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }

      PreRequest.Do(game);

      var player = game.player;

      Asserts.Assert(game.executionState.actingUnit.Is(game.player));
      Asserts.Assert(game.player.Is(Utils.GetNextActingUnit(game)));

      player.ClearDirective();

      bool success = Actions.Interact(context, game, game.player);

      GameLoop.NoteUnitActed(game, game.player);

      return success;
    }
  }
}

