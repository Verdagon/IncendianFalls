using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class IslandLevelControllerExtensions {
    public static void LoadLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocation,
        out Location exitLocation,
        Game game,
        Superstate superstate,
        int depth) {
      level =
        game.root.EffectLevelCreate(
          new Vec3(0, -8, 16),
          game.root.EffectTerrainCreate(
            PentagonPattern9.makePentagon9Pattern(),
            0.3f,
            game.root.EffectTerrainTileByLocationMutMapCreate()),
          game.root.EffectUnitMutSetCreate(),
          NullILevelController.Null,
          0);

      levelSuperstate = new LevelSuperstate(level);

      var geomancy =
        Vivifier.Vivify(level, levelSuperstate, Vivifier.ParseGeomancy(LEVEL));
      if (geomancy.Count > 0) {
        Asserts.Assert(false, Vivifier.PrintMembers(geomancy));
      }

      level.controller = game.root.EffectIslandLevelControllerCreate(level).AsILevelController();

      level.EnterUnit(
        levelSuperstate,
        levelSuperstate.FindMarkerLocation("exit"),
        level.time,
        Emberfolk.Make(level.root));

      entryLocation = levelSuperstate.FindMarkerLocation("start");
      exitLocation = levelSuperstate.FindMarkerLocation("exit");
    }

    public static string GetName(this IslandLevelController obj) {
      return "Island";
    }

    public static bool ConsiderCornersAdjacent(this IslandLevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this IslandLevelController obj,
        Game game,
        Superstate superstate,
        string triggerName) {
      game.root.logger.Info("Got simple trigger: " + triggerName);
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this IslandLevelController obj,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      game.root.logger.Info("Got simple unit trigger: " + triggerName);
      return new Atharia.Model.Void();
    }

    private static string LEVEL = @"
-4 -1 4 1 Mud
-4 -1 7 1 Mud
-3 -1 0 1 Mud
-3 -1 2 1 Mud
-3 -1 4 1 Mud
-3 -1 5 1 Mud
-3 -1 6 2 Mud
-3 -1 7 2 Mud
-3 0 0 1 Mud
-3 0 2 1 Mud
-3 0 4 1 Mud
-3 0 7 1 Mud
-2 -1 0 1 Mud
-2 -1 1 1 Mud
-2 -1 2 1 Mud
-2 -1 3 2 Mud
-2 -1 4 2 Mud
-2 -1 5 2 Mud
-2 -1 6 3 Mud
-2 -1 7 3 Mud
-2 0 0 3 Mud
-2 0 1 2 Mud
-2 0 2 3 Mud
-2 0 3 2 Mud
-2 0 4 3 Mud
-2 0 5 2 Mud
-2 0 6 2 Mud
-2 0 7 2 Mud
-2 1 0 1 Mud
-1 -1 2 1 Mud
-1 -1 3 1 Mud
-1 -1 4 2 Mud
-1 -1 5 2 Mud
-1 -1 6 3 Mud
-1 -1 7 3 Mud
-1 0 0 3 Mud
-1 0 1 3 Mud
-1 0 2 3 Mud Marker(enemy)
-1 0 3 3 Mud
-1 0 4 3 Mud
-1 0 5 3 Mud
-1 0 6 2 Mud
-1 0 7 3 Mud
-1 1 0 1 Mud
-1 1 1 1 Mud
-1 1 2 1 Mud
0 -1 2 1 Mud
0 -1 3 1 Mud
0 -1 4 2 Mud
0 -1 5 2 Mud
0 -1 6 3 Mud
0 -1 7 3 Mud
0 0 0 3 Mud
0 0 1 3 Mud
0 0 2 3 Mud
0 0 3 3 Mud
0 0 4 3 Mud
0 0 5 3 Mud
0 0 6 3 Mud
0 0 7 3 Mud
0 1 0 3 Mud Marker(exit)
0 1 1 2 Mud
0 1 2 2 Mud
0 1 3 1 Mud
0 1 4 2 Mud
0 1 5 1 Mud
0 1 6 1 Mud
0 1 7 1 Mud
1 -1 3 1 Mud
1 -1 4 1 Mud
1 -1 5 2 Mud
1 -1 6 2 Mud
1 -1 7 1 Mud
1 0 0 3 Mud
1 0 1 3 Mud
1 0 2 3 Mud
1 0 3 3 Mud
1 0 4 3 Mud Marker(start)
1 0 5 3 Mud
1 0 6 3 Mud
1 0 7 3 Mud
1 1 0 3 Mud
1 1 1 3 Mud
1 1 2 3 Mud
1 1 3 3 Mud
1 1 4 2 Mud
1 1 5 2 Mud
1 1 6 1 Mud
1 1 7 1 Mud
2 0 0 1 Mud
2 0 1 2 Mud
2 0 2 1 Mud
2 0 3 2 Mud
2 0 4 2 Mud
2 0 5 3 Mud
2 0 6 3 Mud
2 0 7 2 Mud
2 1 0 3 Mud
2 1 1 3 Mud
2 1 2 2 Mud
2 1 3 3 Mud
2 1 4 2 Mud
2 1 5 2 Mud
2 1 6 1 Mud
2 1 7 1 Mud
3 0 3 1 Mud
3 0 5 1 Mud
3 0 6 1 Mud
3 1 0 2 Mud
3 1 1 2 Mud
3 1 2 1 Mud
3 1 3 2 Mud
3 1 4 1 Mud
3 1 5 1 Mud
4 1 1 1 Mud
4 1 2 1 Mud
4 1 3 1 Mud
";
  }
}
