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
        Location endLoc,
        bool includeStart) {
      var path =
          AStarExplorer.Go(
              pattern,
              startLoc,
              endLoc,
              considerCornersAdjacent,
              (Location from, Location to, float totalCost) => true);
      Asserts.Assert(path.Count > 0);
      if (includeStart) {
        path.Insert(0, startLoc);
      }
      return path;
    }
  }
}
