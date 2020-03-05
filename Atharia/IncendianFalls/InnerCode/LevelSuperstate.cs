using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  // Serves as a caching mechanism for the level.
  public class LevelSuperstate {
    public readonly Level level;

    // Even if a unit is on that location, the location will be in here.
    SortedSet<Location> walkableLocations;

    SortedDictionary<Location, Unit> liveUnitByLocation;

    //public LevelSuperstate() {
    //}

    public LevelSuperstate(Level level) {
      this.level = level;
      walkableLocations = new SortedSet<Location>();
      liveUnitByLocation = new SortedDictionary<Location, Unit>();
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

    public Unit FindLiveUnit(string classId) {
      foreach (var locationAndUnit in liveUnitByLocation) {
        if (locationAndUnit.Value.classId == classId) {
          return locationAndUnit.Value;
        }
      }
      Asserts.Assert(false);
      return Unit.Null;
    }

    public void RemoveSimplePresenceTriggers(string name, int expectAtLeast) {
      foreach (var locationAndTrigger in FindSimplePresenceTriggers(name, expectAtLeast)) {
        level.terrain.tiles[locationAndTrigger.Key].components.Remove(locationAndTrigger.Value.AsITerrainTileComponent());
        locationAndTrigger.Value.Destruct();
      }
    }

    public void RemoveMarkers(string name, int expectAtLeast) {
      foreach (var locationAndMarker in FindMarkers(name, expectAtLeast)) {
        level.terrain.tiles[locationAndMarker.Key].components.Remove(locationAndMarker.Value.AsITerrainTileComponent());
        locationAndMarker.Value.Destruct();
      }
    }

    public Location FindMarkerLocation(string name) {
      return FindMarker(name).Key;
    }

    public KeyValuePair<Location, MarkerTTC> FindMarker(string name) {
      return FindMarkers(name, 1)[0];
    }

    public List<KeyValuePair<Location, MarkerTTC>> FindMarkers(string name, int expectAtLeast) {
      var locationsAndMarkers = new List<KeyValuePair<Location, MarkerTTC>>();
      foreach (var locationAndTile in level.terrain.tiles) {
        foreach (var marker in locationAndTile.Value.components.GetAllMarkerTTC()) {
          if (marker.name == name) {
            locationsAndMarkers.Add(new KeyValuePair<Location, MarkerTTC>(locationAndTile.Key, marker));
          }
        }
      }
      Asserts.Assert(locationsAndMarkers.Count >= expectAtLeast, "Couldn't find enough markers with name " + name);
      return locationsAndMarkers;
    }

    public Location FindSimplePresenceTriggerLocation(string name) {
      return FindSimplePresenceTrigger(name).Key;
    }

    public KeyValuePair<Location, SimplePresenceTriggerTTC> FindSimplePresenceTrigger(string name) {
      return FindSimplePresenceTriggers(name, 1)[0];
    }

    public List<KeyValuePair<Location, SimplePresenceTriggerTTC>> FindSimplePresenceTriggers(string name, int expectAtLeast) {
      var locationsAndTriggers = new List<KeyValuePair<Location, SimplePresenceTriggerTTC>>();
      foreach (var locationAndTile in level.terrain.tiles) {
        foreach (var trigger in locationAndTile.Value.components.GetAllSimplePresenceTriggerTTC()) {
          if (trigger.name == name) {
            locationsAndTriggers.Add(new KeyValuePair<Location, SimplePresenceTriggerTTC>(locationAndTile.Key, trigger));
          }
        }
      }
      Asserts.Assert(locationsAndTriggers.Count >= expectAtLeast, "Couldn't find enough markers with name " + name);
      return locationsAndTriggers;
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
