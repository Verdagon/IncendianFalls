using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  class GenerateCliffLevel {

    public static void MakeLevel(
        out Level cliffLevel,
        out LevelSuperstate levelSuperstate,
        out Location entryLocation,
        Game game,
        Superstate superstate,
        //Level levelAbove,
        //Location levelAboveDownStaircaseLocation,
        //int levelIndex,
        int depth) {

      bool waterfallTopLeftToBottomRight = game.rand.Next() % 2 == 0;
      CliffTerrainGenerator.GenerateWithWaterfall(
          out Terrain terrain,
          out List<CliffTerrainGenerator.CliffHalf> cliffHalves,
          out SortedDictionary<Location, int> preRandifiedElevationByLocation,
          game.root,
          game.rand,
          PentagonPattern9.makePentagon9Pattern(),
          1000,
          //300,
          waterfallTopLeftToBottomRight);


      var units = game.root.EffectUnitMutSetCreate();

      cliffLevel =
          game.root.EffectLevelCreate(
              terrain, units, NullILevelController.Null, game.time);
      levelSuperstate = new LevelSuperstate(cliffLevel);

      game.levels.Add(cliffLevel);

      var controller =
          game.root.EffectCliffLevelControllerCreate(
              cliffLevel, depth);
      cliffLevel.controller = controller.AsILevelController();

      Location caveLevelEntryLocation;
      Location caveLevelExitLocation;
      Level caveLevel;
      LevelSuperstate caveLevelSuperstate;
      if (depth % 2 == 1) {
        SquareCaveLevelControllerExtensions.MakeLevel(
            out caveLevel,
            out caveLevelSuperstate,
            out caveLevelEntryLocation,
            out caveLevelExitLocation,
            game,
            superstate,
            //cliffLevel,
            //2,
            //cliffLevel,
            //3,
            //levelIndex + 1,
            depth);
      } else {
        PentagonalCaveLevelControllerExtensions.MakeLevel(
            out caveLevel,
            out caveLevelSuperstate,
            out caveLevelEntryLocation,
            out caveLevelExitLocation,
            game,
            superstate,
            //cliffLevel,
            //2,
            //cliffLevel,
            //3,
            //levelIndex + 1,
            depth);
      }

      //if (cliffHalves.Count == 2) {
      Location upStaircaseLocation =
            GenerationCommon.GetFurthestLocationInDirection(
                terrain.pattern,
                cliffHalves[0].walkableLocs,
                new Vec2(0, 1));

      entryLocation = upStaircaseLocation;

      var upStaircaseTile = terrain.tiles[upStaircaseLocation];
      upStaircaseTile.components.Add(
        terrain.root.EffectUpStairsTTCCreate().AsITerrainTileComponent());

      Location downStaircaseLocation =
          GenerationCommon.GetFurthestLocationInDirection(
              terrain.pattern,
              cliffHalves[1].walkableLocs,
              new Vec2(0, -1));
      var downStaircaseTile = terrain.tiles[downStaircaseLocation];
      downStaircaseTile.components.Add(
        terrain.root.EffectDownStairsTTCCreate().AsITerrainTileComponent());
      downStaircaseTile.components.Add(
        terrain.root.EffectIncendianFallsLevelLinkerTTCCreate(depth).AsITerrainTileComponent());


      PlaceCave(
          out Location highHalfCaveLocation,
          out SortedSet<Location> highHalfCaveRoomBorder,
          terrain, game.rand, cliffHalves[0].rooms, caveLevel, caveLevelEntryLocation);

      PlaceCave(
          out Location lowHalfCaveLocation,
          out SortedSet<Location> lowHalfCaveRoomBorder,
          terrain, game.rand, cliffHalves[1].rooms, caveLevel, caveLevelExitLocation);

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
        Asserts.Assert(lowerHalfPath.Count > 0);
        lowerHalfPath.Insert(0, upStaircaseLocation);
        ResetCliffs(terrain, preRandifiedElevationByLocation, new SortedSet<Location>(lowerHalfPath));
        Asserts.Assert(CanReachLimited(cliffLevel, downStaircaseLocation, lowHalfCaveLocation));
      }

      GenerationCommon.PlaceItems(
          game.rand, cliffLevel, levelSuperstate, depth, upStaircaseLocation, .02f, .02f);

      GenerationCommon.FillWithUnits(
          game, cliffLevel, levelSuperstate, depth);

      //}
    }

    private static Location GetArbitraryCliffIn(Terrain terrain, SortedSet<Location> borderLocations) {
      foreach (var x in borderLocations) {
        if (terrain.tiles[x].components.GetAllCliffTTC().Count > 0) {
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
        if (terrain.tiles[step].components.GetAllCliffTTC().Count > 0) {
          terrain.tiles[step].elevation = preRandifiedElevationByLocation[step];
        }
      }
    }

    private static bool CanReachLimited(
        Level level,
        Location origin,
        Location destination) {
      var terrain = level.terrain;
      var upperHalfPathWithLimitedHopping =
          AStarExplorer.Go(
              terrain.pattern,
              origin,
              destination,
              level.ConsiderCornersAdjacent(),
              (Location from, Location to) => {
                return terrain.tiles.ContainsKey(to) &&
                    terrain.tiles[to].IsWalkable() &&
                    terrain.GetElevationDifference(from, to) <= 2;
              });

      return upperHalfPathWithLimitedHopping.Count > 0;
    }

    private static List<Location> GetUnlimitedPathAlongBorder(
        Level level, Location origin, Location destination) {
      return AStarExplorer.Go(
          level.terrain.pattern,
          origin,
          destination,
          level.ConsiderCornersAdjacent(),
              (Location from, Location to) => {
                return level.terrain.tiles.ContainsKey(to) &&
                    level.terrain.tiles[to].IsWalkable() &&
                    level.terrain.tiles[to].components.GetAllCliffTTC().Count > 0;
              });
    }

    private static void PlaceCave(
        out Location caveLocation,
        out SortedSet<Location> caveRoomBorder,
        Terrain terrain,
        Rand rand,
        SortedDictionary<int, Room> rooms,
        Level caveLevel,
        Location caveLevelEntry) {
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
          terrain.root.EffectLevelLinkTTCCreate(caveLevel, caveLevelEntry)
          .AsITerrainTileComponent());

      caveTile.components.Add(terrain.root.EffectCaveTTCCreate().AsITerrainTileComponent());
      caveLocation = highestSpaceInLowHalfRoom;
      caveRoomBorder = randomRoom.border;
    }

    //public static Location GetEntryLocation(
    //    this CliffLevelController obj,
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
  }
}
