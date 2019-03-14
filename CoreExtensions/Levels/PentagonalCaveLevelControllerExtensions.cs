﻿using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class PentagonalCaveLevelControllerExtensions {
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
        int levelIndex,
        int depth) {
      ForestTerrainGenerator.Generate(
          out Terrain terrain,
          out SortedDictionary<int, Room> rooms,
          context,
          game.rand,
          PentagonPattern9.makePentagon9Pattern(),
          1000);
          //1000);

      var units = context.root.EffectUnitMutSetCreate();

      level =
          context.root.EffectLevelCreate(
              terrain, units, depth, NullILevelController.Null, game.time);
      levelSuperstate = new LevelSuperstate(level);

      game.levels.Add(level);

      var controller =
          context.root.EffectPentagonalCaveLevelControllerCreate(
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
      GenerationCommon.PlaceStaircase(terrain, upStairsLoc, false, 0, levelAbove, levelAbovePortalIndex);

      var downStairsRoom = rooms[stairRoomNumbers[1]];
      var downStairsLoc = SetUtils.GetRandom(game.rand.Next(), downStairsRoom.floors);
      GenerationCommon.PlaceStaircase(terrain, downStairsLoc, true, 1, levelBelow, levelBelowPortalIndex);

      GenerationCommon.FillWithUnits(
          context, game, level, levelSuperstate, levelIndex);

      GenerationCommon.PlaceItems(
          context, game.rand, level, levelSuperstate, levelIndex, upStairsLoc, .02f, .02f);
    }

    public static string GetName(this PentagonalCaveLevelController obj) {
      return "Cave" + obj.depth;
    }

    public static bool ConsiderCornersAdjacent(this PentagonalCaveLevelController obj) {
      return false;
    }

    public static Location GetEntryLocation(
        this PentagonalCaveLevelController obj,
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
          obj.level.terrain, game.rand, 1, true, true)[0];
    }

    public static Atharia.Model.Void Generate(
        this PentagonalCaveLevelController cliffLevelController,
        Game game) {
      return new Atharia.Model.Void();
    }
  }
}