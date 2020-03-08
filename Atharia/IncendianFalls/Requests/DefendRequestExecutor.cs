using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class DefyRequestExecutor {
    public static string Execute(
        SSContext context,
        Superstate superstate,
        DefyRequest request) {
      int gameId = request.gameId;
      var game = context.root.GetGame(gameId);

      EventsClearer.Clear(game);

      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }
      var player = game.player;

      if (!game.executionState.actingUnit.Is(game.player)) {
        return "Error: Player not next acting unit! (a)";
      }
      //if (!game.player.Is(Utils.GetNextActingUnit(game))) {
      //  return "Error: Player not next acting unit! (b)";
      //}
      if (superstate.timeShiftingState != null) {
        return "Error: Cannot defend while time shifting!";
      }

      superstate.previousTurns.Add(context.root.Snapshot());
      superstate.requests.Add(request.AsIRequest());

      //player.ClearDirective();

      Actions.Defy(game, game.player);

      var impulse = game.root.EffectDefyImpulseCreate(1000);
      foreach (var postReactor in game.player.components.GetAllIImpulsePostReactor()) {
        postReactor.AfterImpulse(game, superstate, game.player, NullIAICapabilityUC.Null, impulse.AsIImpulse());
      }
      impulse.Destruct();

      GameLoop.NoteUnitActed(game, game.player);

      return "";
    }
  }
}
