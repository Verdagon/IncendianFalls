using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public class LiveUnitByLocationMap {
    SortedDictionary<Location, Unit> liveUnitByLocation;

    public LiveUnitByLocationMap() {
      liveUnitByLocation = new SortedDictionary<Location, Unit>();
    }

    public LiveUnitByLocationMap(Game game) : this() {
      Reconstruct(game);
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

    public void Reconstruct(Game game) {
      liveUnitByLocation.Clear();
      foreach (var unit in game.level.units) {
        if (unit.alive) {
          liveUnitByLocation.Add(unit.location, unit);
        }
      }
    }
  }
}
