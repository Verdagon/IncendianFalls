using System;
using Atharia.Model;

namespace IncendianFalls {
  public static class MakeLevel {
    public static Level MakeNextLevel(Root root, Rand rand, bool squareLevelsOnly, string name) {
      if (squareLevelsOnly || rand.Next() % 2 == 0) {
        return MakeSquareLevel(root, rand, name);
      } else {
        return MakePentagonalLevel(root, rand, name);
      }
    }

    private static Level MakePentagonalLevel(Root root, Rand rand, string name) {
      var terrain =
          ForestTerrainGenerator.Generate(
              root,
              rand,
              PentagonPattern9.makePentagon9Pattern());

      var units = root.EffectUnitMutBunchCreate();

      var walkableLocations = new WalkableLocations(terrain, units);

      SetupCommon.FillWithUnits(root, rand, terrain, units, walkableLocations, 40);

      return root.EffectLevelCreate(name, false, terrain, units);
    }

    private static Level MakeSquareLevel(Root root, Rand rand, string name) {
      var terrain = DungeonTerrainGenerator.Generate(root, 80, 20, rand);

      var units = root.EffectUnitMutBunchCreate();

      var walkableLocations = new WalkableLocations(terrain, units);

      SetupCommon.FillWithUnits(root, rand, terrain, units, walkableLocations, 15);

      return root.EffectLevelCreate(name, true, terrain, units);
    }

  }
}
