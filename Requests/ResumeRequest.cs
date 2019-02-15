using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class ResumeRequestExecutor {
    ILogger logger;
    public ResumeRequestExecutor(ILogger logger) {
      this.logger = logger;
    }
    public bool Execute(Root root, int gameId) {
      var game = root.GetGame(gameId);

      var liveUnitByLocationMap = PreRequest.Do(game);

      switch (game.GetExecutionStateType()) {
        case GameExecutionStateType.kBetweenUnits:
          GameLoop.ContinueAtStartTurn(game, liveUnitByLocationMap);
          break;
        case GameExecutionStateType.kAfterUnitAction:
          GameLoop.ContinueAfterUnitAction(game, liveUnitByLocationMap);
          break;
        case GameExecutionStateType.kPreActingDetail:
          GameLoop.ContinueAfterPreActingDetail(game, liveUnitByLocationMap);
          break;
        case GameExecutionStateType.kPostActingDetail:
          GameLoop.ContinueAfterPostActingDetail(game, liveUnitByLocationMap);
          break;
        case GameExecutionStateType.kBeforeEnemyAction:
          // shouldnt be possible, the game loop doesnt pause here.
          Asserts.Assert(false);
          break;
        case GameExecutionStateType.kBeforePlayerAction:
          logger.Error("Use a Move, Attack, FollowDirective etc to resume when paused before player!");
          return false;
      }

      return true;
    }
  }
}
