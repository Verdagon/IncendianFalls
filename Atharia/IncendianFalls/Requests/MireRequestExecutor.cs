﻿using System;
using Atharia.Model;

namespace IncendianFalls {
  public class MireRequestExecutor {
    public static string Execute(
        SSContext context,
        Superstate superstate,
        MireRequest request) {

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
        return "Error: Cannot slow while time shifting!";
      }

      if (!game.player.Exists()) {
        throw new Exception("Player is dead!");
      }
      var player = game.player;

      Unit victim = context.root.GetUnit(targetUnitId);
      if (!victim.alive) {
        return "Victim is already dead!";
      }

      if (victim.Is(player)) {
        return "Can't slow self!";
      }

      if (game.level.terrain.pattern.GetDistanceBetween(victim.location, player.location) > 5) {
        return "Too far, can't slow!";
      }

      var sorcerous = game.player.components.GetOnlySorcerousUCOrNull();
      Asserts.Assert(sorcerous != null, "Cant use potion, dont have sorcerous!");
      if (sorcerous.mp < Actions.MIRE_COST) {
        return "Can't slow, need " + Actions.MIRE_COST + "mp!";
      }

      superstate.previousTurns.Add(context.root.Snapshot());
      superstate.requests.Add(request.AsIRequest());

      //player.ClearDirective();

      Actions.Mire(game, superstate, game.player, victim);

      GameLoop.NoteUnitActed(game, game.player);

      return "";
    }
  }
}
