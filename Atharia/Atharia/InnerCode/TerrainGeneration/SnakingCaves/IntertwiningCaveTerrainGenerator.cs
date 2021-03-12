using System;
using System.Collections.Generic;
using System.Diagnostics;
using Atharia.Model;

namespace IncendianFalls {
  public class SnakingCaveTerrainGenerator {
    public const int PATH_HEIGHT = 5;
    public const int WATER_HEIGHT = 1;
    
    public static Terrain Generate(
        SSContext context,
        Root root,
        Pattern pattern,
        Rand rand,
        bool considerCornersAdjacent,
        float radius) {
      float elevationStepHeight = .2f;

      var terrain =
          root.EffectTerrainCreate(
              pattern,
              elevationStepHeight,
              root.EffectTerrainTileByLocationMutMapCreate());

      var originLocation = new Location(0, 0, 0);
      
      // Get a random location in the level's circle.
      var circleLocs = GetCircle(context, pattern, new Location(0, 0, 0), radius);

      var (mainLocs, sideLocs) = SnakeDirector.addSnakes(rand, terrain, considerCornersAdjacent, originLocation, radius, circleLocs);
      
      var overlayLocs = Bridger.addBridgesAndOverlay(terrain, considerCornersAdjacent, circleLocs, mainLocs, sideLocs);

      foreach (var circleLoc in circleLocs) {
        if (!terrain.TileExists(circleLoc)) {
          AddTile(terrain, circleLoc, WATER_HEIGHT, terrain.root.EffectWaterTTCCreate().AsITerrainTileComponent());
        }
      }

      var nonOverlayLocs = new SortedSet<Location>(circleLocs);
      SetUtils.RemoveAll(nonOverlayLocs, overlayLocs);
      var explorer =
          new AStarExplorer(
              pattern,
              nonOverlayLocs,
              true,
              (a, b, totalCost) => overlayLocs.Contains(b),
              (b) => false, // Dont stop early
              (a) => 0, // No goal
              (a, b) => 1); // Each step costs 1
      foreach (var loc in explorer.getClosedLocations()) {
        var tile = terrain.tiles[loc];
        if (explorer.GetCostTo(loc) <= 1.5) {
          // do nothing
        } else if (explorer.GetCostTo(loc) <= 2.5) {
          // randomly put stalagmites in occasionally
          if (rand.Next() % 5 == 0) {
            tile.components.Add(terrain.root.EffectTreeTTCCreate().AsITerrainTileComponent());
          }
        } else {
          foreach (var mud in terrain.tiles[loc].components.GetAllGrassTTC()) {
            tile.components.Remove(mud.AsITerrainTileComponent());
            mud.Destruct();
          }
          tile.components.Add(terrain.root.EffectWaterTTCCreate().AsITerrainTileComponent());
          tile.elevation = WATER_HEIGHT;
        }
      }
      
      // we'll need to make any locations next to ADEH -1, not just IJKL. See BridgeMaking2.png for an example.

      return terrain;
    }

    public static SortedSet<Location> GetCircle(SSContext context, Pattern pattern, Location originLocation, float radius) {
      var searcher = new PatternExplorer(context, pattern, false, originLocation);
      var locationsSoFar = new SortedSet<Location>();
      while (true) {
        Location loc = searcher.Next(context);
        Vec2 center = pattern.GetTileCenter(loc);
        if (center.distance(new Vec2(0, 0)) <= radius) {
          Asserts.Assert(!locationsSoFar.Contains(loc));
          locationsSoFar.Add(loc);
        } else {
          break;
        }
      }
      return locationsSoFar;
    }

    public static TerrainTile AddTile(Terrain terrain, Location location, int elevation, params ITerrainTileComponent[] components) {
      var tile =
        terrain.root.EffectTerrainTileCreate(
              NullITerrainTileEvent.Null,
              elevation,
              ITerrainTileComponentMutBunch.New(terrain.root));
      foreach (var component in components) {
        tile.components.Add(component);
      }
      terrain.tiles.Add(location, tile);
      return tile;
    }
  }
}
