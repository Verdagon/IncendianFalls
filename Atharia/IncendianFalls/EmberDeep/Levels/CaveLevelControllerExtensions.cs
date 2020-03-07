using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class CaveLevelControllerExtensions {
    public static void LoadLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocation,
        Game game,
        Superstate superstate,
        int depth) {
      bool considerCornersAdjacent = false;

      var terrain = CellularAutomataTerrainGenerator.Generate(game.root, PentagonPattern9.makePentagon9Pattern(), game.rand, considerCornersAdjacent, 15.0f);
      foreach (var locationAndTile in terrain.tiles) {
        locationAndTile.Value.components.Add(game.root.EffectMudTTCCreate().AsITerrainTileComponent());
      }

      var locations = new SortedSet<Location>();
      foreach (var locationAndTile in terrain.tiles) {
        locations.Add(locationAndTile.Key);
      }
      var borderLocations = terrain.pattern.GetAdjacentLocations(locations, false, true);
      foreach (var borderLocation in borderLocations) {
        if (!terrain.tiles.ContainsKey(borderLocation)) {
          var tile = game.root.EffectTerrainTileCreate(3, ITerrainTileComponentMutBunch.New(game.root));
          tile.components.Add(game.root.EffectCaveWallTTCCreate().AsITerrainTileComponent());
          terrain.tiles.Add(borderLocation, tile);
        }
      }

      var units = game.root.EffectUnitMutSetCreate();

      level =
          game.root.EffectLevelCreate(
          new Vec3(0, -8, 16),
              terrain,
              units,
              NullILevelController.Null,
              game.time);
      levelSuperstate = new LevelSuperstate(level);

      var entryAndExitLocations = levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 2, false, false);
      entryLocation = entryAndExitLocations[0];
      var exitLocation = entryAndExitLocations[1];
      level.terrain.tiles[exitLocation].components.Add(
        game.root.EffectEmberDeepLevelLinkerTTCCreate(depth + 1).AsITerrainTileComponent());

      level.controller = game.root.EffectCaveLevelControllerCreate(level).AsILevelController();

      game.levels.Add(level);
    }

    public static string GetName(this CaveLevelController obj) {
      return "Cave";
    }

    public static bool ConsiderCornersAdjacent(this CaveLevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this CaveLevelController obj,
        Game game,
        Superstate superstate,
        string triggerName) {
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this CaveLevelController obj,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      return new Atharia.Model.Void();
    }
  }
}
