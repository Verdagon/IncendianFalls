using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atharia.Model;

namespace IncendianFalls {
  public class EventsClearer {
    public static void Clear(Game game) {
      game.events.Clear();
      foreach (var eventedUnit in game.eventedUnits) {
        eventedUnit.events.Clear();
      }
      game.eventedUnits.Clear();
      foreach (var eventedTerrainTile in game.eventedTerrainTiles) {
        eventedTerrainTile.events.Clear();
      }
      game.eventedTerrainTiles.Clear();
    }
  }
}
