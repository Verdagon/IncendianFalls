using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class CircleTerrainGenerator {
    public static Terrain Generate(SSContext context, Rand rand, float radius) {
      float elevationStepHeight = .2f;

      var terrain =
          context.root.EffectTerrainCreate(
              PentagonPattern9.makePentagon9Pattern(),
              elevationStepHeight,
              context.root.EffectTerrainTileByLocationMutMapCreate());

      var searcher = new PatternExplorer(terrain.pattern, false, new Location(0, 0, 0));

      while (true) {
        Location loc = searcher.Next();
        Vec2 center = terrain.pattern.GetTileCenter(loc);
        if (center.distance(new Vec2(0, 0)) <= radius) {
          terrain.tiles.Add(
              loc,
              context.root.EffectTerrainTileCreate(
                  1,
                  true,
                  "ravanest",
                  ITerrainTileComponentMutBunch.New(context.root)));
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
