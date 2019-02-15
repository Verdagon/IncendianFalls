using System;
using Atharia.Model;
using System.Collections.Generic;

namespace IncendianFalls {
  public class PatternDriver {
    private class Prioritizer : IPatternExplorerPrioritizer {
      readonly Pattern pattern;
      readonly Ray ray;
      public Prioritizer(Pattern pattern, Ray ray) {
        this.pattern = pattern;
        this.ray = ray;
      }
      public float GetPriority(Location location, Vec2 position) {
        return pattern.RayIntersectsLocation(location, ray) ? 1 : -1;
      }
    }

    public static List<Location> Drive(
        Pattern pattern,
        // If corners are adjacent, we'll use the same algorithm as if they weren't, but afterwards
        // we'll skip any needless steps.
        bool considerCornersAdjacent,
        Location startLoc,
        Location endLoc) {
      List<Location> result = new List<Location>();
      Vec2 startPos = pattern.GetTileCenter(startLoc);
      Vec2 endPos = pattern.GetTileCenter(endLoc);
      PatternExplorer explorer =
          new PatternExplorer(
              pattern,
              false, // not considering corners adjacent yet
              startLoc,
              new Prioritizer(pattern, new Ray(startPos, endPos.minus(startPos))));
      List<Location> path =
          explorer.ExploreWhile(
              delegate(Location loc) { return loc != endLoc; },
              -1,
              0);
      path.Add(endLoc);
      if (considerCornersAdjacent) {
        // Take out any needless steps.
        for (int i = 0; i < path.Count - 2; ) {
          if (pattern.GetAdjacentLocations(path[i], true).Contains(path[i + 2])) {
            path.RemoveAt(i + 1);
          } else {
            i++;
          }
        }
      }
      path.Remove(startLoc);
      return path;
    }
  }
}
