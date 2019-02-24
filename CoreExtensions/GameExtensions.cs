using System;
using Atharia.Model;

namespace Atharia.Model {
  public enum GameExecutionStateType {
    kBetweenUnits = 0,
    kPreActingDetail = 1,
    kAfterUnitAction = 2,
    kBeforeEnemyAction = 3,
    kBeforePlayerAction = 4,
    kPostActingDetail = 5,
  }

  public static class GameExtensions {
    public static GameExecutionStateType GetExecutionStateType(this Game game) {
      if (!game.executionState.actingUnit.Exists()) {
        return GameExecutionStateType.kBetweenUnits;
      }
      if (game.executionState.remainingPreActingUnitComponents.Exists()) {
        return GameExecutionStateType.kPreActingDetail;
      }
      if (game.executionState.remainingPostActingUnitComponents.Exists()) {
        return GameExecutionStateType.kPostActingDetail;
      }
      if (!game.executionState.actingUnitDidAction) {
        if (game.executionState.actingUnit.Is(game.player)) {
          return GameExecutionStateType.kBeforePlayerAction;
        } else {
          return GameExecutionStateType.kBeforeEnemyAction;
        }
      } else {
        return GameExecutionStateType.kAfterUnitAction;
      }
    }
  }
}
