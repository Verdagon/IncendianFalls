using System;
using Atharia.Model;

namespace Atharia.Model {
  public enum WorldStateType {
    kBetweenUnits = 0,
    kBeforePlayerInput = 4,
  }

  public static class GameExtensions {
    public static bool WaitingOnPlayerInput(this Game game) {
      return game.actingUnit.Exists() &&
        game.actingUnit.NullableIs(game.player) &&
          game.player.alive &&
          game.player.nextActionTime == game.time;
    }
    public static void AddEvent(this Game game, IGameEvent e) {
      game.evvent = e;
      game.evvent = NullIGameEvent.Null;
    }

    public static void EnterCinematic(this Game game) {
      if (game.hideInput) {
        game.root.logger.Error("Entering cinematic but we were already in one!");
      }
      game.hideInput = true;
    }

    public static void ExitCinematic(this Game game) {
      if (!game.hideInput) {
        game.root.logger.Error("Exiting cinematic but we werent in one!");
      }
      game.hideInput = false;
    }
  }
}
