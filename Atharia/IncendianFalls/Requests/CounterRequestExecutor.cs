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

      Asserts.Assert(game.WaitingOnPlayerInput());

      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }
      var player = game.player;

      if (!game.actingUnit.Is(game.player)) {
        return "Error: Player not next acting unit! (a)";
      }
      //if (!game.player.Is(Utils.GetNextActingUnit(game))) {
      //  return "Error: Player not next acting unit! (b)";
      //}
      if (superstate.timeShiftingState != null) {
        return "Error: Cannot counter while time shifting!";
      }

      var sorcerous = player.components.GetOnlySorcerousUCOrNull();
      Asserts.Assert(sorcerous != null);
      if (sorcerous.mp < 3) {
        return "Can't counter, requires 1mp up-front, and 2mp more if attacked!";
      }

      superstate.previousTurns.Add(context.root.Snapshot());
      superstate.requests.Add(request.AsIRequest());

      //player.ClearDirective();

      Actions.Counter(game, game.player);

      //GameLoop.NoteUnitActed(game, game.player);

      return "";
    }
  }
}
