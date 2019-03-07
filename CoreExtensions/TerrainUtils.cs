using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class TerrainUtils {
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

    public static void randify(Random random, Terrain terrain, int range) {
      foreach (var locationAndTile in terrain.tiles) {
        var tile = locationAndTile.Value;

        var heightIncrease = random.Next(range - 1);
        var height = tile.elevation + heightIncrease;
        tile.elevation = height;
      }
    }

    public static void randify(Rand rand, Terrain terrain, int range) {
      foreach (var locationAndTile in terrain.tiles) {
        var tile = locationAndTile.Value;

        var heightIncrease = rand.Next() % range;
        var height = tile.elevation + heightIncrease;
        tile.elevation = height;
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