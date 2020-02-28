using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class CliffTerrainGenerator {
    public class CliffHalf {
      public SortedSet<Location> walkableLocs;
      public SortedDictionary<int, Room> rooms;
      public SortedSet<Location> roomLocs;

      public CliffHalf(
          SortedSet<Location> walkableLocs,
          SortedDictionary<int, Room> rooms) {
        this.walkableLocs = walkableLocs;
        this.rooms = rooms;
      }
    }

    private class RoomMappingThing {
      Pattern pattern;

      public SortedSet<Location> unusedLocations;
      SortedSet<Location> roomLocations =
          new SortedSet<Location>();

      public SortedDictionary<int, Room> roomByNumber =
          new SortedDictionary<int, Room>();
      public SortedDictionary<int, SortedSet<Location>> locsByDistFromNearestRoomOrEdge =
          new SortedDictionary<int, SortedSet<Location>>();
      public SortedDictionary<Location, int> distFromNearestRoomOrEdgeByLocation =
          new SortedDictionary<Location, int>();

      public RoomMappingThing(
          Pattern pattern,
          SortedSet<Location> unusedLocations) {
        this.pattern = pattern;
        this.unusedLocations = unusedLocations;

        // A mesuloc is a measured unused location.
        // An umuloc is an unmeasured unused location.

        var emptysNextToUnused = pattern.GetAdjacentLocations(unusedLocations, false, false);
        var unusedsNextToEmpty = GetAdjacentUnusedLocations(emptysNextToUnused, false, false);
        foreach (var unusedNextToEmpty in unusedsNextToEmpty) {
          distFromNearestRoomOrEdgeByLocation.Add(unusedNextToEmpty, 1);
          DictionaryUtils.AddToSetMultimap(locsByDistFromNearestRoomOrEdge, 1, unusedNextToEmpty);
        }

        int curDist = 1;
        while (distFromNearestRoomOrEdgeByLocation.Count != unusedLocations.Count) {
          curDist++;
          var umulocsNextToMesulocs =
              GetAdjacentUnusedLocations(
                  new SortedSet<Location>(distFromNearestRoomOrEdgeByLocation.Keys),
                  false, false);
          Asserts.Assert(umulocsNextToMesulocs.Count > 0);
          foreach (var umulocNextToMesuloc in umulocsNextToMesulocs) {
            AddToDistMaps(umulocNextToMesuloc, curDist);
          }
        }
      }

      private SortedSet<Location> GetAdjacentUnusedLocations(
          SortedSet<Location> sourceLocs,
          bool includeSourceLocs,
          bool considerCornersAdjacent) {
        var adjacents =
            pattern.GetAdjacentLocations(
                sourceLocs, includeSourceLocs, considerCornersAdjacent);
        var result = new SortedSet<Location>();
        foreach (var loc in adjacents) {
          if (!unusedLocations.Contains(loc))
            continue;
          result.Add(loc);
        }
        return result;
      }

      private void RemoveFromDistMaps(Location loc) {
        int previousDist = distFromNearestRoomOrEdgeByLocation[loc];
        distFromNearestRoomOrEdgeByLocation.Remove(loc);
        locsByDistFromNearestRoomOrEdge[previousDist].Remove(loc);
        if (locsByDistFromNearestRoomOrEdge[previousDist].Count == 0) {
          locsByDistFromNearestRoomOrEdge.Remove(previousDist);
        }
      }

      private void AddToDistMaps(Location loc, int dist) {
        distFromNearestRoomOrEdgeByLocation.Add(loc, dist);
        DictionaryUtils.AddToSetMultimap(locsByDistFromNearestRoomOrEdge, dist, loc);
      }

      public void AddRoom(Room room) {
        foreach (var roomLoc in room.floors) {
          RemoveFromDistMaps(roomLoc);
          unusedLocations.Remove(roomLoc);
        }

        var upToDateLocs = new SortedSet<Location>();
        var adjacentLocs = GetAdjacentUnusedLocations(room.floors, false, false);

        for (int currentDistance = 1; adjacentLocs.Count > 0; currentDistance++) {
          var adjacentLocsToUpdate = new SortedSet<Location>();
          // We have all the adjacent locs from the previous update, in adjacentLocs.
          // Now, lets filter those down to the ones we actually need to update.
          foreach (var adjacentLoc in adjacentLocs) {
            var previousDistance = distFromNearestRoomOrEdgeByLocation[adjacentLoc];
            // If they already report being closer than where we are, remove them
            // from consideration.
            if (currentDistance < previousDistance) {
              adjacentLocsToUpdate.Add(adjacentLoc);
            } else {
              upToDateLocs.Add(adjacentLoc);
            }
          }
          // Now, update all the needed ones to the current distance.
          foreach (var adjacentLoc in adjacentLocsToUpdate) {
            RemoveFromDistMaps(adjacentLoc);
            AddToDistMaps(adjacentLoc, currentDistance);
            upToDateLocs.Add(adjacentLoc);
          }
          // Now, find the ones next to adjacentLocsToUpdate so we can check them in
          // the next iteration.
          adjacentLocs = GetAdjacentUnusedLocations(adjacentLocsToUpdate, false, false);
          SetUtils.RemoveAll(adjacentLocs, upToDateLocs);
        }

        roomByNumber.Add(roomByNumber.Count, room);
      }
    }

    private static List<SortedSet<Location>> FindAllContiguous(
        Pattern pattern,
        bool considerCornersAdjacent,
        SortedSet<Location> allLocs) {
      List<SortedSet<Location>> result = new List<SortedSet<Location>>();
      SortedSet<Location> remaining = new SortedSet<Location>(allLocs);
      while (remaining.Count > 0) {
        var contiguous = FindContiguous(pattern, false, remaining, SetUtils.GetFirst(remaining));
        result.Add(contiguous);
        foreach (var x in contiguous) {
          remaining.Remove(x);
        }
      }
      return result;
    }

    private static SortedSet<Location> FindContiguous(
        Pattern pattern,
        bool considerCornersAdjacent,
        SortedSet<Location> allLocs,
        Location startLoc) {
      SortedSet<Location> found = new SortedSet<Location>();
      FindContiguousInner(pattern, considerCornersAdjacent, allLocs, startLoc, found);
      return found;
    }

    private static void FindContiguousInner(
        Pattern pattern,
        bool considerCornersAdjacent,
        SortedSet<Location> allLocs,
        Location startLoc,
        SortedSet<Location> found) {
      if (found.Contains(startLoc)) {
        return;
      }
      found.Add(startLoc);
      foreach (var adjacent in pattern.GetAdjacentLocations(startLoc, considerCornersAdjacent)) {
        if (allLocs.Contains(adjacent) && !found.Contains(adjacent)) {
          FindContiguousInner(pattern, considerCornersAdjacent, allLocs, adjacent, found);
        }
      }
    }

    public static void GenerateWithWaterfall(
        out Terrain terrain,
        out List<CliffHalf> halves,
        out SortedDictionary<Location, int> preRandifiedElevationByLocation,
        Root root,
        Rand rand,
        Pattern pattern,
        int size,
        bool waterfallTopLeftToBottomRight) {
      float elevationStepHeight = .4f;

      // The "canvas" is the entire area of the level upon which we will
      // paint our layout of rooms and corridors.
      var canvasSearcher =
          new PatternExplorer(
              pattern,
              false,
              new Location(0, 0, 0),
              new RectanglePrioritizer(
                  pattern.GetTileCenter(new Location(0, 0, 0)),
                  2.0f),
              (location, position) => true);

      var tiles = root.EffectTerrainTileByLocationMutMapCreate();

      // Find a ton of tiles.
      for (int i = 0; i < size; i++) {
        Location location = canvasSearcher.Next();
        var tile =
            root.EffectTerrainTileCreate(
                1, ITerrainTileComponentMutBunch.New(root));
        tile.components.Add(root.EffectMagmaTTCCreate().AsITerrainTileComponent());
        tiles.Add(location, tile);
      }

      terrain = root.EffectTerrainCreate(pattern, elevationStepHeight, tiles);

      // Between .74 and .75, pentagonal9 had tiles that had some neighbors with elevation
      // more than 2 above them. Going with .73 to be safe.
      TerrainUtils.slopify(terrain, new Vec2(0, -1), .73f);
      foreach (var location in terrain.tiles.Keys) {
        foreach (var adjacent in terrain.GetAdjacentExistingLocations(location, false)) {
          Asserts.Assert(terrain.GetElevationDifference(location, adjacent) <= 2);
        }
      }

      var waterLocations = GetWaterfallLocations(rand, terrain, waterfallTopLeftToBottomRight);
      foreach (var waterLoc in waterLocations) {
        terrain.tiles[waterLoc].components.Clear();
        terrain.tiles[waterLoc].components.Add(terrain.root.EffectFallsTTCCreate().AsITerrainTileComponent());
      }

      // Save all the elevations of all the things before randifying
      preRandifiedElevationByLocation = new SortedDictionary<Location, int>();
      foreach (var entry in terrain.tiles) {
        preRandifiedElevationByLocation[entry.Key] = entry.Value.elevation;
      }
      TerrainUtils.randify(rand, terrain, 3);
      // Restore the waterfall's elevations
      foreach (var waterLocation in waterLocations) {
        terrain.tiles[waterLocation].elevation =
            preRandifiedElevationByLocation[waterLocation];
      }

      var waterAndAdjacentLocations = new SortedSet<Location>();
      SetUtils.AddAll(waterAndAdjacentLocations, waterLocations);
      SetUtils.AddAll(
          waterAndAdjacentLocations,
          terrain.GetAdjacentExistingLocations(waterLocations, false, false));

      var unusedLocations = new SortedSet<Location>();
      foreach (var tile in terrain.tiles) {
        unusedLocations.Add(tile.Key);
      }
      SetUtils.RemoveAll(unusedLocations, waterAndAdjacentLocations);

      var contiguousAreas = FindAllContiguous(terrain.pattern, false, unusedLocations);
      while (contiguousAreas.Count > 2) {
        int smallestContiguousAreaNumLocs = 0;
        int smallestContiguousAreaIndex = -1;
        for (int i = 0; i < contiguousAreas.Count; i++) {
          if (smallestContiguousAreaIndex == -1 ||
              contiguousAreas[i].Count < smallestContiguousAreaNumLocs) {
            smallestContiguousAreaIndex = i;
            smallestContiguousAreaNumLocs = contiguousAreas[i].Count;
          }
        }
        Asserts.Assert(smallestContiguousAreaIndex != -1);
        contiguousAreas.RemoveAt(smallestContiguousAreaIndex);
      }
      Asserts.Assert(contiguousAreas.Count == 2);
      //if (contiguousAreas.Count != 2) {
      //  context.logger.Error("Not 2!? " + contiguousAreas.Count);
      //} else {
        // Fix the order of contiguousAreas, we want the higher side to be first.
        Location lowLeftLoc = SetUtils.GetFirst(contiguousAreas[0]);
        Vec2 lowLeftPos = pattern.GetTileCenter(lowLeftLoc);
        foreach (var loc in terrain.tiles.Keys) {
          var center = pattern.GetTileCenter(loc);
          if (center.x + center.y < lowLeftPos.x + lowLeftPos.y) {
            lowLeftPos = center;
            lowLeftLoc = loc;
          }
        }
        if (contiguousAreas[0].Contains(lowLeftLoc)) {
          if (waterfallTopLeftToBottomRight) {
            var tmp = contiguousAreas[0];
            contiguousAreas[0] = contiguousAreas[1];
            contiguousAreas[1] = tmp;
          } else {
            // Nothing
          }
        } else { // lowLeftLoc is in contiguousAreas[1]
          Asserts.Assert(contiguousAreas[1].Contains(lowLeftLoc));

          if (waterfallTopLeftToBottomRight) {

          } else {
            var tmp = contiguousAreas[0];
            contiguousAreas[0] = contiguousAreas[1];
            contiguousAreas[1] = tmp;
          }
        //}
      }

      halves = new List<CliffHalf>();
      List<List<Room>> roomsByContiguousArea = new List<List<Room>>();
      for (int i = 0; i < contiguousAreas.Count; i++) {
        var contiguousArea = contiguousAreas[i];
        var roomByNumber = FindRooms(terrain, contiguousArea);
        FlattenRooms(terrain, roomByNumber);
        ConnectRooms(pattern, rand, contiguousArea, roomByNumber);
        SetRoomsTiles(terrain, roomByNumber);

        SortedSet<Location> walkableLocsInThisArea = new SortedSet<Location>();
        foreach (var loc in contiguousArea) {
          if (terrain.tiles[loc].IsWalkable()) {
            walkableLocsInThisArea.Add(loc);
          }
        }
        if (walkableLocsInThisArea.Count == 0) {
          throw new Exception("no walkable locs?");
        }

        halves.Add(new CliffHalf(walkableLocsInThisArea, roomByNumber));
      }
    }

    private static SortedSet<Location> GetWaterfallLocations(
        Rand rand, Terrain terrain, bool topLeftToBottomRight) {

      GenerationCommon.GetMapBounds(
          out float mapMinX,
          out float mapMinY,
          out float mapMaxX,
          out float mapMaxY,
          terrain);

      float mapWidth = mapMaxX - mapMinX;
      float mapHeight = mapMaxY - mapMinY;

      // The waterfall will zig zag down the mountain:
      // ############.x.###
      // ###########...####
      // #########...######
      // ########.x.#######
      // ########...#######
      // #########...######
      // #########.x.######
      // ########...#######
      // #######...########
      // ######...#########
      // #####.x.##########
      // #####...##########
      // ####...###########
      // ####...###########
      // ###.x.############
      // The x are the corners ("elbows").
      // The lines directly between the x's, 1 wide, are the "bones".
      // The thicker lines between the x's, riverWidth wide, are the "arms".

      Vec2[] riverElbowPositions;
      if (topLeftToBottomRight) {
        riverElbowPositions = new Vec2[5] {
          new Vec2(mapMinX + mapWidth * (0.33f + rand.Next(-.05f, .05f, 1000)), mapMinY + mapHeight * 1.0f),
          new Vec2(mapMinX + mapWidth * (0.42f + rand.Next(-.10f, .10f, 1000)), mapMinY + mapHeight * 0.75f),
          new Vec2(mapMinX + mapWidth * (0.50f + rand.Next(-.20f, .20f, 1000)), mapMinY + mapHeight * 0.5f),
          new Vec2(mapMinX + mapWidth * (0.58f + rand.Next(-.10f, .10f, 1000)), mapMinY + mapHeight * 0.25f),
          new Vec2(mapMinX + mapWidth * (0.67f + rand.Next(-.05f, .05f, 1000)), mapMinY + mapHeight * 0.0f),
        };
      } else {
        riverElbowPositions = new Vec2[5] {
          new Vec2(mapMinX + mapWidth * (0.67f + rand.Next(-.05f, .05f, 1000)), mapMinY + mapHeight * 1.0f),
          new Vec2(mapMinX + mapWidth * (0.58f + rand.Next(-.10f, .10f, 1000)), mapMinY + mapHeight * 0.75f),
          new Vec2(mapMinX + mapWidth * (0.50f + rand.Next(-.20f, .20f, 1000)), mapMinY + mapHeight * 0.5f),
          new Vec2(mapMinX + mapWidth * (0.42f + rand.Next(-.10f, .10f, 1000)), mapMinY + mapHeight * 0.25f),
          new Vec2(mapMinX + mapWidth * (0.33f + rand.Next(-.05f, .05f, 1000)), mapMinY + mapHeight * 0.0f),
        };
      }

      var riverElbowLocations = new Location[riverElbowPositions.Length];
      for (int i = 0; i < riverElbowPositions.Length; i++) {
        riverElbowLocations[i] =
            GenerationCommon.GetLocationClosestTo(terrain, riverElbowPositions[i]);
      }

      var waterLocations = new SortedSet<Location>();
      for (int i = 0; i < riverElbowLocations.Length - 1; i++) {
        var boneLocations =
            PatternDriver.Drive(
                terrain.pattern,
                false,
                riverElbowLocations[i],
                riverElbowLocations[i + 1],
                true);
        boneLocations.Insert(0, riverElbowLocations[i]);
        Asserts.Assert(boneLocations.Contains(riverElbowLocations[i]));
        Asserts.Assert(boneLocations.Contains(riverElbowLocations[i + 1]));
        var armLocations =
            terrain.pattern.GetAdjacentLocations(
                new SortedSet<Location>(boneLocations), true, false);
        foreach (var loc in armLocations) {
          if (terrain.TileExists(loc)) {
            waterLocations.Add(loc);
          }
        }
      }

      return waterLocations;
    }

    private static SortedDictionary<int, Room> FindRooms(
        Terrain terrain,
        SortedSet<Location> initialUnusedLocations) {
      int minNumTilesInRoom = 5;
      int maxNumTilesInRoomIncludingBorder = 50;

      var roomThing = new RoomMappingThing(terrain.pattern, new SortedSet<Location>(initialUnusedLocations));
      initialUnusedLocations = null; // So we dont accidentally use it

      SortedSet<Location> attemptedLocations = new SortedSet<Location>();

      for (int i = 0; i < 200; i++) {
        if (roomThing.unusedLocations.Count == 0) {
          break;
        }

        var distancesDecreasing =
            new List<int>(roomThing.locsByDistFromNearestRoomOrEdge.Keys);
        distancesDecreasing.Reverse();

        Location startLocation = null;
        foreach (int distance in distancesDecreasing) {
          foreach (var loc in roomThing.locsByDistFromNearestRoomOrEdge[distance]) {
            if (attemptedLocations.Contains(loc))
              continue;
            startLocation = loc;
            break;
          }
          if (startLocation != null)
            break;
        }
        if (startLocation == null)
          break;


        var roomSearcher =
            new PatternExplorer(
                terrain.pattern,
                false,
                startLocation,
                new OvalPrioritizer(
                    terrain.pattern.GetTileCenter(startLocation),
                    1.5f),
                (location, position) => true);
        var roomFloorLocations =
            new SortedSet<Location>(
                roomSearcher.ExploreWhile(
                    delegate (Location loc) { return roomThing.unusedLocations.Contains(loc); },
                    maxNumTilesInRoomIncludingBorder));

        var roomBorderLocations =
            TerrainUtils.FindBorderLocations(
                terrain.pattern, roomFloorLocations, true);
        // Border locations are not floors, but theyre not unused either.
        SetUtils.RemoveAll(roomFloorLocations, roomBorderLocations);

        attemptedLocations.Add(startLocation);
        // It's already upper bounded by the ExploreWhile max parameter.
        if (roomFloorLocations.Count >= minNumTilesInRoom) {
          roomThing.AddRoom(new Room(roomFloorLocations, roomBorderLocations));
        }
      }

      //foreach (var thing in roomThing.distFromNearestRoomOrEdgeByLocation) {
      //  terrain.tiles[thing.Key].elevation = thing.Value;
      //}

      return roomThing.roomByNumber;
    }

    private static void FlattenRooms(
        Terrain terrain,
        SortedDictionary<int, Room> roomByNumber) {
      foreach (var room in roomByNumber.Values) {
        // To later divide by the number of tiles, to get the average elevation
        int elevationsSum = 0;
        foreach (var roomFloorLocation in room.floors) {
          var tile = terrain.tiles[roomFloorLocation];
          elevationsSum += tile.elevation;
        }
        int averageElevation = (int)Math.Ceiling((float)elevationsSum / room.floors.Count);
        foreach (var roomFloorLocation in room.floors) {
          terrain.tiles[roomFloorLocation].elevation = averageElevation;
        }
      }
    }

    private static void SetRoomsTiles(
        Terrain terrain,
        SortedDictionary<int, Room> roomByNumber) {
      foreach (var room in roomByNumber.Values) {
        if (room.border != null) {
          foreach (var roomFloorLocation in room.border) {
            terrain.tiles[roomFloorLocation].components.Clear();
            terrain.tiles[roomFloorLocation].components.Add(terrain.root.EffectCliffTTCCreate().AsITerrainTileComponent());
          }
        }
      }
      foreach (var room in roomByNumber.Values) {
        if (room.border == null) {
          foreach (var roomFloorLocation in room.floors) {
            terrain.tiles[roomFloorLocation].components.Clear();
            terrain.tiles[roomFloorLocation].components.Add(terrain.root.EffectCliffTTCCreate().AsITerrainTileComponent());
          }
        } else {
          foreach (var roomFloorLocation in room.floors) {
            terrain.tiles[roomFloorLocation].components.Clear();
            terrain.tiles[roomFloorLocation].components.Add(terrain.root.EffectCliffLandingTTCCreate().AsITerrainTileComponent());
          }
        }
      }
    }

    static void ConnectRooms(
        Pattern pattern,
        Rand rand,
        SortedSet<Location> allLocations,
        SortedDictionary<int, Room> roomByNumber) {
      // This function will be adding the corridors to roomByNumber.

      foreach (var room in roomByNumber.Values) {
        foreach (var floor in room.floors) {
          Asserts.Assert(allLocations.Contains(floor));
        }
      }

      SortedDictionary<Location, int> roomNumberByLocation = new SortedDictionary<Location, int>();
      foreach (var numberAndRoom in roomByNumber) {
        foreach (var roomFloorLocation in numberAndRoom.Value.floors) {
          roomNumberByLocation.Add(roomFloorLocation, numberAndRoom.Key);
        }
      }

      // I would just use integers but C# has no typedefs >:(
      var regions = new SortedSet<string>();

      var regionByRoomNumber = new SortedDictionary<int, String>();
      var roomNumbersByRegion = new SortedDictionary<String, SortedSet<int>>();

      foreach (var roomNumberAndRoom in roomByNumber) {
        int roomNumber = roomNumberAndRoom.Key;
        String region = "region" + roomNumber;
        regionByRoomNumber.Add(roomNumber, region);
        var roomNumbersInRegion = new SortedSet<int>();
        roomNumbersInRegion.Add(roomNumber);
        roomNumbersByRegion.Add(region, roomNumbersInRegion);
        regions.Add(region);
        //Logger.Info("Made region " + region);
      }

      while (true) {
        var distinctRegions = new SortedSet<String>(regionByRoomNumber.Values);
        //Logger.Info(distinctRegions.Count + " distinct regions!");
        if (distinctRegions.Count < 2) {
          break;
        }
        var twoRegions = SetUtils.GetFirstN(distinctRegions, 2);
        String regionA = twoRegions[0];
        String regionB = twoRegions[1];
        //Logger.Info("Will aim to connect regions " + regionA + " and " + regionB);

        int regionARoomNumber = SetUtils.GetRandom(rand.Next(), roomNumbersByRegion[regionA]);
        var regionARoom = roomByNumber[regionARoomNumber];
        var regionALocation = SetUtils.GetRandom(rand.Next(), regionARoom.floors);

        int regionBRoomNumber = SetUtils.GetRandom(rand.Next(), roomNumbersByRegion[regionB]);
        var regionBRoom = roomByNumber[regionBRoomNumber];
        var regionBLocation = SetUtils.GetRandom(rand.Next(), regionBRoom.floors);

        // Now lets drive from regionALocation to regionBLocation, and see what happens on the
        // way there.
        var explorer =
            new PatternExplorer(
                pattern,
                false,
                regionALocation,
                new LinearPrioritizer(pattern.GetTileCenter(regionBLocation)),
                (location, position) => allLocations.Contains(location));
        List<Location> path = new List<Location>();
        while (true) {
          Location currentLocation = explorer.Next();
          if (!roomNumberByLocation.ContainsKey(currentLocation)) {
            // It means we're in open space, keep going.
            Asserts.Assert(allLocations.Contains(currentLocation));
            path.Add(currentLocation);
          } else {
            int currentRoomNumber = roomNumberByLocation[currentLocation];
            String currentRegion = regionByRoomNumber[currentRoomNumber];
            if (currentRegion == regionA) {
              // Keep going, but restart the path here.
              path = new List<Location>();
            } else if (currentRegion != regionA) {
              // currentRegionNumber is probably regionBNumber, but isn't necessarily... we could
              // have just come across a random other region.
              // Either way, we hit something, so we stop now.
              break;
            }
          }
        }

        String combinedRegion = "region" + regions.Count;
        regions.Add(combinedRegion);

        int newRoomNumber = roomByNumber.Count;
        roomByNumber.Add(newRoomNumber, new Room(new SortedSet<Location>(path), null));
        foreach (var pathLocation in path) {
          roomNumberByLocation.Add(pathLocation, newRoomNumber);
        }
        regionByRoomNumber.Add(newRoomNumber, combinedRegion);
        // We'll fill in regionNumberByRoomNumber and roomNumbersByRegionNumber shortly.

        // So, now we have a path that we know connects some regions. However, it might be
        // accidentally connecting more than two! It could have grazed past another region without
        // us realizing it.
        // So now, figure out all the regions that this path touches.

        var pathAdjacentLocations = pattern.GetAdjacentLocations(new SortedSet<Location>(path), true, false);
        var pathAdjacentRegions = new SortedSet<String>();
        foreach (var pathAdjacentLocation in pathAdjacentLocations) {
          if (roomNumberByLocation.ContainsKey(pathAdjacentLocation)) {
            int roomNumber = roomNumberByLocation[pathAdjacentLocation];
            String region = regionByRoomNumber[roomNumber];
            pathAdjacentRegions.Add(region);
          }
        }

        var roomNumbersInCombinedRegion = new SortedSet<int>();
        roomNumbersInCombinedRegion.Add(newRoomNumber);
        foreach (var pathAdjacentRegion in pathAdjacentRegions) {
          if (pathAdjacentRegion == combinedRegion) {
            // The new room is already part of this region
            continue;
          }
          foreach (var pathAdjacentRoomNumber in roomNumbersByRegion[pathAdjacentRegion]) {
            //Logger.Info("Overwriting " + pathAdjacentRoomNumber + "'s region to " + combinedRegion);
            regionByRoomNumber[pathAdjacentRoomNumber] = combinedRegion;
            roomNumbersInCombinedRegion.Add(pathAdjacentRoomNumber);
          }
          roomNumbersByRegion.Remove(pathAdjacentRegion);
        }
        roomNumbersByRegion.Add(combinedRegion, roomNumbersInCombinedRegion);

        String roomNums = "";
        foreach (var pathAdjacentRoomNumber in roomNumbersInCombinedRegion) {
          if (roomNums != "") {
            roomNums = roomNums + ", ";
          }
          roomNums = roomNums + pathAdjacentRoomNumber;
        }
        //Logger.Info("Region " + combinedRegion + " now has room numbers: " + roomNums);
        roomNumbersByRegion[combinedRegion] = roomNumbersInCombinedRegion;
      }
    }
  }
}
