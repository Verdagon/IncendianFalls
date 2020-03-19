using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atharia.Model;

namespace IncendianFalls {
  public class EventsClearer {
    public static void Clear(Game game) {
      game.events.Clear();
      //game.root.logger.Error("clearing events for num units " + game.eventedUnits.Count);
      foreach (var eventedUnit in game.eventedUnits) {
        if (eventedUnit.Exists()) {
          //game.root.logger.Error("clearing events for unit, num events: " + eventedUnit.events.Count);
          eventedUnit.events.Clear();
        }
      }
      game.eventedUnits.Clear();
      //game.root.logger.Error("clearing events for num tiles " + game.eventedTerrainTiles.Count);
      foreach (var eventedTerrainTile in game.eventedTerrainTiles) {
        if (eventedTerrainTile.Exists()) {
          //game.root.logger.Error("clearing events for tile, num events: " + eventedTerrainTile.events.Count);
          eventedTerrainTile.events.Clear();
        }
      }
      game.eventedTerrainTiles.Clear();
    }
  }
}
