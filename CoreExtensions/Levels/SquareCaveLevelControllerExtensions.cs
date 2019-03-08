using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class SquareCaveLevelControllerExtensions {
    public static void MakeLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        SSContext context,
        Game game,
        Superstate superstate,
        Level levelAbove,
        int levelAbovePortalIndex,
        Level levelBelow,
        int levelBelowPortalIndex,
        int depth) {
      DungeonTerrainGenerator.Generate(
          out Terrain terrain,
          out SortedDictionary<int, DungeonTerrainGenerator.Room> rooms,
          context,
          80,
          20,
          //30,
          //20,
          game.rand);

      var units = context.root.EffectUnitMutSetCreate();

      level =
          context.root.EffectLevelCreate(
              terrain, units, depth, NullILevelController.Null);
      levelSuperstate = new LevelSuperstate(level);

      var controller =
          context.root.EffectSquareCaveLevelControllerCreate(
              level, depth);
      level.controller = controller.AsILevelController();

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
        var stairsLocs = SetUtils.GetRandomN(game.rand, locs, 2);

        upStairsLoc = stairsLocs[0];
        GenerationCommon.PlaceStaircase(terrain, upStairsLoc, false, 0, levelAbove, levelAbovePortalIndex);

        downStairsLoc = stairsLocs[1];
        GenerationCommon.PlaceStaircase(terrain, downStairsLoc, true, 1, levelBelow, levelBelowPortalIndex);
      } else {
        var stairRoomNumbers = SetUtils.GetRandomN(game.rand, roomNumbers, 2);

        Asserts.Assert(stairRoomNumbers[0] != stairRoomNumbers[1]);

        var upStairsRoom = rooms[stairRoomNumbers[0]];
        upStairsLoc = RandomLocationInRoom(game.rand, upStairsRoom);
        GenerationCommon.PlaceStaircase(terrain, upStairsLoc, false, 0, levelAbove, levelAbovePortalIndex);

        var downStairsRoom = rooms[stairRoomNumbers[1]];
        downStairsLoc = RandomLocationInRoom(game.rand, downStairsRoom);
        GenerationCommon.PlaceStaircase(terrain, downStairsLoc, true, 1, levelBelow, levelBelowPortalIndex);
      }

      Asserts.Assert(upStairsLoc != downStairsLoc);

      GenerationCommon.FillWithUnits(
          context, game, level, levelSuperstate, depth, true);
    }

    public static Location RandomLocationInRoom(Rand rand, DungeonTerrainGenerator.Room room) {
      Asserts.Assert(room.size.row > 2);
      Asserts.Assert(room.size.col > 2);
      var rowInRoom = rand.Next() % (room.size.row - 2);
      var colInRoom = rand.Next() % (room.size.row - 2);
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

    public static Location GetEntryLocation(
        this SquareCaveLevelController obj,
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
      var forbiddenLocations = new SortedSet<Location>();
      foreach (var locationAndTile in obj.level.terrain.tiles) {
        var staircase = locationAndTile.Value.components.GetOnlyStaircaseTTCOrNull();
        if (staircase.Exists()) {
          forbiddenLocations.Add(locationAndTile.Key);
        }
      }
      return levelSuperstate.GetNRandomWalkableLocations(
          game.rand, 1, forbiddenLocations, true)[0];
    }

    public static Atharia.Model.Void Generate(
        this SquareCaveLevelController cliffLevelController,
        Game game) {
      return new Atharia.Model.Void();
    }
  }
}
