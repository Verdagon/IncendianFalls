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
      var thisLevelDepth = linker.thisLevelDepth;
      game.level.terrain.tiles[containingTileLocation].components.Remove(linker.AsITerrainTileComponent());
      linker.Destruct();

      MakeNextLevel(
          out var nextLevel,
          out var nextLevelSuperstate,
          out var nextLevelEntryLocation,
          game,
          superstate,
          thisLevelDepth + 1);

      // Link to the next level.
      var levelLink = game.root.EffectLevelLinkTTCCreate(nextLevel, nextLevelEntryLocation);
      game.level.terrain.tiles[containingTileLocation].components.Add(levelLink.AsITerrainTileComponent());

      // Make the next level link back to here.
      var nextLevelEntryTile = nextLevel.terrain.tiles[nextLevelEntryLocation];
      nextLevelEntryTile.components.Add(
        game.root.EffectLevelLinkTTCCreate(game.level, containingTileLocation).AsITerrainTileComponent());

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
        game.root.logger.Info("making nest!");
        NestLevelControllerExtensions.LoadLevel(
          out level,
          out levelSuperstate,
          out entryLocation,
          game,
          superstate,
          depth);
      } else if (depth == 2) {
        IslandLevelControllerExtensions.LoadLevel(
          out level,
          out levelSuperstate,
          out entryLocation,
          out var exitLocation,
          game,
          superstate,
          depth);
      } else {
        CaveLevelControllerExtensions.LoadLevel(
          out level,
          out levelSuperstate,
          out entryLocation,
          game,
          superstate,
          depth);
      }
    }
  }
}
