using System;
using Atharia.Model;

namespace Atharia.Model {
  public enum WorldStateType {
    kBetweenUnits = 0,
    kPreActingDetail = 1,
    kAfterUnitAction = 2,
    kBeforeEnemyAction = 3,
    kBeforePlayerInput = 4,
    kPostActingDetail = 5
  }

  public static class GameExtensions {
    public static WorldStateType GetStateType(this Game game) {
      var executionState = game.executionState;
      var player = game.player;
      if (!executionState.actingUnit.Exists()) {
        return WorldStateType.kBetweenUnits;
      }
      if (executionState.remainingPreActingUnitComponents.Exists()) {
        return WorldStateType.kPreActingDetail;
      }
      if (executionState.remainingPostActingUnitComponents.Exists()) {
        return WorldStateType.kPostActingDetail;
      }
      if (!executionState.actingUnitDidAction) {
        if (game.player.Exists() && executionState.actingUnit.Is(game.player)) {
          return WorldStateType.kBeforePlayerInput;
        } else {
          return WorldStateType.kBeforeEnemyAction;
        }
      } else {
        return WorldStateType.kAfterUnitAction;
      }
    }

    public static void AddEvent(this Game game, IGameEvent e) {
      game.events.Add(e);
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
