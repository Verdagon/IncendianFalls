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
      bool considerCornersAdjacent = true;

      var terrain = CellularAutomataTerrainGenerator.Generate(game.root, PentagonPattern9.makePentagon9Pattern(), game.rand, considerCornersAdjacent, 20.0f);
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
              terrain, units, NullILevelController.Null, game.time);
      levelSuperstate = new LevelSuperstate(level);

      game.levels.Add(level);

      level.controller = game.root.EffectCaveLevelControllerCreate(level).AsILevelController();
      
      entryLocation = new Location(0, 0, 0);
      foreach (var locationAndTile in terrain.tiles) {
        if (locationAndTile.Value.IsWalkable()) {
          entryLocation = locationAndTile.Key;
          break;
        }
      }
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
