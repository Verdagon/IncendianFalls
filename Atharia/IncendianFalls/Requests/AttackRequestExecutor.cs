using System;
using Atharia.Model;

namespace IncendianFalls {
  public class AttackRequestExecutor {
    public static string Execute(
        SSContext context,
        Superstate superstate,
        AttackRequest request) {

      int gameId = request.gameId;
      int targetUnitId = request.targetUnitId;

      var game = context.root.GetGame(gameId);

      EventsClearer.Clear(game);

      if (!game.executionState.actingUnit.Is(game.player)) {
        return "Error: Player not next acting unit! (a)";
      }
      //if (!game.player.Is(Utils.GetNextActingUnit(game))) {
      //  return "Error: Player not next acting unit! (b)";
      //}
      if (superstate.timeShiftingState != null) {
        return "Error: Cannot attack while time shifting!";
      }

      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }
      var player = game.player;

      Unit victim = context.root.GetUnit(targetUnitId);
      if (!victim.alive) {
        return "Can't attack, victim already dead!";
      }

      int elevationDifference =
          game.level.terrain.GetElevationDifference(victim.location, player.location);
      if (elevationDifference > 2) {
        return "Can't attack that unit, can only attack <=2 elevation up or down.";
      }

      superstate.previousTurns.Add(context.root.Snapshot());
      superstate.requests.Add(request.AsIRequest());

      //player.ClearDirective();

      Actions.Bump(game, superstate, game.player, victim, 1.0f, true);

      GameLoop.NoteUnitActed(game, game.player);

      return "";
    }
  }
}

