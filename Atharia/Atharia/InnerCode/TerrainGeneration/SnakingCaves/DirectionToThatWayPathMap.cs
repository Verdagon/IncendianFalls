using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class DirectionToThatWayPathMap {
    public static SortedDictionary<Direction, List<Location>> GetDirectionToThatWayPathMap(
        Pattern pattern,
        bool considerCornersAdjacent,
        Location fromLocation) {
      var fromPosition = pattern.GetTileCenter(fromLocation);
      var explorer =
          new AStarExplorer(
              pattern,
              new SortedSet<Location>() { fromLocation },
              considerCornersAdjacent,
              (a, b, totalCost) => pattern.GetTileCenter(fromLocation).distance(pattern.GetTileCenter(b)) < SnakingCaveTerrainGenerator.SLITHER_DISTANCE,
              (a) => false,
              (a) => 0,
              (a, b) => pattern.GetTileCenter(a).distance(pattern.GetTileCenter(b)));
      var directionToTargetPosition = new Vec2[Direction.NUM];
      var directionToThatWayLocationAndError = new (Location, float)[Direction.NUM];
      for (int i = 0; i < Direction.NUM; i++) {
        var direction = new Direction(i);
        Vec2 targetPosition = fromPosition.plus(new Vec2(direction.toVec().x * 3, direction.toVec().y * 3));
        directionToTargetPosition[direction.dirNum] = targetPosition;
        // Just to give it an arbitrary initial location
        var initialLocation = fromLocation;
        var initialLocationError = pattern.GetTileCenter(initialLocation).distance(targetPosition);
        directionToThatWayLocationAndError[direction.dirNum] = (initialLocation, initialLocationError);
      }
      foreach (var thisLocation in explorer.getClosedLocations()) {
        for (int dirNum = 0; dirNum < directionToTargetPosition.Length; dirNum++) {
          var targetPosition = directionToTargetPosition[dirNum];
          var thisLocationError = pattern.GetTileCenter(thisLocation).distance(targetPosition);
          var existingClosestLocationError = directionToThatWayLocationAndError[dirNum].Item2;
          if (thisLocationError < existingClosestLocationError) {
            directionToThatWayLocationAndError[dirNum] = (thisLocation, thisLocationError);
          }
        }
      }

      var directionToThatWayPath = new SortedDictionary<Direction, List<Location>>();
      for (int dirNum = 0; dirNum < Direction.NUM; dirNum++) {
        var thatWayLocation = directionToThatWayLocationAndError[dirNum].Item1;
        var thatWayPath = explorer.GetPathTo(thatWayLocation);
        Asserts.Assert(thatWayPath.Count > 0);
        directionToThatWayPath.Add(new Direction(dirNum), thatWayPath);
      }
      return directionToThatWayPath;
    }
  }
}