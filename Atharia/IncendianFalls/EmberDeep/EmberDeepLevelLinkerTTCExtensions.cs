using Atharia.Model;
using IncendianFalls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Atharia.Model {
  static class EmberDeepLevelLinkerTTCExtensions {
    public static Atharia.Model.Void Destruct(this EmberDeepLevelLinkerTTC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static string Interact(
      this EmberDeepLevelLinkerTTC linker,
      Game game,
      Superstate superstate,
      Unit interactingUnit,
      Location containingTileLocation) {

      Asserts.Assert(game.level.units.Contains(game.player));

      // Replace this linker with a regular LevelLink.
      var nextLevelDepth = linker.nextLevelDepth;
      game.level.terrain.tiles[containingTileLocation].components.Remove(linker.AsITerrainTileComponent());
      linker.Destruct();

      MakeNextLevel(
          out var nextLevel,
          out var nextLevelSuperstate,
          out var nextLevelEntryLocation,
          game,
          superstate,
          nextLevelDepth);

      // Link to the next level.
      var levelLink = game.root.EffectLevelLinkTTCCreate(false, nextLevel, nextLevelEntryLocation);
      game.level.terrain.tiles[containingTileLocation].components.Add(levelLink.AsITerrainTileComponent());

      // Make the next level link back to here.
      var nextLevelEntryTile = nextLevel.terrain.tiles[nextLevelEntryLocation];
      nextLevelEntryTile.components.Add(
        game.root.EffectLevelLinkTTCCreate(false, game.level, containingTileLocation).AsITerrainTileComponent());

      // Travel the level link, to switch levels.
      return levelLink.Interact(game, superstate, interactingUnit, containingTileLocation);
    }

    public static void MakeNextLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocation,
        Game game,
        Superstate superstate,
        int depth) {
      game.root.logger.Info("in MakeNextLevel! depth " + depth);
      if (depth == 0) {
        // Heal the player before they go onto the first level.
        var sorcerous = game.player.components.GetOnlySorcerousUCOrNull();
        if (sorcerous.Exists()) {
          sorcerous.mp = sorcerous.maxMp;
        }
        game.player.hp = game.player.maxHp;
      }
      switch (depth) {
        case 0:
        case 2:
        case 4:
        case 6:
          CaveLevelControllerExtensions.LoadLevel(
            out level,
            out levelSuperstate,
            out entryLocation,
            game,
            superstate,
            depth);
          break;
        case 1:
          BridgesLevelControllerExtensions.LoadLevel(
            out level,
            out levelSuperstate,
            out entryLocation,
            game,
            superstate,
            depth);
          break;
        case 3:
          NestLevelControllerExtensions.LoadLevel(
            out level,
            out levelSuperstate,
            out entryLocation,
            game,
            superstate,
            depth);
          break;
        case 5:
          LakeLevelControllerExtensions.LoadLevel(
            out level,
            out levelSuperstate,
            out entryLocation,
            game,
            superstate,
            depth);
          break;
        case 7:
          AncientTownLevelControllerExtensions.LoadLevel(
            out level,
            out levelSuperstate,
            out entryLocation,
            game,
            superstate,
            depth);
          break;
        case 8:
          VolcaetusLevelControllerExtensions.LoadLevel(
            out level,
            out levelSuperstate,
            out entryLocation,
            game,
            superstate,
            depth);
          break;
        default:
          throw new Exception("Unexpected depth!");
      }
    }
  }
}
