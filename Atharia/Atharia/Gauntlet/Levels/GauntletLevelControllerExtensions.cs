using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class GauntletLevelControllerExtensions {
    public static Atharia.Model.Void Destruct(this GauntletLevelController self) {
      self.Delete();
      return new Atharia.Model.Void();
    }

    public static void MakeLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocation,
        SSContext context,
        Game game,
        bool squareLevelsOnly) {
      var terrain =
          CircleTerrainGenerator.Generate(
            context,
            game.root,
            squareLevelsOnly ? SquarePattern.MakeSquarePattern() : PentagonPattern9.makePentagon9Pattern(),
            false,
            game.rand,
            8.0f);

      var units = game.root.EffectUnitMutSetCreate();

      level =
          game.root.EffectLevelCreate(
          new Vec3(0, 0, 20),
              terrain, units, NullILevelController.Null, game.time);
      levelSuperstate = new LevelSuperstate(level);

      IncendianFallsUnitsAndItems.PlaceRocks(game.rand, level, levelSuperstate);
      IncendianFallsUnitsAndItems.PlaceItems(
          game.rand, level, levelSuperstate, 0, new Location(0, 0, 0), .2f, .2f);

      var controller = game.root.EffectGauntletLevelControllerCreate(level);
      level.controller = controller.AsILevelController();

      entryLocation = new Location(0, 0, 0);

      for (int i = 0; i < 5; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => true, true, true)[0],
          level.time,
          Avelisk.Make(level.root));
      }
      for (int i = 0; i < 8; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => true, true, true)[0],
          level.time,
          Novafaire.Make(level.root));
      }
      for (int i = 0; i < 4; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => true, true, true)[0],
          level.time,
          Draxling.Make(level.root));
      }
      for (int i = 0; i < 3; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => true, true, true)[0],
          level.time,
          Lornix.Make(level.root));
      }
      for (int i = 0; i < 3; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => true, true, true)[0],
          level.time,
          Yoten.Make(level.root));
      }
      for (int i = 0; i < 3; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => true, true, true)[0],
          level.time,
          Spiriad.Make(level.root));
      }
      for (int i = 0; i < 4; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => true, true, true)[0],
          level.time,
          Mordranth.Make(level.root));
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
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        string triggerName) {
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this GauntletLevelController obj,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      return new Atharia.Model.Void();
    }
  }
}
