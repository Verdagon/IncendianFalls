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
        if (terrainTile.IsWalkable()) {
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

    public SortedSet<Location> GetWalkableLocations(
        Terrain terrain,
        bool checkLevelLinkPresent,
        bool checkUnitPresent) {
      // Gather the candidates
      var candidates = new SortedSet<Location>();
      foreach (var location in walkableLocations) {
        if (checkUnitPresent && liveUnitByLocation.ContainsKey(location)) {
          continue;
        }
        if (checkLevelLinkPresent &&
            terrain.tiles[location].components.GetAllLevelLinkTTC().Count > 0) {
          continue;
        }
        candidates.Add(location);
      }
      return candidates;
    }
    public List<Location> GetNRandomWalkableLocations(
        Terrain terrain,
        Rand rand,
        int numToGet,
        bool checkLevelLinkPresent,
        bool checkUnitPresent) {
      var candidates = GetWalkableLocations(terrain, checkLevelLinkPresent, checkUnitPresent);
      // Shuffle the candidates four times. For some reason if we don't do this
      // most the units are on the left side of the map.
      return ListUtils.GetRandomN(new List<Location>(candidates), rand, 4, numToGet);
    }

    public Unit GetLiveUnitAt(Location loc) {
      if (liveUnitByLocation.TryGetValue(loc, out Unit result)) {
        return result;
      } else {
        return Unit.Null;
      }
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
