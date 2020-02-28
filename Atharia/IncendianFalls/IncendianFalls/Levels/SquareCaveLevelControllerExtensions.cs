using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class SquareCaveLevelControllerExtensions {
    public static void MakeLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocation,
        out Location exitLocation,
        Game game,
        Superstate superstate,
        //int levelIndex,
        int depth) {
      DungeonTerrainGenerator.Generate(
          out Terrain terrain,
          out SortedDictionary<int, DungeonTerrainGenerator.Room> rooms,
          game.root,
          40,
          20,
          //30,
          //20,
          game.rand);

      var units = game.root.EffectUnitMutSetCreate();

      level =
          game.root.EffectLevelCreate(
              terrain, units, NullILevelController.Null, game.time);
      levelSuperstate = new LevelSuperstate(level);

      var controller =
          game.root.EffectSquareCaveLevelControllerCreate(
              level, depth);
      level.controller = controller.AsILevelController();

      game.levels.Add(level);

      SortedSet<int> roomNumbers = new SortedSet<int>();
      foreach (var entry in rooms) {
        if (!entry.Value.isCorridor) {
          roomNumbers.Add(entry.Key);
        }
      }

      Asserts.Assert(roomNumbers.Count > 0);
      Location upStairsLoc;
      Location downStairsLoc;
      if (roomNumbers.Count == 1) {
        var room = rooms[0];

        SortedSet<Location> locs = new SortedSet<Location>();
        for (int row = 0; row < room.size.row; row++) {
          for (int col = 0; col < room.size.col; col++) {
            locs.Add(new Location(room.origin.col + col, room.origin.row + row, 0));
          }
        }
        var stairsLocs =
            ListUtils.GetRandomN(new List<Location>(locs), game.rand, 3, 2);

        entryLocation = stairsLocs[0];

        exitLocation = stairsLocs[1];

        Asserts.Assert(entryLocation != exitLocation);
      } else {
        var stairRoomNumbers =
            ListUtils.GetRandomN(new List<int>(roomNumbers), game.rand, 3, 2);

        Asserts.Assert(stairRoomNumbers[0] != stairRoomNumbers[1]);

        var upStairsRoom = rooms[stairRoomNumbers[0]];
        entryLocation = RandomLocationInRoom(game.rand, upStairsRoom);

        var downStairsRoom = rooms[stairRoomNumbers[1]];
        exitLocation = RandomLocationInRoom(game.rand, downStairsRoom);

        Asserts.Assert(entryLocation != exitLocation);
      }

      GenerationCommon.FillWithUnits(
          game, level, levelSuperstate, depth);

      GenerationCommon.PlaceItems(
          game.rand, level, levelSuperstate, depth, entryLocation, .02f, .02f);
    }

    public static Location RandomLocationInRoom(Rand rand, DungeonTerrainGenerator.Room room) {
      Asserts.Assert(room.size.row > 2);
      Asserts.Assert(room.size.col > 2);
      var rowInRoom = rand.Next() % (room.size.row - 2);
      var colInRoom = rand.Next() % (room.size.col - 2);
      return new Location(
          room.origin.col + colInRoom,
          room.origin.row + rowInRoom,
          0);
    }

    public static string GetName(this SquareCaveLevelController obj) {
      return "Cave" + obj.depth;
    }

    public static bool ConsiderCornersAdjacent(this SquareCaveLevelController obj) {
      return true;
    }

    //public static Location GetEntryLocation(
    //    this SquareCaveLevelController obj,
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
    //    obj.level.terrain,  
    //    game.rand, 1, true, true)[0];
    //}

    public static Atharia.Model.Void Generate(
        this SquareCaveLevelController cliffLevelController,
        Game game) {
      return new Atharia.Model.Void();
    }
  }
}
