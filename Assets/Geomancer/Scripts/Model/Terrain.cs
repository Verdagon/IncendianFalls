using System;
using System.Collections.Generic;

namespace Geomancer.Model {
  public class Terrain {
    public readonly Pattern pattern;
    public readonly int elevationStepHeight;
    public readonly SortedDictionary<Location, TerrainTile> tiles;

    public Terrain(Pattern pattern, int elevationStepHeight, SortedDictionary<Location, TerrainTile> tiles) {
      this.pattern = pattern;
      this.elevationStepHeight = elevationStepHeight;
      this.tiles = tiles;
    }
    
    public bool TileExists(Location location) {
      return tiles.ContainsKey(location);
    }

    public List<Location> GetAdjacentExistingLocations(
        
        Location loc,
        bool adjacentCornersToo) {
      List<Location> result = new List<Location>();
      foreach (Location adjacentLoc in pattern.GetAdjacentLocations(loc, adjacentCornersToo)) {
        if (TileExists(adjacentLoc))
          result.Add(adjacentLoc);
      }
      return result;
    }

    public SortedSet<Location> GetAdjacentExistingLocations(
        
        SortedSet<Location> sourceLocs,
        bool includeSourceLocs,
        bool adjacentCornersToo) {
      SortedSet<Location> result = new SortedSet<Location>();
      foreach (Location adjacentLoc in pattern.GetAdjacentLocations(sourceLocs, includeSourceLocs, adjacentCornersToo)) {
        if (!TileExists(adjacentLoc))
          continue;
        result.Add(adjacentLoc);
      }
      return result;
    }

    public Vec3 GetTileCenter( Location loc) {
      var terrainTile = tiles[loc];
      var positionVec2 = pattern.GetTileCenter(loc);
      return new Vec3(
          positionVec2.x,
          positionVec2.y,
          terrainTile.elevation * elevationStepHeight);
    }

    public int GetElevationDifference( Location locA, Location locB) {
      return Math.Abs(
          tiles[locA].elevation -
          tiles[locB].elevation);
    }
  }
}
