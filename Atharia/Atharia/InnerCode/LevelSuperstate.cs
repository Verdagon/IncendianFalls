using System;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public delegate bool LocationPredicate(Location location);

  // Serves as a caching mechanism for the level.
  public class LevelSuperstate {
    public readonly Level level;

    // Even if a unit is on that location, the location will be in here.
    SortedSet<Location> walkableLocations;
    SortedDictionary<Location, SortedSet<Location>> locToHoppableLocs;
    SortedDictionary<Location, SortedDictionary<Location, List<Location>>> locToNearbyLocToPath;
    SortedDictionary<Location, SortedSet<Location>> locToVisibleLocs;

    SortedDictionary<Location, Unit> liveUnitByLocation;
    SortedSet<Location> noUnitLocations;

    SortedSet<Location> locationsWithActingTTCs;

    //public LevelSuperstate() {
    //}

    public LevelSuperstate(Level level) {
      this.level = level;
      walkableLocations = new SortedSet<Location>();
      locToHoppableLocs = new SortedDictionary<Location, SortedSet<Location>>();
      locToNearbyLocToPath = new SortedDictionary<Location, SortedDictionary<Location, List<Location>>>();
      locToVisibleLocs = new SortedDictionary<Location, SortedSet<Location>>();
      liveUnitByLocation = new SortedDictionary<Location, Unit>();
      locationsWithActingTTCs = new SortedSet<Location>();
      noUnitLocations = new SortedSet<Location>();
      ReconstructExpensive(level);
    }

    public void ReconstructExpensive(Level level) {
      walkableLocations.Clear();
      locationsWithActingTTCs.Clear();
      locToHoppableLocs.Clear();
      locToVisibleLocs.Clear();
      locToNearbyLocToPath.Clear();
      liveUnitByLocation.Clear();
      foreach (var entry in level.terrain.tiles) {
        var location = entry.Key;
        var terrainTile = entry.Value;
        if (terrainTile.IsWalkable()) {
          walkableLocations.Add(location);
        }
        if (terrainTile.components.GetAllIActingTTC().Count > 0) {
          locationsWithActingTTCs.Add(location);
        }
      }

      // var startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
      
      var locAndHoppableLocPairs = new ConcurrentBag<(Location, SortedSet<Location>)>();
      // foreach (var location in level.terrain.tiles.Keys) {
        Parallel.ForEach(level.terrain.tiles.Keys, location => {
        var visibleLocs = Actions.GetHoppableLocationsExpensive(this, level, location, false);
        locAndHoppableLocPairs.Add((location, visibleLocs));
        });
      // }
      foreach (var pair in locAndHoppableLocPairs) {
        locToHoppableLocs.Add(pair.Item1, pair.Item2);
      }

      var locAndVisibleLocPairs = new ConcurrentBag<(Location, SortedSet<Location>)>();
      // foreach (var location in level.terrain.tiles.Keys) {
        Parallel.ForEach(level.terrain.tiles.Keys, location => {
        var visibleLocs = Sight.GetVisibleLocationsExpensive(level.terrain, location);
        locAndVisibleLocPairs.Add((location, visibleLocs));
        });
      // }
      foreach (var pair in locAndVisibleLocPairs) {
        locToVisibleLocs.Add(pair.Item1, pair.Item2);
      }
      
      var locAndNearbyLocPairs = new ConcurrentBag<(Location, SortedDictionary<Location, List<Location>>)>();
      // foreach (var location in level.terrain.tiles.Keys) {
        Parallel.ForEach(level.terrain.tiles.Keys, location => {
        var visibleLocs = Navigation.GetNearbyLocationPathsExpensive(this, location);
        locAndNearbyLocPairs.Add((location, visibleLocs));
        });
      // }
      foreach (var pair in locAndNearbyLocPairs) {
        locToNearbyLocToPath.Add(pair.Item1, pair.Item2);
      }
      
      // var endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
      // Console.WriteLine("Duration: " + (endTime - startTime));
      
      foreach (var unit in level.units) {
        if (unit.Alive()) {
          Asserts.Assert(walkableLocations.Contains(unit.location));
          liveUnitByLocation.Add(unit.location, unit);
        }
      }
    }

    public SortedSet<Location> GetHoppableLocs(Location source, bool checkUnitNotPresent) {
      Asserts.Assert(level.terrain.TileExists(source));
      var locs = new SortedSet<Location>(locToHoppableLocs[source]);
      if (checkUnitNotPresent) {
        foreach (var loc in locToHoppableLocs[source]) {
          if (LocationContainsUnit(loc)) {
            locs.Remove(loc);
          }
        }
      }
      return locs;
    }
    
    public SortedSet<Location> GetVisibleLocs(Location source) {
      return new SortedSet<Location>(locToVisibleLocs[source]);
    }

    public bool CanSee(Location source, Location destination) {
      return locToVisibleLocs[source].Contains(destination);
    }
    
    public List<Location> FindPath(Location source, Location destination) {
      if (locToNearbyLocToPath.TryGetValue(
          source, out SortedDictionary<Location, List<Location>> nearbyLocToPath)) {
        if (nearbyLocToPath.TryGetValue(destination, out List<Location> path)) {
          return new List<Location>(path);
        }
      }
      return Navigation.FindPathExpensive(level.terrain, this, source, destination);
    }

    public bool CanHop(Location source, Location destination, bool checkUnitNotPresent) {
      if (!locToHoppableLocs[source].Contains(destination)) {
        return false;
      }
      if (checkUnitNotPresent && LocationContainsUnit(destination)) {
        return false;
      }
      return true;
    }

    public void AddedActingTTC(Location location) {
      if (!locationsWithActingTTCs.Contains(location)) {
        locationsWithActingTTCs.Add(location);
      }
    }

    public void RemovedActingTTC(Location location) {
      if (level.terrain.tiles[location].components.GetAllIActingTTC().Count == 0) {
        locationsWithActingTTCs.Remove(location);
      }
    }

    public bool IsNoUnitLocation(Location loc) {
      return noUnitLocations.Contains(loc);
    }

    public void AddNoUnitZone(Location loc, int radiusIncludingCenter) {
      var noUnitZone = new SortedSet<Location>();
      noUnitZone.Add(loc);
      for (int i = 1; i < radiusIncludingCenter; i++) {
        noUnitZone = level.terrain.GetAdjacentExistingLocations(noUnitZone, true, false);
      }
      SetUtils.AddAll(noUnitLocations, noUnitZone);
    }

    public List<KeyValuePair<Location, IActingTTC>> GetAllActingTTCs() {
      var actingTTCs = new List<KeyValuePair<Location, IActingTTC>>();
      foreach (var location in locationsWithActingTTCs) {
        foreach (var actingTTC in level.terrain.tiles[location].components.GetAllIActingTTC()) {
          actingTTCs.Add(new KeyValuePair<Location, IActingTTC>(location, actingTTC));
        }
      }
      return actingTTCs;
    }

    public void AddUnit(Unit unit) {
      liveUnitByLocation.Add(unit.location, unit);
    }

    public bool RemoveUnit(Unit unit) {
      return liveUnitByLocation.Remove(unit.location);
    }

    public bool ContainsUnit(Unit unit) {
      return liveUnitByLocation.ContainsKey(unit.location) && liveUnitByLocation[unit.location].NullableIs(unit);
    }

    public bool LocationContainsUnit(Location location) {
      return liveUnitByLocation.ContainsKey(location);
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
        LocationPredicate filter,
        bool checkNoUnitLocation,
        bool checkUnitPresent) {
      // Gather the candidates
      var candidates = new SortedSet<Location>();
      foreach (var location in walkableLocations) {
        if (!filter(location)) {
          continue;
        }
        if (checkUnitPresent && liveUnitByLocation.ContainsKey(location)) {
          continue;
        }
        if (checkNoUnitLocation && IsNoUnitLocation(location)) {
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
        LocationPredicate filter,
        bool checkNoUnitLocation,
        bool checkUnitPresent) {
      var candidates = GetWalkableLocations(terrain, filter, checkNoUnitLocation, checkUnitPresent);
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

    public List<Location> FindMarkersLocations(string name, int expectAtLeast) {
      var locations = new List<Location>();
      foreach (var locationAndTile in FindMarkers(name, expectAtLeast)) {
        locations.Add(locationAndTile.Key);
      }
      return locations;
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

    public List<Location> FindSimplePresenceTriggerLocations(string name, int expectAtLeast) {
      var locations = new List<Location>();
      foreach (var locationAndTile in FindSimplePresenceTriggers(name, expectAtLeast)) {
        locations.Add(locationAndTile.Key);
      }
      return locations;
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
      // When changing this, remember that theres sometimes Challenging units across the
      // map that we dont want to miss.
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
