using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public class LevelSuperstate {
    // Even if a unit is on that location, the location will be in here.
    SortedSet<Location> walkableLocations;

    SortedDictionary<Location, Unit> liveUnitByLocation;

    public LevelSuperstate() {
      walkableLocations = new SortedSet<Location>();
      liveUnitByLocation = new SortedDictionary<Location, Unit>();
    }

    public LevelSuperstate(Level level) : this() {
      Reconstruct(level);
    }

    public void Add(Unit unit) {
      liveUnitByLocation.Add(unit.location, unit);
    }

    public bool Remove(Unit unit) {
      return liveUnitByLocation.Remove(unit.location);
    }

    public bool ContainsKey(Location location) {
      return liveUnitByLocation.ContainsKey(location);
    }

    public void Reconstruct(Level level) {
      walkableLocations.Clear();
      foreach (var entry in level.terrain.tiles) {
        var location = entry.Key;
        var terrainTile = entry.Value;
        if (terrainTile.walkable) {
          walkableLocations.Add(location);
        }
      }

      liveUnitByLocation.Clear();
      foreach (var unit in level.units) {
        if (unit.alive) {
          Asserts.Assert(walkableLocations.Contains(unit.location));
          liveUnitByLocation.Add(unit.location, unit);
        }
      }
    }

    public bool IsLocationWalkable(
        Location location,
        bool checkUnitPresent) {
      if (!walkableLocations.Contains(location)) {
        return false;
      }

      if (checkUnitPresent) {
        if (liveUnitByLocation.ContainsKey(location)) {
          return false;
        }
      }

      return true;
    }

    public int NumWalkableLocations(bool checkUnitPresent) {
      if (checkUnitPresent) {
        return walkableLocations.Count - liveUnitByLocation.Count;
      } else {
        return walkableLocations.Count;
      }
    }

    public List<Location> GetNRandomWalkableLocations(
        Rand rand,
        int numToGet,
        SortedSet<Location> forbiddenLocations,
        bool checkUnitPresent) {
      // Gather the candidates
      List<Location> candidates = new List<Location>(walkableLocations.Count);
      foreach (var location in walkableLocations) {
        if (liveUnitByLocation.ContainsKey(location)) {
          continue;
        }
        if (forbiddenLocations.Contains(location)) {
          continue;
        }
        candidates.Add(location);
      }

      // Shuffle the candidates
      int n = candidates.Count;
      while (n > 1) {
        n--;
        int k = rand.Next() % (candidates.Count - 1);
        var value = candidates[k];
        candidates[k] = candidates[n];
        candidates[n] = value;
      }

      var result = new List<Location>(numToGet);
      for (int i = 0; i < numToGet; i++) {
        result.Add(candidates[i]);
      }
      return result;
    }

    public Unit FindNearestLiveUnit(
        Game game,
        Location nearestTo,
        Unit notThisUnit,
        bool goodFilter) {
      Unit nearestEnemy = Unit.Null;
      float distanceToNearestEnemy = 0;
      foreach (var otherUnit in game.level.units) {
        if (otherUnit.Is(notThisUnit))
          continue;
        if (otherUnit.good != goodFilter)
          continue;
        var distanceToOtherUnit =
            game.level.terrain.pattern.DistanceBetween(
                nearestTo, otherUnit.location);
        if (!nearestEnemy.Exists() || distanceToOtherUnit < distanceToNearestEnemy) {
          nearestEnemy = otherUnit;
          distanceToNearestEnemy = distanceToOtherUnit;
        }
      }
      return nearestEnemy;
    }
  }
}
