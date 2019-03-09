﻿using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class DefendRequestExecutor {
    public static string Execute(
        SSContext context,
        Superstate superstate,
        DefendRequest request) {
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

      game.lastPlayerRequest = request.AsIRequest();

      player.ClearDirective();

      Actions.Defend(game, game.player);

      GameLoop.NoteUnitActed(game, game.player);

      return "";
    }
  }
}
