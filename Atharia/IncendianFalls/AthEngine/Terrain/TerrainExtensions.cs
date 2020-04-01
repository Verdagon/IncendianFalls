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

  public static class TerrainExtensions {
    public static Atharia.Model.Void Destruct(
        this Terrain obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static bool TileExists(
        this Terrain terrain,
        Location location) {
      return terrain.tiles.ContainsKey(location);
    }

    public static List<Location> GetAdjacentExistingLocations(
        this Terrain terrain,
        Location loc,
        bool adjacentCornersToo) {
      List<Location> result = new List<Location>();
      foreach (Location adjacentLoc in terrain.pattern.GetAdjacentLocations(loc, adjacentCornersToo)) {
        if (terrain.TileExists(adjacentLoc))
          result.Add(adjacentLoc);
      }
      return result;
    }

    public static SortedSet<Location> GetAdjacentExistingLocations(
        this Terrain terrain,
        SortedSet<Location> sourceLocs,
        bool includeSourceLocs,
        bool adjacentCornersToo) {
      SortedSet<Location> result = new SortedSet<Location>();
      foreach (Location adjacentLoc in terrain.pattern.GetAdjacentLocations(sourceLocs, includeSourceLocs, adjacentCornersToo)) {
        if (!terrain.TileExists(adjacentLoc))
          continue;
        result.Add(adjacentLoc);
      }
      return result;
    }

    public static Vec3 GetTileCenter(this Terrain terrain, Location loc) {
      var terrainTile = terrain.tiles[loc];
      var positionVec2 = terrain.pattern.GetTileCenter(loc);
      return new Vec3(
          positionVec2.x,
          positionVec2.y,
          terrainTile.elevation * terrain.elevationStepHeight);
    }

    public static int GetElevationDifference(this Terrain terrain, Location locA, Location locB) {
      return Math.Abs(
          terrain.tiles[locA].elevation -
          terrain.tiles[locB].elevation);
    }
  }
}
