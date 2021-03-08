using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class JumpingCaveTerrainGenerator {
    public static Terrain Generate(
        SSContext context,
        Root root,
        Pattern pattern,
        Rand rand,
        bool considerCornersAdjacent,
        float radius) {
      float elevationStepHeight = .2f;

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      var terrain =
          root.EffectTerrainCreate(
              pattern,
              elevationStepHeight,
              root.EffectTerrainTileByLocationMutMapCreate());

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      AddCircle(context, terrain, new Location(0, 0, 0), radius);

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      return terrain;
    }

    public static void AddCircle(SSContext context, Terrain terrain, Location originLocation, float radius) {
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      var searcher = new PatternExplorer(context, terrain.pattern, false, originLocation);

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      while (true) {
        context.Flare(context.root.GetDeterministicHashCode().ToString());

        Location loc = searcher.Next(context);
        context.Flare(context.root.GetDeterministicHashCode().ToString());
        Vec2 center = terrain.pattern.GetTileCenter(loc);
        context.Flare(context.root.GetDeterministicHashCode().ToString());
        if (center.distance(new Vec2(0, 0)) <= radius) {
          context.Flare(context.root.GetDeterministicHashCode().ToString());
          if (!terrain.tiles.ContainsKey(loc)) {
            context.Flare(context.root.GetDeterministicHashCode().ToString());
            AddTile(context, terrain, loc, 0);
            context.Flare(context.root.GetDeterministicHashCode().ToString());
          }
          context.Flare(context.root.GetDeterministicHashCode().ToString());
        } else {
          context.Flare(context.root.GetDeterministicHashCode().ToString());
          break;
        }
        context.Flare(context.root.GetDeterministicHashCode().ToString());
      }
      context.Flare(context.root.GetDeterministicHashCode().ToString());

    }

    private static void AddTile(SSContext context, Terrain terrain, Location location, int elevation) {
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      var tile =
        terrain.root.EffectTerrainTileCreate(
              NullITerrainTileEvent.Null,
              elevation,
              ITerrainTileComponentMutBunch.New(terrain.root));
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      terrain.tiles.Add(location, tile);
      context.Flare(context.root.GetDeterministicHashCode().ToString());
    }
  }
}
