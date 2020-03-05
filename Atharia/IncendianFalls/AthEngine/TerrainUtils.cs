using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class TerrainUtils {

    public static Location GetLocationClosestTo(
        Terrain terrain,
        Vec2 targetPos) {
      Location closestLocation = new Location(0, 0, 0);
      float closestLocationDistance =
          terrain.pattern.GetTileCenter(closestLocation)
          .distance(targetPos);

      foreach (var hay in terrain.tiles) {
        var hayLoc = hay.Key;
        var hayCenter = terrain.pattern.GetTileCenter(hayLoc);
        var hayDistance = hayCenter.distance(targetPos);
        if (hayDistance < closestLocationDistance) {
          closestLocation = hayLoc;
          closestLocationDistance = hayDistance;
        }
      }

      return closestLocation;
    }

    public static void GetMapBounds(
        out float mapMinX,
        out float mapMinY,
        out float mapMaxX,
        out float mapMaxY,
        Terrain terrain) {
      mapMinX = 0;
      mapMinY = 0;
      mapMaxX = 0;
      mapMaxY = 0;

      foreach (var entry in terrain.tiles) {
        var location = entry.Key;
        var center = terrain.pattern.GetTileCenter(location);
        mapMinX = Math.Min(mapMinX, center.x);
        mapMinY = Math.Min(mapMinY, center.y);
        mapMaxX = Math.Max(mapMaxX, center.x);
        mapMaxY = Math.Max(mapMaxY, center.y);
      }
    }

    // Returns all this-room spaces that are touching anything not in this room.
    public static SortedSet<Location> FindBorderLocations(
        Pattern pattern,
        SortedSet<Location> floorLocations,
        bool considerCornersAdjacent) {
      var borderLocations = new SortedSet<Location>();
      foreach (var roomLocation in floorLocations) {
        foreach (var adjacentLocation in pattern.GetAdjacentLocations(roomLocation, considerCornersAdjacent)) {
          if (!floorLocations.Contains(adjacentLocation)) {
            borderLocations.Add(roomLocation);
            break;
          }
        }
      }
      return borderLocations;
    }

    delegate float GetDistInEyeDirection(Location loc);

    public static void slopify(Terrain terrain, Vec2 directionToEye, float slope) {
      GetDistInEyeDirection getDistInEyeDirection = delegate (Location loc) {
        Vec2 center = terrain.pattern.GetTileCenter(loc);
        return center.dot(directionToEye);
      };

      var locationFurthestInEyeDirection = new Location(0, 0, 0);
      float distanceFurthestInEyeDirection =
          getDistInEyeDirection(locationFurthestInEyeDirection);

      foreach (var locationAndTile in terrain.tiles) {
        float distanceInEyeDirection = getDistInEyeDirection(locationAndTile.Key);
        if (distanceInEyeDirection > distanceFurthestInEyeDirection) {
          distanceFurthestInEyeDirection = distanceInEyeDirection;
          locationFurthestInEyeDirection = locationAndTile.Key;
        }
      }

      foreach (var locationAndTile in terrain.tiles) {
        float distanceInEyeDirection = getDistInEyeDirection(locationAndTile.Key);
        float distanceFromClosest =
            distanceFurthestInEyeDirection - getDistInEyeDirection(locationAndTile.Key);

        var tile = locationAndTile.Value;
        tile.elevation = (int)(1 + distanceFromClosest * slope / terrain.elevationStepHeight);
      }
    }

    //public static void randify(Random random, Terrain terrain, int range) {
    //  foreach (var locationAndTile in terrain.tiles) {
    //    var tile = locationAndTile.Value;

    //    var heightIncrease = random.Next(range - 1);
    //    var height = tile.elevation + heightIncrease;
    //    tile.elevation = height;
    //  }
    //}

    public static void randify(Rand rand, Terrain terrain, int range) {
      foreach (var locationAndTile in terrain.tiles) {
        var tile = locationAndTile.Value;

        var heightIncrease = rand.Next() % range;
        var height = tile.elevation + heightIncrease;
        tile.elevation = height;
      }
    }

    public static List<SortedSet<Location>> IdentifyRooms(Terrain terrain, bool considerCornersAdjacent) {
      var roomIndexByLocation = new SortedDictionary<Location, int>();
      var rooms = new List<SortedSet<Location>>();

      foreach (var locationAndTile in terrain.tiles) {
        var sparkLocation = locationAndTile.Key;
        if (roomIndexByLocation.ContainsKey(sparkLocation)) {
          continue;
        }
        var connectedLocations = FindAllConnectedLocations(terrain, considerCornersAdjacent, sparkLocation);
        var newRoomIndex = rooms.Count;
        rooms.Add(connectedLocations);
        foreach (var connectedLocation in connectedLocations) {
          Asserts.Assert(!roomIndexByLocation.ContainsKey(connectedLocation));
          roomIndexByLocation.Add(connectedLocation, newRoomIndex);
        }
      }
      return rooms;
    }

    public static SortedSet<Location> FindAllConnectedLocations(Terrain terrain, bool considerCornersAdjacent, Location startLocation) {
      var connectedWithUnexploredNeighbors = new SortedSet<Location>();
      var connectedWithExploredNeighbors = new SortedSet<Location>();

      connectedWithUnexploredNeighbors.Add(startLocation);

      while (connectedWithUnexploredNeighbors.Count > 0) {
        var current = SetUtils.GetFirst(connectedWithUnexploredNeighbors);
        Asserts.Assert(!connectedWithExploredNeighbors.Contains(current));

        connectedWithUnexploredNeighbors.Remove(current);
        connectedWithExploredNeighbors.Add(current);

        foreach (var neighbor in terrain.GetAdjacentExistingLocations(current, considerCornersAdjacent)) {
          if (connectedWithExploredNeighbors.Contains(neighbor)) {
            continue;
          }
          if (connectedWithUnexploredNeighbors.Contains(neighbor)) {
            continue;
          }
          connectedWithUnexploredNeighbors.Add(neighbor);
        }
      }

      return connectedWithExploredNeighbors;
    }

    public static void ConnectRooms(Pattern pattern, Rand rand, List<SortedSet<Location>> rooms) {
      // This function will be adding the corridors to roomByNumber.

      SortedDictionary<Location, int> roomIndexByLocation = new SortedDictionary<Location, int>();

      for (int roomIndex = 0; roomIndex < rooms.Count; roomIndex++) {
        var room = rooms[roomIndex];
        foreach (var roomFloorLocation in room) {
          roomIndexByLocation.Add(roomFloorLocation, roomIndex);
        }
      }

      // I would just use integers but C# has no typedefs >:(
      var regions = new SortedSet<string>();

      var regionByRoomIndex = new SortedDictionary<int, String>();
      var roomIndexsByRegion = new SortedDictionary<String, SortedSet<int>>();

      for (int roomIndex = 0; roomIndex < rooms.Count; roomIndex++) {
        var room = rooms[roomIndex];
        String region = "region" + roomIndex;
        regionByRoomIndex.Add(roomIndex, region);
        var roomIndexsInRegion = new SortedSet<int>();
        roomIndexsInRegion.Add(roomIndex);
        roomIndexsByRegion.Add(region, roomIndexsInRegion);
        regions.Add(region);
        //Logger.Info("Made region " + region);
      }

      while (true) {
        var distinctRegions = new SortedSet<String>(regionByRoomIndex.Values);
        //Logger.Info(distinctRegions.Count + " distinct regions!");
        if (distinctRegions.Count < 2) {
          break;
        }
        var twoRegions = SetUtils.GetFirstN(distinctRegions, 2);
        String regionA = twoRegions[0];
        String regionB = twoRegions[1];
        //Logger.Info("Will aim to connect regions " + regionA + " and " + regionB);

        int regionARoomIndex = SetUtils.GetRandom(rand.Next(), roomIndexsByRegion[regionA]);
        var regionARoom = rooms[regionARoomIndex];
        var regionALocation = SetUtils.GetRandom(rand.Next(), regionARoom);

        int regionBRoomIndex = SetUtils.GetRandom(rand.Next(), roomIndexsByRegion[regionB]);
        var regionBRoom = rooms[regionBRoomIndex];
        var regionBLocation = SetUtils.GetRandom(rand.Next(), regionBRoom);

        // Now lets drive from regionALocation to regionBLocation, and see what happens on the
        // way there.
        var explorer =
            new PatternExplorer(
                pattern,
                false,
                regionALocation,
                new LinearPrioritizer(pattern.GetTileCenter(regionBLocation)),
            (location, position) => true);
        List<Location> path = new List<Location>();
        while (true) {
          Location currentLocation = explorer.Next();
          if (!roomIndexByLocation.ContainsKey(currentLocation)) {
            // It means we're in open space, keep going.
            path.Add(currentLocation);
          } else {
            int currentRoomIndex = roomIndexByLocation[currentLocation];
            String currentRegion = regionByRoomIndex[currentRoomIndex];
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

        int newRoomIndex = rooms.Count;
        rooms.Add(new SortedSet<Location>(path));
        foreach (var pathLocation in path) {
          roomIndexByLocation.Add(pathLocation, newRoomIndex);
        }
        regionByRoomIndex.Add(newRoomIndex, combinedRegion);
        // We'll fill in regionNumberByRoomIndex and roomIndexsByRegionNumber shortly.

        // So, now we have a path that we know connects some regions. However, it might be
        // accidentally connecting more than two! It could have grazed past another region without
        // us realizing it.
        // So now, figure out all the regions that this path touches.

        var pathAdjacentLocations = pattern.GetAdjacentLocations(new SortedSet<Location>(path), true, false);
        var pathAdjacentRegions = new SortedSet<String>();
        foreach (var pathAdjacentLocation in pathAdjacentLocations) {
          if (roomIndexByLocation.ContainsKey(pathAdjacentLocation)) {
            int roomIndex = roomIndexByLocation[pathAdjacentLocation];
            String region = regionByRoomIndex[roomIndex];
            pathAdjacentRegions.Add(region);
          }
        }

        var roomIndexsInCombinedRegion = new SortedSet<int>();
        roomIndexsInCombinedRegion.Add(newRoomIndex);
        foreach (var pathAdjacentRegion in pathAdjacentRegions) {
          if (pathAdjacentRegion == combinedRegion) {
            // The new room is already part of this region
            continue;
          }
          foreach (var pathAdjacentRoomIndex in roomIndexsByRegion[pathAdjacentRegion]) {
            //Logger.Info("Overwriting " + pathAdjacentRoomIndex + "'s region to " + combinedRegion);
            regionByRoomIndex[pathAdjacentRoomIndex] = combinedRegion;
            roomIndexsInCombinedRegion.Add(pathAdjacentRoomIndex);
          }
          roomIndexsByRegion.Remove(pathAdjacentRegion);
        }
        roomIndexsByRegion.Add(combinedRegion, roomIndexsInCombinedRegion);

        String roomNums = "";
        foreach (var pathAdjacentRoomIndex in roomIndexsInCombinedRegion) {
          if (roomNums != "") {
            roomNums = roomNums + ", ";
          }
          roomNums = roomNums + pathAdjacentRoomIndex;
        }
        //Logger.Info("Region " + combinedRegion + " now has room numbers: " + roomNums);
        roomIndexsByRegion[combinedRegion] = roomIndexsInCombinedRegion;
      }
    }
  }


  public class Room {
    public readonly SortedSet<Location> floors;
    public readonly SortedSet<Location> border;

    public Room(SortedSet<Location> floors, SortedSet<Location> border) {
      this.floors = floors;
      this.border = border;
    }

    public Location CalculateCentermostLocation(Pattern pattern) {
      Vec2 roomPositionsSum = new Vec2(0, 0);
      foreach (var roomFloorLocation in floors) {
        roomPositionsSum = roomPositionsSum.plus(pattern.GetTileCenter(roomFloorLocation));
      }
      Vec2 roomCenterPosition = roomPositionsSum.div(floors.Count);

      Location centermostLocation = SetUtils.GetFirst(floors);
      float distanceCenterToCentermostLocation =
          pattern.GetTileCenter(centermostLocation).distance(roomCenterPosition);
      foreach (var roomFloorLocation in floors) {
        var position = pattern.GetTileCenter(roomFloorLocation);
        var distance = position.distance(roomCenterPosition);
        if (distance < distanceCenterToCentermostLocation) {
          distanceCenterToCentermostLocation = distance;
          centermostLocation = roomFloorLocation;
        }
      }
      return centermostLocation;
    }
  }


  public class RectanglePrioritizer : IPatternExplorerPrioritizer {
    readonly Vec2 originPosition;
    readonly float widthOverHeightRatio;
    public RectanglePrioritizer(Vec2 originPosition, float widthOverHeightRatio) {
      this.originPosition = originPosition;
      this.widthOverHeightRatio = widthOverHeightRatio;
    }

    public float GetPriority(Location location, Vec2 position) {
      return -Math.Abs(
          Math.Max(
              Math.Abs(originPosition.x - position.x),
              Math.Abs(originPosition.y - position.y) * widthOverHeightRatio));
    }
  }

  // Not really an oval, more like a stretched circle.
  public class OvalPrioritizer : IPatternExplorerPrioritizer {
    Vec2 target;
    readonly float widthOverHeightRatio;
    public OvalPrioritizer(Vec2 target, float widthOverHeightRatio) {
      this.target = target;
      this.widthOverHeightRatio = widthOverHeightRatio;
    }

    public float GetPriority(Location location, Vec2 position) {
      // - because higher is better
      return -(float)Math.Sqrt(
         ((position.x - target.x)) * ((position.x - target.x)) +
         ((position.y - target.y) * widthOverHeightRatio) * ((position.y - target.y) * widthOverHeightRatio));
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