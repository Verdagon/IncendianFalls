using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class PatternUtils {

    public static Location GetFurthestLocationInDirection(
        Pattern pattern,
        SortedSet<Location> locs,
        Vec2 direction) {
      Location furthestLoc = SetUtils.GetFirst(locs);
      float furthestDistance =
          pattern.GetTileCenter(furthestLoc)
          .dot(direction);
      foreach (var hayLoc in locs) {
        float hayDistance =
            pattern.GetTileCenter(hayLoc)
            .dot(direction);
        if (hayDistance > furthestDistance) {
          furthestDistance = hayDistance;
          furthestLoc = hayLoc;
        }
      }
      return furthestLoc;
    }
  }
}