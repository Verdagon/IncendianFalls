using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class RidgeLevelControllerExtensions {
    public static void LoadLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocation,
        Root root) {
      level =
        root.EffectLevelCreate(
          root.EffectTerrainCreate(
            PentagonPattern9.makePentagon9Pattern(),
            0.3f,
            root.EffectTerrainTileByLocationMutMapCreate()),
          root.EffectUnitMutSetCreate(),
          NullILevelController.Null,
          0);
      level.controller = root.EffectRidgeLevelControllerCreate(level).AsILevelController();

      levelSuperstate = new LevelSuperstate(level);

      var geomancy =
        Vivifier.Vivify(level, levelSuperstate, Vivifier.ParseGeomancy(LEVEL));

      var ambush4Rocks = Vivifier.ExtractLocation(geomancy, "ambush4Rocks");
      var ambush4Warning = Vivifier.ExtractLocation(geomancy, "ambush4Warning");
      var ambush4Trigger = Vivifier.ExtractLocation(geomancy, "ambush4Trigger");
      var ambush4Summon = Vivifier.ExtractLocation(geomancy, "ambush4Summon");
      var ambush4IntendedClonePlacement = Vivifier.ExtractLocation(geomancy, "ambush4IntendedClonePlacement");
      var ambush3Summon = Vivifier.ExtractLocations(geomancy, "ambush3Summon");
      var ambush3Trigger = Vivifier.ExtractLocations(geomancy, "ambush3Trigger");
      var ambush2Summon = Vivifier.ExtractLocation(geomancy, "ambush2Summon");
      var ambush2Trigger = Vivifier.ExtractLocation(geomancy, "ambush2Trigger");
      var ambush1Trigger = Vivifier.ExtractLocations(geomancy, "ambush1Trigger");
      var ambush1Summon = Vivifier.ExtractLocation(geomancy, "ambush1Summon");
      var start = Vivifier.ExtractLocation(geomancy, "start");

      if (geomancy.Count > 0) {
        Asserts.Assert(false, Vivifier.PrintMembers(geomancy));
      }

      entryLocation = start;
    }

    public static string GetName(this RidgeLevelController obj) {
      return "Ridge";
    }

    public static bool ConsiderCornersAdjacent(this RidgeLevelController obj) {
      return false;
    }

    private static string LEVEL = @"
-8 -2 4 4 mud
-8 -2 5 4 mud
-8 -2 6 4 mud
-8 -2 7 1 dirt
-7 -2 0 4 mud
-7 -2 2 4 mud
-7 -2 3 4 mud
-7 -2 4 1 dirt ambush4Rocks
-7 -2 5 4 mud
-7 -2 6 1 dirt
-7 -2 7 1 dirt ambush4Summon
-7 -1 0 1 dirt
-7 -1 1 4 mud
-7 -1 2 1 dirt cave
-7 -1 3 4 mud
-7 -1 4 4 mud
-6 -3 6 4 mud
-6 -3 7 4 mud
-6 -2 0 4 mud
-6 -2 1 4 mud
-6 -2 2 1 dirt
-6 -2 3 1 dirt
-6 -2 4 4 mud
-6 -2 5 1 dirt ambush4Trigger
-6 -2 6 1 dirt ambush4IntendedClonePlacement
-6 -2 7 4 mud
-6 -1 0 4 mud
-6 -1 1 1 dirt
-6 -1 2 1 dirt
-6 -1 3 1 dirt
-6 -1 4 4 mud
-6 -1 5 4 mud
-5 -3 6 4 mud
-5 -3 7 4 mud
-5 -2 0 1 dirt
-5 -2 1 1 dirt
-5 -2 2 1 dirt
-5 -2 3 1 dirt ambush4Warning
-5 -2 4 4 mud
-5 -2 5 4 mud
-5 -1 0 4 mud
-5 -1 1 4 mud
-5 -1 3 4 mud
-5 -1 5 4 mud
-4 -2 0 4 mud
-4 -2 1 1 dirt
-4 -2 2 1 dirt
-4 -2 3 1 dirt
-4 -2 4 1 dirt
-4 -2 5 4 mud
-4 -2 6 4 mud
-4 -2 7 1 dirt ambush3Summon
-3 -2 1 4 mud
-3 -2 3 4 mud
-3 -2 4 4 mud
-3 -2 5 1 dirt
-3 -2 6 1 dirt ambush3Summon
-3 -2 7 4 mud
-3 -1 0 1 dirt
-3 -1 1 4 mud
-3 -1 2 1 dirt ambush3Trigger
-3 -1 3 4 mud
-3 -1 4 1 dirt
-3 -1 5 4 mud
-3 -1 6 4 mud
-3 -1 7 1 dirt
-3 0 0 4 mud
-3 0 2 4 mud
-2 -1 0 4 mud
-2 -1 1 1 dirt
-2 -1 2 4 mud
-2 -1 3 1 dirt ambush3Trigger
-2 -1 4 4 mud
-2 -1 5 1 dirt
-2 -1 6 1 dirt
-2 -1 7 4 mud
-2 0 0 1 dirt ambush2Summon
-2 0 1 1 dirt
-2 0 2 1 dirt ambush2Trigger
-2 0 3 4 mud
-2 0 4 1 dirt
-2 0 5 4 mud
-2 0 6 4 mud
-2 0 7 4 mud
-1 -1 0 5 mud
-1 -1 2 5 mud
-1 -1 4 5 mud
-1 -1 7 4 mud
-1 0 0 4 mud
-1 0 1 4 mud
-1 0 2 4 mud
-1 0 3 1 dirt
-1 0 4 1 dirt
-1 0 5 1 dirt
-1 0 6 1 dirt
-1 0 7 1 dirt
-1 1 0 4 mud
0 -1 0 4 mud
0 -1 1 5 mud
0 -1 2 1 dirt
0 -1 3 2 dirt
0 -1 4 1 dirt rocks
0 -1 5 1 mud dirt
0 -1 6 1 mud dirt
0 -1 7 2 mud dirt
0 0 0 4 mud
0 0 2 4 mud
0 0 3 4 mud
0 0 4 1 dirt
0 0 5 1 dirt
0 0 6 1 dirt rocks
0 0 7 1 dirt
0 1 0 4 mud
0 1 1 4 mud
1 -1 1 4 mud
1 -1 2 4 mud
1 -1 3 1 dirt
1 -1 4 1 dirt
1 -1 5 1 dirt start
1 -1 6 1 dirt
1 -1 7 2 dirt
1 0 0 1 mud dirt
1 0 1 1 mud dirt
1 0 2 1 dirt rocks
1 0 3 1 dirt ambush1Trigger
1 0 4 2 dirt ambush1Trigger
1 0 5 1 dirt ambush1Summon
1 0 6 4 mud
1 0 7 5 mud
1 1 0 4 mud
1 1 1 4 mud
2 -1 3 4 mud
2 -1 5 5 mud
2 -1 6 5 mud
2 0 0 5 mud
2 0 1 2 mud dirt
2 0 2 5 mud
2 0 3 5 grass mud
2 0 5 5 mud
";
  }
}
