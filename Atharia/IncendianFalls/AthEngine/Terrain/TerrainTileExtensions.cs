using System;
using System.Collections.Generic;

namespace Atharia.Model {
	/*
	struct Terrain node(NobiliaModel) {
	  pattern: Pattern;
	  tileset: Tileset;
	  elevationStepHeight: F32;

	  tiles: !Map:(Location, TerrainTile, locationBefore);
	}
	*/

  public static class TerrainTileExtensions {
    public static Atharia.Model.Void Destruct(this TerrainTile obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static bool IsWalkable(this TerrainTile obj) {
      return obj.components.GetAllIUnwalkableTTC().Count == 0;
    }

    public static void AddEvent(this TerrainTile terrainTile, Game game, ITerrainTileEvent e) {
      game.eventedTerrainTiles.Add(terrainTile);
      terrainTile.events.Add(e);
    }
  }
}
