using System;
using Atharia.Model;
using System.Collections.Generic;

namespace IncendianFalls {
  // This is used to prioritize which ones to explore first.
  // Numbers that are higher are explored with more interest.
  // For example, ApatheticPatternExplorerPrioritizer treats all locations
  // equally and therefore just spreads out in a breadth-first search (which
  // is almost a circle but not exactly).
  // To get an exact circle, you'd use LessDistanceFromPrioritizer which
  // returns the negative distance from the start. It's negative because
  // higher numbers are explored first.
  // To make a square, make one that returns the difference between the X
  // distance and Y distance from the start.

  //if (distanceA == distanceB) {
  //  return (new Location.BeforeComparer()).Compare(a, b);
  //}

  public class AStarExplorer {
    private class LowerFScoreComparer : IComparer<Location> {
      SortedDictionary<Location, GFAndCameFrom> fGAndCameFromByLocation;
      public LowerFScoreComparer(SortedDictionary<Location, GFAndCameFrom> fGAndCameFromByLocation) {
        this.fGAndCameFromByLocation = fGAndCameFromByLocation;
      }
      public int Compare(Location a, Location b) {
        float aF = float.PositiveInfinity;
        if (fGAndCameFromByLocation.ContainsKey(a)) {
          aF = fGAndCameFromByLocation[a].fScore;
        }
        float bF = float.PositiveInfinity;
        if (fGAndCameFromByLocation.ContainsKey(b)) {
          bF = fGAndCameFromByLocation[b].fScore;
        }
        if (aF != bF) {
          return Math.Sign(aF - bF);
        }
        return a.CompareTo(b);
      }
    }

    private struct GFAndCameFrom {
      public readonly float gScore;
      public readonly float fScore;
      public readonly Location cameFrom;
      public GFAndCameFrom(float gScore, float fScore, Location cameFrom) {
        this.gScore = gScore;
        this.fScore = fScore;
        this.cameFrom = cameFrom;
      }
    }

    // Could optimize this... instead of having all these separate maps, combine some of them
    // so that they share cache lines.
    public static List<Location> Go(
        Terrain terrain,
        Location startLocation,
        Location targetLocation,
        bool cornersAreAdjacent,
        bool limit2ElevationDifference,
        string tileClassIdFilter) {
      //UnityEngine.Debug.Log("Pathing from " + startLocation + " to " + targetLocation);

      if (startLocation == targetLocation) {
        throw new Exception("wat");
      }
      // The set of nodes already evaluated
      var closedLocations = new SortedSet<Location>();
      // For each node:
      // - cameFrom: which node it can most efficiently be reached from.
      //   If a node can be reached from many nodes, cameFrom will eventually contain the
      //   most efficient previous step.
      // - g: the cost of getting from the start node to that node.
      // - f: the total cost of getting from the start node to the goal
      //   by passing by that node. That value is partly known, partly heuristic.
      SortedDictionary<Location, GFAndCameFrom> gFAndCameFromByLocation = new SortedDictionary<Location, GFAndCameFrom>();
      // The set of currently discovered nodes that are not evaluated yet.
      // Initially, only the start node is known.
      SortedDictionary<Location, object> openLocationsLowestFFirst =
          new SortedDictionary<Location, object>(
              new LowerFScoreComparer(gFAndCameFromByLocation));

      openLocationsLowestFFirst.Add(startLocation, new object());

      gFAndCameFromByLocation[startLocation] =
          new GFAndCameFrom(
              EstimateHeuristicCost(terrain.pattern, startLocation, targetLocation),
              0,
              startLocation);


      while (openLocationsLowestFFirst.Count > 0) {
        //UnityEngine.Debug.Log("remaining in open: " + openLocationsLowestFFirst.Count);
        Location currentLocation =
            DictionaryUtils.GetFirstKey<Location>(openLocationsLowestFFirst);
        //UnityEngine.Debug.Log("current is " + currentLocation + " f score " + fGAndCameFromByLocation[currentLocation].fScore);

        if (currentLocation == targetLocation) {
          return ReconstructPath(gFAndCameFromByLocation, currentLocation);
        }

        bool removed = openLocationsLowestFFirst.Remove(currentLocation);
        //if (!removed) {
        //  throw new Exception("wtf");
        //}
        closedLocations.Add(currentLocation);

        var neighbors = terrain.pattern.GetAdjacentLocations(currentLocation, cornersAreAdjacent);
        foreach (var neighborLocation in neighbors) {
          if (!terrain.tiles.ContainsKey(neighborLocation)) {
            continue;
          }
          if (!terrain.tiles[neighborLocation].walkable) {
            continue;
          }
          int elevationDifference = terrain.GetElevationDifference(neighborLocation, currentLocation);
          if (limit2ElevationDifference && elevationDifference > 2) {
            continue;
          }
          if (tileClassIdFilter.Length > 0 && terrain.tiles[neighborLocation].classId != tileClassIdFilter) {
            continue;
          }
          if (closedLocations.Contains(neighborLocation)) {
            continue;
          }
          float tentativeGScore =
              gFAndCameFromByLocation[currentLocation].gScore +
              terrain.pattern.GetTileCenter(currentLocation)
                  .distance(terrain.pattern.GetTileCenter(neighborLocation));
          if (!openLocationsLowestFFirst.ContainsKey(neighborLocation)) {
            // Discovered a new node
            openLocationsLowestFFirst.Add(neighborLocation, new object());
          } else if (tentativeGScore >= gFAndCameFromByLocation[neighborLocation].gScore) {
            continue;
          }

          // This path is the best until now. Record it!

          // Since the comparer depends on gFAndCameFromByLocation, we need to make it recompare.
          // To do that, we remove and afterwards re-add.
          openLocationsLowestFFirst.Remove(neighborLocation);
          gFAndCameFromByLocation[neighborLocation] =
            new GFAndCameFrom(
              tentativeGScore,
              tentativeGScore +
                  EstimateHeuristicCost(terrain.pattern, neighborLocation, targetLocation),
              currentLocation);
          openLocationsLowestFFirst.Add(neighborLocation, new object());
        }
      }
      // There was no path.
      return new List<Location>();
    }

    static float EstimateHeuristicCost(
        Pattern pattern,
        Location fromHere,
        Location toHere) {
      return pattern.GetTileCenter(fromHere).distance(
          pattern.GetTileCenter(toHere));
    }

    static List<Location> ReconstructPath(
        SortedDictionary<Location, GFAndCameFrom> cameFromByLocation,
        Location currentLocation) {
      List<Location> path = new List<Location>();
      while (true) {
        if (cameFromByLocation[currentLocation].cameFrom == currentLocation) {
          path.Reverse();
          return path;
        }
        path.Add(currentLocation);
        currentLocation = cameFromByLocation[currentLocation].cameFrom;
      }
    }
  }
}
