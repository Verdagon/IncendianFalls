using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class GauntletLevelControllerExtensions {
    public static void MakeLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocation,
        Game game) {
      var terrain =
          CircleTerrainGenerator.Generate(game.root, PentagonPattern9.makePentagon9Pattern(), game.rand, 8.0f);

      var units = game.root.EffectUnitMutSetCreate();

      level =
          game.root.EffectLevelCreate(
              terrain, units, NullILevelController.Null, game.time);
      levelSuperstate = new LevelSuperstate(level);

      GenerationCommon.PlaceRocks(game.rand, level, levelSuperstate);
      GenerationCommon.PlaceItems(
          game.rand, level, levelSuperstate, 0, new Location(0, 0, 0), .2f, .2f);

      var controller = game.root.EffectGauntletLevelControllerCreate(level);
      level.controller = controller.AsILevelController();

      entryLocation = new Location(0, 0, 0);

      for (int i = 0; i < 5; i++) {
        GenerationCommon.PlaceAvelisk(game, level, levelSuperstate);
      }
      for (int i = 0; i < 8; i++) {
        GenerationCommon.PlaceNovafaire(game, level, levelSuperstate);
      }
      for (int i = 0; i < 4; i++) {
        GenerationCommon.PlaceDraxling(game, level, levelSuperstate);
      }
      for (int i = 0; i < 3; i++) {
        GenerationCommon.PlaceLornix(game, level, levelSuperstate);
      }
      for (int i = 0; i < 3; i++) {
        GenerationCommon.PlaceYoten(game, level, levelSuperstate);
      }
      for (int i = 0; i < 3; i++) {
        GenerationCommon.PlaceSpiriad(game, level, levelSuperstate);
      }
      for (int i = 0; i < 4; i++) {
        GenerationCommon.PlaceMordranth(game, level, levelSuperstate);
      }
    }

    //  Level level;
    //  if (squareLevelsOnly || rand.Next() % 2 == 0) {
    //    level = MakeSquareLevel(rand, currentTime, name);
    //  } else {
    //    level = MakePentagonalLevel(rand, currentTime, name);
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

    //public static Location GetEntryLocation(
    //    this GauntletLevelController obj,
    //    Game game,
    //    LevelSuperstate levelSuperstate,
    //    Level fromLevel,
    //    int fromLevelPortalIndex) {
    //  var level = obj.level;
    //  var terrain = level.terrain;
    //  if (fromLevel.depth < obj.level.depth) {
    //    return new Location(0, 0, 0);
    //  } else {
    //    var pattern = terrain.pattern;
    //    var centerLoc = new Location(0, 0, 0);
    //    var centerPos = pattern.GetTileCenter(centerLoc);

    //    var walkableLocations =
    //        levelSuperstate.GetWalkableLocations(terrain, true, true);

    //    var locationsNearStairs =
    //        new PatternExplorer(pattern, false, centerLoc)
    //            .ExploreWhile(
    //                (location) => pattern.GetTileCenter(location).distance(centerPos) <= 3.0f);
    //    Asserts.Assert(locationsNearStairs.Count > 1);
    //    SetUtils.RemoveAll(walkableLocations, locationsNearStairs);

    //    var loc = SetUtils.GetRandomN(walkableLocations, game.rand, 3, 1)[0];
    //    Asserts.Assert(!terrain.tiles[loc].components.GetOnlyLevelLinkTTCOrNull().Exists());
    //    return loc;
    //  }
    //}

    public static Atharia.Model.Void SimpleTrigger(
        this GauntletLevelController obj,
        Game game,
        Superstate superstate,
        string triggerName) {
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this GauntletLevelController obj,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      return new Atharia.Model.Void();
    }
  }
}
