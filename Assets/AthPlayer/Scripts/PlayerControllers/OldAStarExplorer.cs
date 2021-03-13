using System;
using Atharia.Model;
using System.Collections.Generic;


namespace IncendianFalls {
  public class OldAStarExplorer {
    public delegate bool ICanStep(Location from, Location to);

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

    // The set of nodes already evaluated
    private SortedSet<Location> closedLocations = new SortedSet<Location>();
    public SortedSet<Location> getClosedLocations() { return closedLocations; }
    
    // For each node:
    // - cameFrom: which node it can most efficiently be reached from.
    //   If a node can be reached from many nodes, cameFrom will eventually contain the
    //   most efficient previous step.
    // - g: the cost of getting from the start node to that node.
    // - f: the total cost of getting from the start node to the goal
    //   by passing by that node. That value is partly known, partly heuristic.
    SortedDictionary<Location, GFAndCameFrom> gFAndCameFromByLocation = new SortedDictionary<Location, GFAndCameFrom>();

    public static List<Location> Go(
        Pattern pattern,
        Location startLocation,
        Location targetLocation,
        bool cornersAreAdjacent,
        ICanStep canStep) {
      var explorer =
          new OldAStarExplorer(
              pattern,
              startLocation,
              targetLocation,
              cornersAreAdjacent,
              canStep);
      if (explorer.closedLocations.Contains(targetLocation)) {
        return explorer.ReconstructPath(targetLocation);
      } else {
        return new List<Location>();
      }
    }

    // Could optimize this... instead of having all these separate maps, combine some of them
    // so that they share cache lines.
    public OldAStarExplorer(
        Pattern pattern,
        Location startLocation,
        Location targetLocation,
        bool cornersAreAdjacent,
        ICanStep canStep) {
      //UnityEngine.Debug.Log("Pathing from " + startLocation + " to " + targetLocation);

      if (startLocation == targetLocation) {
        throw new Exception("wat");
      }
      // The set of nodes already evaluated
      // var closedLocations = new SortedSet<Location>();
      // For each node:
      // - cameFrom: which node it can most efficiently be reached from.
      //   If a node can be reached from many nodes, cameFrom will eventually contain the
      //   most efficient previous step.
      // - g: the cost of getting from the start node to that node.
      // - f: the total cost of getting from the start node to the goal
      //   by passing by that node. That value is partly known, partly heuristic.
      // SortedDictionary<Location, GFAndCameFrom> gFAndCameFromByLocation = new SortedDictionary<Location, GFAndCameFrom>();
      // The set of currently discovered nodes that are not evaluated yet.
      // Initially, only the start node is known.
      SortedDictionary<Location, object> openLocationsLowestFFirst =
          new SortedDictionary<Location, object>(
              new LowerFScoreComparer(gFAndCameFromByLocation));

      openLocationsLowestFFirst.Add(startLocation, new object());

      gFAndCameFromByLocation[startLocation] =
          new GFAndCameFrom(
              EstimateHeuristicCost(pattern, startLocation, targetLocation),
              0,
              startLocation);


      while (openLocationsLowestFFirst.Count > 0) {
        //UnityEngine.Debug.Log("remaining in open: " + openLocationsLowestFFirst.Count);
        Location currentLocation =
            DictionaryUtils.GetFirstKey<Location>(openLocationsLowestFFirst);
        //UnityEngine.Debug.Log("current is " + currentLocation + " f score " + fGAndCameFromByLocation[currentLocation].fScore);

        bool removed = openLocationsLowestFFirst.Remove(currentLocation);
        //if (!removed) {
        //  throw new Exception("wtf");
        //}
        closedLocations.Add(currentLocation);

        if (currentLocation == targetLocation) {
          return;
        }

        var neighbors = pattern.GetAdjacentLocations(currentLocation, cornersAreAdjacent);
        foreach (var neighborLocation in neighbors) {
          if (!canStep(currentLocation, neighborLocation)) {
            continue;
          }
          if (closedLocations.Contains(neighborLocation)) {
            continue;
          }
          float tentativeGScore =
              gFAndCameFromByLocation[currentLocation].gScore +
              pattern.GetTileCenter(currentLocation)
                  .distance(pattern.GetTileCenter(neighborLocation));
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
                  EstimateHeuristicCost(pattern, neighborLocation, targetLocation),
              currentLocation);
          openLocationsLowestFFirst.Add(neighborLocation, new object());
        }
      }
      // There was no path.
      return;
    }

    static float EstimateHeuristicCost(
        Pattern pattern,
        Location fromHere,
        Location toHere) {
      return pattern.GetTileCenter(fromHere).distance(
          pattern.GetTileCenter(toHere));
    }

    List<Location> ReconstructPath(
        Location currentLocation) {
      List<Location> path = new List<Location>();
      while (true) {
        if (gFAndCameFromByLocation[currentLocation].cameFrom == currentLocation) {
          path.Reverse();
          return path;
        }
        path.Add(currentLocation);
        currentLocation = gFAndCameFromByLocation[currentLocation].cameFrom;
      }
    }
  }
}
