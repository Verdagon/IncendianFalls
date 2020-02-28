using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class SetupTerrainRequestExecutor {
    public static Terrain Execute(
        SSContext context,
        SetupTerrainRequest request) {
      var pattern = request.pattern;
      var rand = context.root.EffectRandCreate(1337);
      var terrain = CircleTerrainGenerator.Generate(context.root, pattern, rand, 8.0f);
      return terrain;
    }
  }
}
