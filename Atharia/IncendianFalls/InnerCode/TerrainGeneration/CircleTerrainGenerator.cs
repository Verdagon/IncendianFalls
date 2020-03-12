using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class CircleTerrainGenerator {
    public static Terrain Generate(Root root, Pattern pattern, Rand rand, float radius) {
      float elevationStepHeight = .2f;

      var terrain =
          root.EffectTerrainCreate(
              pattern,
              elevationStepHeight,
              root.EffectTerrainTileByLocationMutMapCreate());

      var searcher = new PatternExplorer(terrain.pattern, false, new Location(0, 0, 0));

      while (true) {
        Location loc = searcher.Next();
        Vec2 center = terrain.pattern.GetTileCenter(loc);
        if (center.distance(new Vec2(0, 0)) <= radius) {
          var tile =
            root.EffectTerrainTileCreate(
                  root.EffectITerrainTileEventMutListCreate(),
                  1,
                  ITerrainTileComponentMutBunch.New(root));
          terrain.tiles.Add(loc, tile);
        } else {
          break;
        }
      }

      //TerrainUtils.slopify(terrain, new Vec2(0, -1), .3f);

      TerrainUtils.randify(rand, terrain, 2);

      return terrain;
    }
  }
}
