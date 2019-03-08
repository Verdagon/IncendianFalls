using System;
using Atharia.Model;

namespace IncendianFalls {
  public class CheatRequestExecutor {
    public static string Execute(
        SSContext context,
        Superstate superstate,
        CheatRequest request) {
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

      switch (request.cheatName) {
        case "warptoend":
          Location end = null;
          foreach (var entry in game.level.terrain.tiles) {
            var stairs = entry.Value.components.GetOnlyStaircaseTTCOrNull();
            if (stairs.Exists() && stairs.portalIndex == 1) {
              end = entry.Key;
              break;
            }
          }
          if (end == null) {
            return "Couldn't find end to warp to!";
          }
          if (!superstate.levelSuperstate.IsLocationWalkable(end, true)) {
            return "Couldn't warp to end!";
          }
          Actions.Step(game, superstate, game.player, end, true);
          break;
        default:
          return "Unknown cheat: " + request.cheatName;
      }

      GameLoop.NoteUnitActed(game, game.player);

      return "";
    }
  }
}

