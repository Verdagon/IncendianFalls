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

      switch (request.cheatName) {
        case "warptoend":
          Location end = null;
          if (end == null) {
            foreach (var entry in game.level.terrain.tiles) {
              var found = entry.Value.components.GetOnlyIncendianFallsLevelLinkerTTCOrNull();
              if (found.Exists()) {
                end = entry.Key;
                break;
              }
            }
          }
          if (end == null) {
            foreach (var entry in game.level.terrain.tiles) {
              var found = entry.Value.components.GetOnlyEmberDeepLevelLinkerTTCOrNull();
              if (found.Exists()) {
                end = entry.Key;
                break;
              }
            }
          }
          if (end == null) {
            foreach (var entry in game.level.terrain.tiles) {
              var found = entry.Value.components.GetAllLevelLinkTTC();
              if (found.Count > 0) {
                end = entry.Key;
                break;
              }
            }
          }
          if (end == null) {
            return "Couldn't find end to warp to!";
          }
          if (!superstate.levelSuperstate.IsLocationWalkable(end, true)) {
            return "Couldn't warp to end!";
          }
          Actions.Teleport(game, superstate, game.player, end);
          break;
        case "poweroverwhelming":
          game.player.components.Add(
            game.root.EffectInvincibilityUCCreate().AsIUnitComponent());
          break;
        case "gimmeblastrod":
          game.player.components.Add(
            game.root.EffectBlastRodCreate().AsIUnitComponent());
          break;
        case "gimmeslowrod":
          game.player.components.Add(
            game.root.EffectSlowRodCreate().AsIUnitComponent());
          break;
        case "gimmearmor":
          game.player.components.Add(
            game.root.EffectArmorCreate().AsIUnitComponent());
          break;
        case "gimmesword":
          game.player.components.Add(
            game.root.EffectGlaiveCreate().AsIUnitComponent());
          break;
        case "healinglove":
          game.player.hp = game.player.maxHp;
          break;
        default:
          return "Unknown cheat: " + request.cheatName;
      }

      //GameLoop.NoteUnitActed(game, game.player);

      return "";
    }
  }
}
