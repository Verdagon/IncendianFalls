﻿using System;
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
  public interface IPatternExplorerPrioritizer {
    float GetPriority(Location location, Vec2 position);
  }

  public delegate bool PatternExplorerFilter(Location location, Vec2 position);

  public class ApatheticPrioritizer : IPatternExplorerPrioritizer {
    public float GetPriority(Location location, Vec2 position) {
      return 0;
    }
  }

  public class LessDistanceFromPrioritizer : IPatternExplorerPrioritizer {
    Vec2 from;
    public LessDistanceFromPrioritizer(Vec2 from) {
      this.from = from;
    }
    public float GetPriority(Location location, Vec2 position) {
      // - because higher is first
      return -position.distance(from);
    }
  }

  //if (distanceA == distanceB) {
  //  return (new Location.BeforeComparer()).Compare(a, b);
  //}

  public class PatternExplorer {
    private struct PriorityAndLocation {
      public struct Comparer : IComparer<PriorityAndLocation> {
        public int Compare(PriorityAndLocation a, PriorityAndLocation b) {
          if (a.priority != b.priority) {
            // - because higher is first
            return -Math.Sign(a.priority - b.priority);
          }
          // Sometimes we get ties, for example between (-1, 0, 0) and (1, 0, 0)
          // relative to (0, 0, 0).
          return (new Location.Comparer()).Compare(a.location, b.location);
        }
      }

      public readonly float priority;
      public readonly Location location;

      public PriorityAndLocation(float priority, Location location) {
        this.priority = priority;
        this.location = location;
      }
    }

    Pattern pattern;
    bool considerCornersAdjacent;
    IPatternExplorerPrioritizer prioritizer;
    PatternExplorerFilter filter;
    SortedDictionary<Location, object> exploredPoints = new SortedDictionary<Location, object>();
    SortedDictionary<Location, object> unexploredPoints = new SortedDictionary<Location, object>();
    SortedDictionary<PriorityAndLocation, object> unexploredPointsPrioritized;

    public PatternExplorer(SSContext context, Pattern pattern, bool considerCornersAdjacent, Location originLocation) :
    this(
      context,
        pattern,
        considerCornersAdjacent,
        originLocation,
        new LessDistanceFromPrioritizer(pattern.GetTileCenter(originLocation)),
        (Location, Vec2) => { return true; }) {
    }

    public PatternExplorer(
      SSContext context,
        Pattern pattern,
        bool considerCornersAdjacent,
        Location originLocation,
        IPatternExplorerPrioritizer prioritizer,
        PatternExplorerFilter filter) {
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      this.pattern = pattern;
      this.considerCornersAdjacent = considerCornersAdjacent;
      this.prioritizer = prioritizer;
      this.filter = filter;
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      unexploredPointsPrioritized =
          new SortedDictionary<PriorityAndLocation, object>(
              new PriorityAndLocation.Comparer());
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      unexploredPoints.Add(originLocation, new object());
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      unexploredPointsPrioritized.Add(
          new PriorityAndLocation(0, originLocation),
          new object());
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      Asserts.Assert(filter(originLocation, pattern.GetTileCenter(originLocation)));
      context.Flare(context.root.GetDeterministicHashCode().ToString());

    }

    public bool Next(out Location outLocation, out float outPriority, SSContext context) {
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      // This only has one iteration
      PriorityAndLocation priorityAndLocation = new PriorityAndLocation(0, new Location(0, 0, 0));
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      if (unexploredPointsPrioritized.Count == 0) {
        outLocation = null;
        outPriority = 0;
        return false;
      }
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      foreach (var locEntry in unexploredPointsPrioritized) {
        priorityAndLocation = locEntry.Key;
        break;
      }
      var loc = priorityAndLocation.location;

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      foreach (Location adjacentLoc in pattern.GetAdjacentLocations(loc, considerCornersAdjacent)) {
        context.Flare(context.root.GetDeterministicHashCode().ToString());

        var center = pattern.GetTileCenter(adjacentLoc);
        if (!filter(adjacentLoc, center))
          continue;
        var priority = prioritizer.GetPriority(adjacentLoc, center);
        if (!exploredPoints.ContainsKey(adjacentLoc) &&
            !unexploredPoints.ContainsKey(adjacentLoc)) {
          unexploredPoints.Add(adjacentLoc, new object());
          unexploredPointsPrioritized.Add(
              new PriorityAndLocation(priority, adjacentLoc),
              new object());
        }
      }

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      exploredPoints.Add(loc, new object());
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      unexploredPointsPrioritized.Remove(priorityAndLocation);
      unexploredPoints.Remove(loc);
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      outLocation = loc;
      outPriority = priorityAndLocation.priority;
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      return true;
    }

    public Location Next(SSContext context) {
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      Location loc = new Location(0, 0, 0);
      float priority = 0;
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      if (Next(out loc, out priority, context)) {
        context.Flare(context.root.GetDeterministicHashCode().ToString());
        return loc;
      } else {
        context.Flare(context.root.GetDeterministicHashCode().ToString());

        throw new Exception("No next element!");
      }
    }

    public delegate bool ILocationPredicate(Location location);

    public List<Location> ExploreWhile(
      SSContext context,
        ILocationPredicate predicate,
        int maxLocations = -1,
        float scoreThreshold = float.NegativeInfinity) {
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      List<Location> foundLocations = new List<Location>();
      for (int j = 0; ; j++) {
        context.Flare(context.root.GetDeterministicHashCode().ToString());

        if (foundLocations.Count > 10000) {
          throw new Exception("Pattern explorer out of control!");
        }
        if (maxLocations != -1 && j >= maxLocations) {
          break;
        }
        context.Flare(context.root.GetDeterministicHashCode().ToString());

        Location nextLocation = new Location(0, 0, 0);
        float nextScore = 0;
        Next(out nextLocation, out nextScore, context);
        if (nextScore < scoreThreshold) {
          break;
        }
        context.Flare(context.root.GetDeterministicHashCode().ToString());

        if (predicate(nextLocation)) {
          foundLocations.Add(nextLocation);
        } else {
          break;
        }
        context.Flare(context.root.GetDeterministicHashCode().ToString());

      }
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      return foundLocations;
    }
  }
}
