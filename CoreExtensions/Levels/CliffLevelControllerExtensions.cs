using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class CliffLevelControllerExtensions {
    public static void MakeLevel(
        out Level cliffLevel,
        out LevelSuperstate levelSuperstate,
        SSContext context,
        Game game,
        Superstate superstate,
        Level levelAbove,
        int depth) {

      bool waterfallTopLeftToBottomRight = game.rand.Next() % 2 == 0;
      CliffTerrainGenerator.GenerateWithWaterfall(
          out Terrain terrain,
          out List<CliffTerrainGenerator.CliffHalf> cliffHalves,
          out SortedDictionary<Location, int> preRandifiedElevationByLocation,
          context,
          game.rand,
          PentagonPattern9.makePentagon9Pattern(),
          1000,
          //300,
          waterfallTopLeftToBottomRight);

      var units = context.root.EffectUnitMutSetCreate();

      cliffLevel =
          context.root.EffectLevelCreate(
              terrain, units, depth, NullILevelController.Null);
      levelSuperstate = new LevelSuperstate(cliffLevel);

      var controller =
          context.root.EffectCliffLevelControllerCreate(
              cliffLevel, depth);
      cliffLevel.controller = controller.AsILevelController();

      Level caveLevel;
      LevelSuperstate caveLevelSuperstate;
      if (depth % 2 == 1) {
        SquareCaveLevelControllerExtensions.MakeLevel(
            out caveLevel,
            out caveLevelSuperstate,
            context,
            game,
            superstate,
            cliffLevel,
            2,
            cliffLevel,
            3,
            depth);
      } else {
        PentagonalCaveLevelControllerExtensions.MakeLevel(
            out caveLevel,
            out caveLevelSuperstate,
            context,
            game,
            superstate,
            cliffLevel,
            2,
            cliffLevel,
            3,
            depth);
      }

      //if (cliffHalves.Count == 2) {
        Location upStaircaseLocation =
            GenerationCommon.GetFurthestLocationInDirection(
                terrain.pattern,
                cliffHalves[0].walkableLocs,
                new Vec2(0, 1));
        GenerationCommon.PlaceStaircase(
            terrain, upStaircaseLocation, false, 0, levelAbove, 1);

        Location downStaircaseLocation =
            GenerationCommon.GetFurthestLocationInDirection(
                terrain.pattern,
                cliffHalves[1].walkableLocs,
                new Vec2(0, -1));
        GenerationCommon.PlaceStaircase(
            terrain, downStaircaseLocation, true, 1, Level.Null, 0);

        PlaceCave(
            out Location highHalfCaveLocation,
            out SortedSet<Location> highHalfCaveRoomBorder,
            terrain, game.rand, cliffHalves[0].rooms, 2, caveLevel, 0);

        PlaceCave(
            out Location lowHalfCaveLocation,
            out SortedSet<Location> lowHalfCaveRoomBorder,
            terrain, game.rand, cliffHalves[1].rooms, 3, caveLevel, 1);

        // IMPORTANT NOTE
        // Sometimes, a landing can be in another landing's border!!
        // They can be close enough for that to happen.
        // Remember, spaces in a room's border needn't even touch the room!

        if (!CanReachLimited(cliffLevel, upStaircaseLocation, highHalfCaveLocation)) {
          ResetCliffs(terrain, preRandifiedElevationByLocation, highHalfCaveRoomBorder);
          var arbitraryBorderLoc = GetArbitraryCliffIn(terrain, highHalfCaveRoomBorder);
          // Find a path that's not limited by hopping
          var upperHalfPath = GetUnlimitedPathAlongBorder(cliffLevel, upStaircaseLocation, arbitraryBorderLoc);
          Asserts.Assert(upperHalfPath.Count > 0);
          upperHalfPath.Insert(0, upStaircaseLocation);
          ResetCliffs(terrain, preRandifiedElevationByLocation, new SortedSet<Location>(upperHalfPath));
          Asserts.Assert(CanReachLimited(cliffLevel, upStaircaseLocation, highHalfCaveLocation));
        }

        if (!CanReachLimited(cliffLevel, downStaircaseLocation, lowHalfCaveLocation)) {
          ResetCliffs(terrain, preRandifiedElevationByLocation, lowHalfCaveRoomBorder);
          var arbitraryBorderLoc = GetArbitraryCliffIn(terrain, lowHalfCaveRoomBorder);
          // Find a path that's not limited by hopping
          var lowerHalfPath = GetUnlimitedPathAlongBorder(cliffLevel, downStaircaseLocation, arbitraryBorderLoc);
          if (lowerHalfPath.Count > 0) {
            lowerHalfPath.Insert(0, upStaircaseLocation);
            ResetCliffs(terrain, preRandifiedElevationByLocation, new SortedSet<Location>(lowerHalfPath));
            Asserts.Assert(CanReachLimited(cliffLevel, downStaircaseLocation, lowHalfCaveLocation));
          } else {
            context.logger.Error("Can't find path!");

            GetUnlimitedPathAlongBorder(
                cliffLevel, downStaircaseLocation, arbitraryBorderLoc);
            //Asserts.Assert(lowerHalfPath.Count > 0);
          }
        }

        GenerationCommon.FillWithUnits(
            context, game, cliffLevel, levelSuperstate, depth, false);
      //}
    }

    private static Location GetArbitraryCliffIn(Terrain terrain, SortedSet<Location> borderLocations) {
      foreach (var x in borderLocations) {
        if (terrain.tiles[x].classId == "cliff") {
          return x;
        }
      }
      throw new Exception("No cliffs in border?"); // Impossible I think
    }

    private static void ResetCliffs(
        Terrain terrain,
        SortedDictionary<Location, int> preRandifiedElevationByLocation,
        SortedSet<Location> locs) {
      foreach (var step in locs) {
        if (terrain.tiles[step].classId == "cliff") {
          terrain.tiles[step].elevation = preRandifiedElevationByLocation[step];
        }
      }
    }

    private static bool CanReachLimited(
        Level level,
        Location from,
        Location to) {
      var upperHalfPathWithLimitedHopping =
          AStarExplorer.Go(
              level.terrain,
              from,
              to,
              level.ConsiderCornersAdjacent(),
              true,
              "");
      return upperHalfPathWithLimitedHopping.Count > 0;
    }

    private static List<Location> GetUnlimitedPathAlongBorder(
        Level level, Location from, Location to) {
      return AStarExplorer.Go(
          level.terrain,
          from,
          to,
          level.ConsiderCornersAdjacent(),
          false,
          "cliff");
    }

    private static void PlaceCave(
        out Location caveLocation,
        out SortedSet<Location> caveRoomBorder,
        Terrain terrain,
        Rand rand,
        SortedDictionary<int, Room> rooms,
        int portalIndex,
        Level destinationLevel,
        int destinationLevelPortalIndex) {
      var roomNumCandidates = new SortedSet<int>();
      foreach (var entry in rooms) {
        if (entry.Value.border != null) {
          roomNumCandidates.Add(entry.Key);
        }
      }
      var randomRoomNum = SetUtils.GetRandom(rand.Next(), roomNumCandidates);
      var randomRoom = rooms[randomRoomNum];
      var highestSpaceInLowHalfRoom =
          GenerationCommon.GetFurthestLocationInDirection(
              terrain.pattern, randomRoom.floors, new Vec2(0, 1));
      var caveTile = terrain.tiles[highestSpaceInLowHalfRoom];
      caveTile.components.Add(
          new StaircaseTTCAsITerrainTileComponent(
              terrain.root.EffectStaircaseTTCCreate(
                  portalIndex, destinationLevel, destinationLevelPortalIndex)));
      caveTile.components.Add(
          new DecorativeTTCAsITerrainTileComponent(
              terrain.root.EffectDecorativeTTCCreate("cave")));
      caveLocation = highestSpaceInLowHalfRoom;
      caveRoomBorder = randomRoom.border;
    }

    public static string GetName(this CliffLevelController obj) {
      return "Cliff" + obj.depth;
    }

    public static bool ConsiderCornersAdjacent(this CliffLevelController obj) {
      return false;
    }

    public static Location GetEntryLocation(
        this CliffLevelController obj,
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
        this CliffLevelController cliffLevelController,
        Game game) {
      return new Atharia.Model.Void();
    }
  }
}
