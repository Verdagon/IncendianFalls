﻿using System;
using Atharia.Model;

namespace IncendianFalls {
  public class InteractRequestExecutor {
    public static string Execute(
        SSContext context,
        Superstate superstate,
        InteractRequest request) {
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

      superstate.previousTurns.Add(context.root.Snapshot());
      superstate.requests.Add(request.AsIRequest());
      //player.ClearDirective();

      context.Flare(game.root.GetDeterministicHashCode());

      string success = Actions.Interact(context, game, superstate, game.player);

      context.Flare(game.root.GetDeterministicHashCode());

      //if (success == "") {
      //  GameLoop.NoteUnitActed(game, game.player);
      //}

      return success;
    }
  }
}

