using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class RidgeLevelControllerExtensions {
    public static void MakeLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        SSContext context,
        Game game,
        Superstate superstate,
        int depth) {
      ForestTerrainGenerator.Generate(
          out Terrain terrain,
          out SortedDictionary<int, Room> rooms,
          context,
          game.rand,
          PentagonPattern9.makePentagon9Pattern(),
          1000);

      var units = context.root.EffectUnitMutSetCreate();

      level =
          context.root.EffectLevelCreate(
              terrain, units, depth, NullILevelController.Null, game.time);
      levelSuperstate = new LevelSuperstate(level);

      var controller = context.root.EffectRidgeLevelControllerCreate(level);
      level.controller = controller.AsILevelController();
    }

    public static string GetName(this RidgeLevelController obj) {
      return "Ridge";
    }

    public static bool ConsiderCornersAdjacent(this RidgeLevelController obj) {
      return false;
    }

    public static Location GetEntryLocation(
        this RidgeLevelController obj,
        Game game,
        LevelSuperstate levelSuperstate,
        Level fromLevel, int fromLevelPortalIndex) {
      foreach (var locationAndTile in obj.level.terrain.tiles) {
        var staircase = locationAndTile.Value.components.GetOnlyStaircaseTTCOrNull();
        if (staircase.Exists()) {
          if (staircase.destinationLevel.NullableIs(fromLevel) &&
              staircase.destinationLevelPortalIndex == fromLevelPortalIndex) {
            return locationAndTile.Key;
          }
        }
      }
      if (!fromLevel.NullableIs(obj.level)) {
        game.root.logger.Error("Couldnt figure out where to place unit!");
      }
      return levelSuperstate.GetNRandomWalkableLocations(
          obj.level.terrain,
          game.rand, 1, true, true)[0];
    }

    public static Atharia.Model.Void Generate(
        this RidgeLevelController cliffLevelController,
        Game game) {
      return new Atharia.Model.Void();
    }
  }
}
