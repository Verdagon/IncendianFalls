using System;
using System.Collections.Generic;
using System.Linq;
using Atharia.Model;

namespace IncendianFalls {
  public class Navigation {
    public const int NEARBY_THRESHOLD = 8;
    
    public static SortedDictionary<Location, List<Location>> GetNearbyLocationPathsExpensive(
        LevelSuperstate levelSuperstate, Location sourceLoc) {
      var explorer =
          new AStarExplorer(
              new SortedSet<Location> {sourceLoc},
              (a) => levelSuperstate.GetHoppableLocs(a, false),
              (from, to, totalCost) => totalCost <= NEARBY_THRESHOLD,
              a => false, // no stopping early
              a => 0, // no guess because no goal
              (a, b) => 1); // all steps equal
      var locsInRange = explorer.getClosedLocations();
      locsInRange.Remove(sourceLoc);
      var result = new SortedDictionary<Location, List<Location>>();
      foreach (var locInRange in locsInRange) {
        result.Add(locInRange, explorer.GetPathTo(locInRange));
      }
      Asserts.Assert(!result.ContainsKey(sourceLoc));
      return result;
    }
    
    public static List<Location> FindPathExpensive(
        Terrain terrain, LevelSuperstate levelSuperstate, Location source, Location destination) {
      var explorer =
          new AStarExplorer(
              new SortedSet<Location> {source},
              (a) => levelSuperstate.GetHoppableLocs(a, false),
              (a, b, totalCost) => true,
              (a) => a == destination,
              AStarExplorer.MakeDistanceCostGuesser(terrain.pattern, destination),
              (a, b) => {
                if (terrain.pattern.LocationsAreAdjacent(a, b, terrain.considerCornersAdjacent)) {
                  return 1;
                } else {
                  return Actions.LEAP_DISTANCE + 1;
                }
              });
      if (explorer.getClosedLocations().Contains(destination)) {
        return explorer.GetPathTo(destination);
      } else {
        return new List<Location>();
      }
    }
  }
}
