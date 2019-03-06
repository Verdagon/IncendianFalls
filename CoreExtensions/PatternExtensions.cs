﻿using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class PatternExtensions {

    public static float DistanceBetween(this Pattern pattern, Location locA, Location locB) {
      return pattern.GetTileCenter(locA).distance(pattern.GetTileCenter(locB));
    }

    public static Vec2 GetTileCenter(this Pattern pattern, Location loc) {
      return pattern.xOffset.mul(loc.groupX)
          .plus(pattern.yOffset.mul(loc.groupY))
          .plus(pattern.patternTiles[loc.indexInGroup].translate);
    }

    private static double DegreesToRadians(double angle) {
      return Math.PI * angle / 180.0;
    }

    public static List<Vec2> GetRelativeCornerPositions(this Pattern pattern, Location loc) {
      var patternTile = pattern.patternTiles[loc.indexInGroup];
      int shapeIndex = patternTile.shapeIndex;
      float rotateDegrees = patternTile.rotateDegrees;
      double rotateRadians = DegreesToRadians(rotateDegrees);
      var corners = pattern.cornersByShapeIndex[shapeIndex];

      List<Vec2> results = new List<Vec2>();

      for (int i = 0; i < corners.Count; i++) {
        Vec2 unrotatedCorner = corners[i];
        Vec2 rotatedCorner =
            new Vec2(
                (float)(unrotatedCorner.x * Math.Cos(rotateRadians) -
                    unrotatedCorner.y * Math.Sin(rotateRadians)),
                (float)(unrotatedCorner.y * Math.Cos(rotateRadians) +
                    unrotatedCorner.x * Math.Sin(rotateRadians)));
        results.Add(rotatedCorner);
      }

      return results;
    }

    public static List<Vec2> GetCornerPositions(this Pattern pattern, Location loc) {
      var center = pattern.GetTileCenter(loc);
      List<Vec2> results = new List<Vec2>();
      foreach (var relativeCorner in pattern.GetRelativeCornerPositions(loc)) {
        results.Add(center.plus(relativeCorner));
      }
      return results;
    }

    public static List<Location> getRelativeAdjacentLocations(this Pattern pattern, int tileIndex, bool adjacentCornersToo) {
      SortedDictionary<Location, object> result = new SortedDictionary<Location, object>();
      PatternTile tile = pattern.patternTiles[tileIndex];
      foreach (var sideAdjacency in tile.sideAdjacenciesBySideIndex) {
        var location =
            new Location(
                sideAdjacency.groupRelativeX,
                sideAdjacency.groupRelativeY,
                sideAdjacency.tileIndex);
        if (!result.ContainsKey(location)) {
          result.Add(location, new object());
        }
      }
      if (adjacentCornersToo) {
        foreach (var cornerAdjacencies in tile.cornerAdjacenciesByCornerIndex) {
          foreach (var cornerAdjacency in cornerAdjacencies) {
            var location = new Location(cornerAdjacency.groupRelativeX, cornerAdjacency.groupRelativeY, cornerAdjacency.tileIndex);
            if (!result.ContainsKey(location)) {
              result.Add(location, new object());
            }
          }
        }
      }
      return new List<Location>(result.Keys);
    }

    public static List<Location> GetAdjacentLocations(this Pattern pattern, Location loc, bool considerCornersAdjacent) {
      List<Location> result = new List<Location>();
      foreach (Location relativeLoc in pattern.getRelativeAdjacentLocations(loc.indexInGroup, considerCornersAdjacent)) {
        result.Add(new Location(
          loc.groupX + relativeLoc.groupX,
          loc.groupY + relativeLoc.groupY,
          relativeLoc.indexInGroup));
      }
      return result;
    }

    public static SortedSet<Location> GetAdjacentLocations(
        this Pattern pattern,
        SortedSet<Location> sourceLocs,
        bool includeSourceLocs,
        bool considerCornersAdjacent) {
      var result = new SortedSet<Location>();
      foreach (var originalLocation in sourceLocs) {
        var adjacents = pattern.GetAdjacentLocations(originalLocation, considerCornersAdjacent);
        if (includeSourceLocs) {
          adjacents.Add(originalLocation);
        }
        foreach (var adjacentLocation in adjacents) {
          if (!includeSourceLocs && sourceLocs.Contains(adjacentLocation))
            continue;
          result.Add(adjacentLocation);
        }
      }
      return result;
    }

    public static bool LocationsAreAdjacent(this Pattern pattern, Location a, Location b, bool considerCornersAdjacent) {
      var locsAdjacentToA = pattern.GetAdjacentLocations(a, considerCornersAdjacent);
      return 0 <= locsAdjacentToA.FindIndex(delegate (Location hay) { return hay == b; });
    }

    public static bool LineIntersectsLocation(this Pattern pattern, Vec2 position, Vec2 direction, Location location) {
      var polygon = new Polygon(pattern.GetCornerPositions(location));
      return MathUtils.Intersects(polygon, new Line(position, direction));
    }
    public static bool SegmentIntersectsLocation(this Pattern pattern, Vec2 start, Vec2 end, Location location) {
      var polygon = new Polygon(pattern.GetCornerPositions(location));
      return MathUtils.Intersects(polygon, new Segment(start, end));
    }
    public static bool RayIntersectsLocation(this Pattern pattern, Location location, Ray ray) {
      var polygon = new Polygon(pattern.GetCornerPositions(location));
      return MathUtils.Intersects(polygon, ray);
    }
  }
}
