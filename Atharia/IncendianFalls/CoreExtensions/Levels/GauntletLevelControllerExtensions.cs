using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class GauntletLevelControllerExtensions {
    public static void MakeLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        SSContext context,
        Game game,
        Superstate superstate,
        Level fromLevel,
        int depth,
        int levelIndex) {
      var terrain =
          CircleTerrainGenerator.Generate(context, PentagonPattern9.makePentagon9Pattern(), game.rand, 8.0f);

      var units = context.root.EffectUnitMutSetCreate();

      level =
          context.root.EffectLevelCreate(
              terrain, units, depth, NullILevelController.Null, game.time);
      levelSuperstate = new LevelSuperstate(level);

      game.levels.Add(level);

      GenerationCommon.PlaceRocks(context, game.rand, level, levelSuperstate);
      GenerationCommon.PlaceItems(
          context, game.rand, level, levelSuperstate, levelIndex, new Location(0, 0, 0), .2f, .2f);

      var controller = context.root.EffectGauntletLevelControllerCreate(level);
      level.controller = controller.AsILevelController();

      GenerationCommon.PlaceStaircase(terrain, new Location(0, 0, 0), false, 0, fromLevel, 0);

      for (int i = 0; i < 5; i++) {
        GenerationCommon.PlaceAvelisk(context, game, level, levelSuperstate, level, 0);
      }
      for (int i = 0; i < 8; i++) {
        GenerationCommon.PlaceNovafaire(context, game, level, levelSuperstate, level, 0);
      }
      for (int i = 0; i < 4; i++) {
        GenerationCommon.PlaceDraxling(context, game, level, levelSuperstate, level, 0);
      }
      for (int i = 0; i < 3; i++) {
        GenerationCommon.PlaceLornix(context, game, level, levelSuperstate, level, 0);
      }
      for (int i = 0; i < 3; i++) {
        GenerationCommon.PlaceYoten(context, game, level, levelSuperstate, level, 0);
      }
      for (int i = 0; i < 3; i++) {
        GenerationCommon.PlaceSpiriad(context, game, level, levelSuperstate, level, 0);
      }
      for (int i = 0; i < 4; i++) {
        GenerationCommon.PlaceMordranth(context, game, level, levelSuperstate, level, 0);
      }
    }

    //  Level level;
    //  if (squareLevelsOnly || rand.Next() % 2 == 0) {
    //    level = MakeSquareLevel(context, rand, currentTime, name);
    //  } else {
    //    level = MakePentagonalLevel(context, rand, currentTime, name);
    //  }

    //  var walkableLocations = new WalkableLocations(level.terrain, level.units);

    //  return level;
    //}

    public static string GetName(this GauntletLevelController obj) {
      return "Gauntlet Lair";
    }

    public static bool ConsiderCornersAdjacent(this GauntletLevelController obj) {
      return false;
    }

    public static Location GetEntryLocation(
        this GauntletLevelController obj,
        Game game,
        LevelSuperstate levelSuperstate,
        Level fromLevel,
        int fromLevelPortalIndex) {
      var level = obj.level;
      var terrain = level.terrain;
      if (fromLevel.depth < obj.level.depth) {
        return new Location(0, 0, 0);
      } else {
        var pattern = terrain.pattern;
        var centerLoc = new Location(0, 0, 0);
        var centerPos = pattern.GetTileCenter(centerLoc);

        var walkableLocations =
            levelSuperstate.GetWalkableLocations(terrain, true, true);

        var locationsNearStairs =
            new PatternExplorer(pattern, false, centerLoc)
                .ExploreWhile(
                    (location) => pattern.GetTileCenter(location).distance(centerPos) <= 3.0f);
        Asserts.Assert(locationsNearStairs.Count > 1);
        SetUtils.RemoveAll(walkableLocations, locationsNearStairs);

        var loc = SetUtils.GetRandomN(walkableLocations, game.rand, 3, 1)[0];
        Asserts.Assert(!terrain.tiles[loc].components.GetOnlyStaircaseTTCOrNull().Exists());
        return loc;
      }
    }

    public static Atharia.Model.Void Generate(
        this GauntletLevelController cliffLevelController,
        Game game) {
      return new Atharia.Model.Void();
    }
  }
}
