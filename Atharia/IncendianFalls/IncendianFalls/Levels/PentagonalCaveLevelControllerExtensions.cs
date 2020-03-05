using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class PentagonalCaveLevelControllerExtensions {
    public static void MakeLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocation,
        out Location exitLocation,
        Game game,
        Superstate superstate,
        int depth) {
      ForestTerrainGenerator.Generate(
          out Terrain terrain,
          out SortedDictionary<int, Room> rooms,
          game.root,
          game.rand,
          PentagonPattern9.makePentagon9Pattern(),
          1000);

      var units = game.root.EffectUnitMutSetCreate();

      level =
          game.root.EffectLevelCreate(
          new Vec3(0, -8, 16),
              terrain, units, NullILevelController.Null, game.time);
      levelSuperstate = new LevelSuperstate(level);

      game.levels.Add(level);

      var controller =
          game.root.EffectPentagonalCaveLevelControllerCreate(
              level, depth);
      level.controller = controller.AsILevelController();


      SortedSet<int> roomNumbers = new SortedSet<int>();
      foreach (var entry in rooms) {
        if (entry.Value.border != null) {
          roomNumbers.Add(entry.Key);
        }
      }
      var stairRoomNumbers = SetUtils.GetRandomN(roomNumbers, game.rand, 3, 2);

      var upStairsRoom = rooms[stairRoomNumbers[0]];
      var upStairsLoc = SetUtils.GetRandom(game.rand.Next(), upStairsRoom.floors);
      entryLocation = upStairsLoc;
      //GenerationCommon.PlaceStaircase(terrain, upStairsLoc, false, 0, levelAbove, levelAbovePortalIndex);

      var downStairsRoom = rooms[stairRoomNumbers[1]];
      var downStairsLoc = SetUtils.GetRandom(game.rand.Next(), downStairsRoom.floors);
      exitLocation = downStairsLoc;
      //GenerationCommon.PlaceStaircase(terrain, downStairsLoc, true, 1, levelBelow, levelBelowPortalIndex);

      IncendianFallsUnitsAndItems.FillWithUnits(
          game, level, levelSuperstate, depth);

      IncendianFallsUnitsAndItems.PlaceItems(
          game.rand, level, levelSuperstate, depth, upStairsLoc, .02f, .02f);
    }

    public static string GetName(this PentagonalCaveLevelController obj) {
      return "Cave" + obj.depth;
    }

    public static bool ConsiderCornersAdjacent(this PentagonalCaveLevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this PentagonalCaveLevelController obj,
        Game game,
        Superstate superstate,
        string triggerName) {
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this PentagonalCaveLevelController obj,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      return new Atharia.Model.Void();
    }

    //public static Location GetEntryLocation(
    //    this PentagonalCaveLevelController obj,
    //    Game game,
    //    LevelSuperstate levelSuperstate,
    //    Level fromLevel, int fromLevelPortalIndex) {
    //  foreach (var locationAndTile in obj.level.terrain.tiles) {
    //    var staircase = locationAndTile.Value.components.GetOnlyStaircaseTTCOrNull();
    //    if (staircase.Exists()) {
    //      if (staircase.destinationLevel.NullableIs(fromLevel) &&
    //          staircase.destinationLevelPortalIndex == fromLevelPortalIndex) {
    //        return locationAndTile.Key;
    //      }
    //    }
    //  }
    //  if (!fromLevel.NullableIs(obj.level)) {
    //    game.root.logger.Error("Couldnt figure out where to place unit!");
    //  }
    //  return levelSuperstate.GetNRandomWalkableLocations(
    //      obj.level.terrain, game.rand, 1, true, true)[0];
    //}

    public static Atharia.Model.Void Generate(
        this PentagonalCaveLevelController cliffLevelController,
        Game game) {
      return new Atharia.Model.Void();
    }
  }
}
