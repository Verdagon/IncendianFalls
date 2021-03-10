using System;
using Atharia.Model;
using System.Collections.Generic;

namespace IncendianFalls {
  public delegate bool ICanStep(Location from, Location to, float totalCost);
  public delegate bool IStopCondition(Location loc);
  public delegate float IFutureCostGuesser(Location loc);
  public delegate float IDetermineCost(Location from, Location to);

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
          new AStarExplorer(
              pattern,
              new SortedSet<Location>() {startLocation},
              cornersAreAdjacent,
              canStep,
              (a) => a == targetLocation,
              MakeDistanceCostGuesser(pattern, targetLocation),
              (a, b) => pattern.GetTileCenter(a).distance(pattern.GetTileCenter(b)));
      if (explorer.closedLocations.Contains(targetLocation)) {
        return explorer.GetPathTo(targetLocation);
      } else {
        return new List<Location>();
      }
    }

    // Could optimize this... instead of having all these separate maps, combine some of them
    // so that they share cache lines.
    public AStarExplorer(
        Pattern pattern,
        SortedSet<Location> startLocations,
        bool cornersAreAdjacent,
        ICanStep canStep,
        IStopCondition stopCondition,
        IFutureCostGuesser costGuesser,
        IDetermineCost determineCost) {
      //UnityEngine.Debug.Log("Pathing from " + startLocation + " to " + targetLocation);

      // The set of currently discovered nodes that are not evaluated yet.
      // Initially, only the start node is known.
      SortedDictionary<Location, object> openLocationsLowestFFirst =
          new SortedDictionary<Location, object>(
              new LowerFScoreComparer(gFAndCameFromByLocation));

      foreach (var startLocation in startLocations) {
        Asserts.Assert(!stopCondition(startLocation));
        openLocationsLowestFFirst.Add(startLocation, new object());
        gFAndCameFromByLocation[startLocation] =
            new GFAndCameFrom(costGuesser(startLocation), 0, startLocation);
      }
      
      while (openLocationsLowestFFirst.Count > 0) {
        Location currentLocation = DictionaryUtils.GetFirstKey<Location>(openLocationsLowestFFirst);

        if (stopCondition(currentLocation)) {
          return;
        }

        bool removed = openLocationsLowestFFirst.Remove(currentLocation);
        //if (!removed) {
        //  throw new Exception("wtf");
        //}
        closedLocations.Add(currentLocation);

        var neighbors = pattern.GetAdjacentLocations(currentLocation, cornersAreAdjacent);
        foreach (var neighborLocation in neighbors) {
          if (closedLocations.Contains(neighborLocation)) {
            continue;
          }

          float tentativeGScore =
              gFAndCameFromByLocation[currentLocation].gScore +
              determineCost(currentLocation, neighborLocation);
          if (!canStep(currentLocation, neighborLocation, tentativeGScore)) {
            continue;
          }
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
              tentativeGScore + costGuesser(neighborLocation),
              currentLocation);
          openLocationsLowestFFirst.Add(neighborLocation, new object());
        }
      }
      // There was no path.
      return;
    }

    static IFutureCostGuesser MakeDistanceCostGuesser(Pattern pattern, Location target) {
      return (Location from) =>
          pattern.GetTileCenter(from).distance(pattern.GetTileCenter(target));
    }

    public bool WasExplored(Location targetLocation) {
      return closedLocations.Contains(targetLocation);
    }

    public float GetCostTo(Location targetLocation) {
      Asserts.Assert(gFAndCameFromByLocation.ContainsKey(targetLocation));
      return gFAndCameFromByLocation[targetLocation].gScore;
    }

    public List<Location> GetPathTo(Location targetLocation) {
      Asserts.Assert(gFAndCameFromByLocation.ContainsKey(targetLocation));
      Location currentLocation = targetLocation;
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
