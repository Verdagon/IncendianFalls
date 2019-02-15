using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class ForestTerrainGenerator {
    public static Terrain Generate(Root root, Rand rand, Pattern pattern) {
      float elevationStepHeight = .2f;

      // The "canvas" is the entire area of the level upon which we will
      // paint our layout of rooms and corridors.
      var canvasSearcher =
          new PatternExplorer(
              pattern,
              false,
              new Location(0, 0, 0),
              new RectanglePrioritizer(
                  pattern.GetTileCenter(new Location(0, 0, 0)),
                  2.0f));

      var roomByNumber = new SortedDictionary<int, Room>();
      var borderLocations = new SortedSet<Location>();
      var unusedLocations = new SortedSet<Location>();

      // Find a ton of tiles.
      for (int i = 0; i < 1000; i++) {
        Location loc = canvasSearcher.Next();
        Vec2 center = pattern.GetTileCenter(loc);
        unusedLocations.Add(loc);
      }

      //foreach (var tile in terrain.tiles) {
      //  unusedLocations.Add(tile.Key, new object());
      //}

      // 100 attempts to make rooms.
      for (int i = 0; i < 100; i++) {
        Location startLocation = SetUtils.GetRandom(rand.Next(), unusedLocations);
        var roomSearcher = new PatternExplorer(pattern, false, startLocation);
        int minNumTilesInRoom = 15;
        int maxNumTilesInRoom = 100;

        var roomFloorLocations =
            new SortedSet<Location>(
                roomSearcher.ExploreWhile(
                    delegate (Location loc) { return unusedLocations.Contains(loc); },
                    maxNumTilesInRoom));

        var roomBorderLocations =
            TerrainUtils.FindBorderLocations(pattern, roomFloorLocations, true);
        // Border locations are not floors, but theyre not unused either.
        SetUtils.RemoveAll(roomFloorLocations, roomBorderLocations);
        SetUtils.RemoveAll(unusedLocations, roomBorderLocations);

        if (roomFloorLocations.Count >= minNumTilesInRoom &&
            roomFloorLocations.Count <= maxNumTilesInRoom) {
          SetUtils.RemoveAll(unusedLocations, roomFloorLocations);
          roomByNumber.Add(roomByNumber.Count, new Room(roomFloorLocations));
        }
      }

      // Now that we have a bunch of rooms, let's connect them.

      ConnectRooms(pattern, rand, roomByNumber);

      var tiles = root.EffectTerrainTileByLocationMutMapCreate();

      foreach (var room in roomByNumber.Values) {
        foreach (var roomFloorLocation in room.floors) {
          var tile = root.EffectTerrainTileCreate(1, true, "grass");
          tiles.Add(roomFloorLocation, tile);
        }
      }

      var allTiles = new SortedSet<Location>(tiles.Keys);
      var allAdjacent = pattern.GetAdjacentLocations(allTiles, true);
      SetUtils.RemoveAll(allAdjacent, allTiles);
      foreach (var borderLocation in allAdjacent) {
        var tile = root.EffectTerrainTileCreate(2, false, "grass");
        tiles.Add(borderLocation, tile);
      }

      var terrain = root.EffectTerrainCreate(pattern, elevationStepHeight, tiles);
      return terrain;
    }

    

    static void ConnectRooms(Pattern pattern, Rand rand, SortedDictionary<int, Room> roomByNumber) {
      // This function will be adding the corridors to roomByNumber.

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
        //Logger.Info("Starting iteration!");
        //foreach (var roomNumberAndRegion in regionByRoomNumber) {
        //  Logger.Info(roomNumberAndRegion.Key + " is in " + roomNumberAndRegion.Value);
        //}
        //foreach (var regionAndRoomNumbers in roomNumbersByRegion) {
        //  foreach (var roomNumber in regionAndRoomNumbers.Value) {
        //    Logger.Info(regionAndRoomNumbers.Key + " has " + roomNumber);
        //  }
        //}

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
                new LinearPrioritizer(pattern.GetTileCenter(regionBLocation)));
        List<Location> path = new List<Location>();
        while (true) {
          Location currentLocation = explorer.Next();
          if (!roomNumberByLocation.ContainsKey(currentLocation)) {
            // It means we're in open space, keep going.
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
        roomByNumber.Add(newRoomNumber, new Room(new SortedSet<Location>(path)));
        foreach (var pathLocation in path) {
          roomNumberByLocation.Add(pathLocation, newRoomNumber);
        }
        regionByRoomNumber.Add(newRoomNumber, combinedRegion);
        // We'll fill in regionNumberByRoomNumber and roomNumbersByRegionNumber shortly.

        // So, now we have a path that we know connects some regions. However, it might be
        // accidentally connecting more than two! It could have grazed past another region without
        // us realizing it.
        // So now, figure out all the regions that this path touches.

        var pathAdjacentLocations = pattern.GetAdjacentLocations(new SortedSet<Location>(path), true);
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

  class LinearPrioritizer : IPatternExplorerPrioritizer {
    Vec2 target;
    public LinearPrioritizer(Vec2 target) {
      this.target = target;
    }
    public float GetPriority(Location location, Vec2 position) {
      // - because higher is better
      return -position.distance(target);
    }
  }
}
