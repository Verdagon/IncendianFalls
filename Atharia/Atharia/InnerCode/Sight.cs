using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class Sight {
    // We could instead store this per terrain or per level. Maybe 0 would mean everyone can see everyone?
    public const int MAX_SIGHT_RANGE = 8;
    
    public static SortedSet<Location> GetVisibleLocationsExpensive(
        Terrain terrain, Location sourceLoc) {
      // This is a super approximation, which doesn't even really do line-of-sight.
      // It does more like... within hearing distance. Meh, it's fine for now.

      var sourceLocElevation = terrain.tiles[sourceLoc].elevation;
      
      // We choose an arbitrary* number to multiply each step by, let's say 9.
      // So, if the max sight range is 8, and each step will cost 9, so we're
      // looking for paths <= 72.
      // *It's not actually arbitrary. We choose a number equal to the sight range + 1.
      // Also, if we step into something that blocks sight range, we add 1.
      // This way, we can know at the end whether the path crossed something that blocked
      // vision, by seeing if it's not a multiple of e.g. 9.
      var STEP_COST = MAX_SIGHT_RANGE + 1;
      var explorer =
          new AStarExplorer(
              new SortedSet<Location> {sourceLoc},
              (a) => terrain.GetAdjacentExistingLocations(a, terrain.considerCornersAdjacent),
              (from, to, totalCost) => {
                // This would be totalCost <= 72, but we want to allow the +1s from the
                // things blocking visibility. There can be up to MAX_SIGHT_RANGE of them
                // so really we want < 81, hence the + 1 here.
                return totalCost < (MAX_SIGHT_RANGE + 1) * STEP_COST;
              },
              a => false, // no stopping early
              a => 0, // no guess because no goal
              (a, b) => {
                var blocksSight =
                    terrain.tiles[b].BlocksSight() ||
                    terrain.tiles[b].elevation >= sourceLocElevation + 3;
                return STEP_COST + (blocksSight ? 1 : 0);
              });
      var locsInRange = explorer.getClosedLocations();
      var result = new SortedSet<Location>();
      foreach (var locInRange in locsInRange) {
        // If it's a multiple, no +1s were thrown in, which means visibility isnt blocked.
        if (explorer.GetCostTo(locInRange) % STEP_COST == 0) {
          result.Add(locInRange);
        }
      }
      return result;
    }
  }
}
