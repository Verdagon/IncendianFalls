using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class ResumeRequestExecutor {
    public static bool Execute(
        SSContext context,
        Superstate superstate,
        ResumeRequest request) {
      int gameId = request.gameId;
      var game = context.root.GetGame(gameId);

      EventsClearer.Clear(game);

      switch (game.GetExecutionStateType()) {
        case GameExecutionStateType.kBetweenUnits:
          GameLoop.ContinueAtStartTurn(game, superstate);
          break;
        case GameExecutionStateType.kAfterUnitAction:
          GameLoop.ContinueAfterUnitAction(game, superstate);
          break;
        case GameExecutionStateType.kPreActingDetail:
          GameLoop.ContinueAfterPreActingDetail(game, superstate);
          break;
        case GameExecutionStateType.kPostActingDetail:
          GameLoop.ContinueAfterPostActingDetail(game, superstate);
          break;
        case GameExecutionStateType.kBeforeEnemyAction:
          // shouldnt be possible, the game loop doesnt pause here.
          Asserts.Assert(false);
          break;
        case GameExecutionStateType.kBeforePlayerAction:
          context.logger.Error("Use a Move, Attack, FollowDirective etc to resume when paused before player!");
          return false;
      }

      return true;
    }
  }
}
