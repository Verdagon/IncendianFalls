﻿using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class TutorialLevelControllerExtensions {
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

      levelSuperstate = new LevelSuperstate(level);

      var geomancy =
        Vivifier.Vivify(level, levelSuperstate, Vivifier.ParseGeomancy(LEVEL.Replace("'", "\"")));

      level.controller = root.EffectTutorialLevelControllerCreate(level).AsILevelController();

      var start = Vivifier.ExtractLocation(geomancy, "Start");

      if (geomancy.Count > 0) {
        Asserts.Assert(false, Vivifier.PrintMembers(geomancy));
      }

      entryLocation = start;
    }

    public static string GetName(this TutorialLevelController obj) {
      return "Tutorial";
    }

    public static bool ConsiderCornersAdjacent(this TutorialLevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this TutorialLevelController obj,
        Game game,
        Superstate superstate,
        string triggerName) {
      game.root.logger.Error("Got trigger! " + triggerName);

      if (triggerName == "dialogDone") {
        game.overlay = Overlay.Null;
      }

      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this TutorialLevelController obj,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      game.root.logger.Error("Got trigger! " + triggerName);
      if (triggerName == "ambush1Trigger") {
        game.overlay =
          game.root.EffectOverlayCreate(
            100,
            new Color(0, 128, 255, 128),
            "this is overlay text",
            new Color(255, 128, 0, 128),
            true,
            true,
            2000,
            3000,
            new ButtonImmList(new List<Button>() { }),
            8000,
            "dialogDone");
      }
      return new Atharia.Model.Void();
    }

    private static string LEVEL = @"
-8 -2 4 4 Mud
-8 -2 5 4 Mud
-8 -2 6 4 Mud
-8 -2 7 1 Dirt
-7 -2 0 4 Mud
-7 -2 2 4 Mud
-7 -2 3 4 Mud
-7 -2 4 1 Dirt Trigger('ambush4Rocks')
-7 -2 5 4 Mud
-7 -2 6 1 Dirt
-7 -2 7 1 Dirt Trigger('ambush4Summon')
-7 -1 0 1 Dirt
-7 -1 1 4 Mud
-7 -1 2 1 Dirt Cave
-7 -1 3 4 Mud
-7 -1 4 4 Mud
-6 -3 6 4 Mud
-6 -3 7 4 Mud
-6 -2 0 4 Mud
-6 -2 1 4 Mud
-6 -2 2 1 Dirt
-6 -2 3 1 Dirt
-6 -2 4 4 Mud
-6 -2 5 1 Dirt Trigger('ambush4Trigger')
-6 -2 6 1 Dirt Trigger('ambush4IntendedClonePlacement')
-6 -2 7 4 Mud
-6 -1 0 4 Mud
-6 -1 1 1 Dirt
-6 -1 2 1 Dirt
-6 -1 3 1 Dirt
-6 -1 4 4 Mud
-6 -1 5 4 Mud
-5 -3 6 4 Mud
-5 -3 7 4 Mud
-5 -2 0 1 Dirt
-5 -2 1 1 Dirt
-5 -2 2 1 Dirt
-5 -2 3 1 Dirt Trigger('ambush4Warning')
-5 -2 4 4 Mud
-5 -2 5 4 Mud
-5 -1 0 4 Mud
-5 -1 1 4 Mud
-5 -1 3 4 Mud
-5 -1 5 4 Mud
-4 -2 0 4 Mud
-4 -2 1 1 Dirt
-4 -2 2 1 Dirt
-4 -2 3 1 Dirt
-4 -2 4 1 Dirt
-4 -2 5 4 Mud
-4 -2 6 4 Mud
-4 -2 7 1 Dirt Trigger('ambush3Summon')
-3 -2 1 4 Mud
-3 -2 3 4 Mud
-3 -2 4 4 Mud
-3 -2 5 1 Dirt
-3 -2 6 1 Dirt Trigger('ambush3Summon')
-3 -2 7 4 Mud
-3 -1 0 1 Dirt
-3 -1 1 4 Mud
-3 -1 2 1 Dirt Trigger('ambush3Trigger')
-3 -1 3 4 Mud
-3 -1 4 1 Dirt
-3 -1 5 4 Mud
-3 -1 6 4 Mud
-3 -1 7 1 Dirt
-3 0 0 4 Mud
-3 0 2 4 Mud
-2 -1 0 4 Mud
-2 -1 1 1 Dirt
-2 -1 2 4 Mud
-2 -1 3 1 Dirt Trigger('ambush3Trigger')
-2 -1 4 4 Mud
-2 -1 5 1 Dirt
-2 -1 6 1 Dirt
-2 -1 7 4 Mud
-2 0 0 1 Dirt Trigger('ambush2Summon')
-2 0 1 1 Dirt
-2 0 2 1 Dirt Trigger('ambush2Trigger')
-2 0 3 4 Mud
-2 0 4 1 Dirt
-2 0 5 4 Mud
-2 0 6 4 Mud
-2 0 7 4 Mud
-1 -1 0 5 Mud
-1 -1 2 5 Mud
-1 -1 4 5 Mud
-1 -1 7 4 Mud
-1 0 0 4 Mud
-1 0 1 4 Mud
-1 0 2 4 Mud
-1 0 3 1 Dirt
-1 0 4 1 Dirt
-1 0 5 1 Dirt
-1 0 6 1 Dirt
-1 0 7 1 Dirt
-1 1 0 4 Mud
0 -1 0 4 Mud
0 -1 1 5 Mud
0 -1 2 1 Dirt
0 -1 3 2 Dirt
0 -1 4 1 Dirt Rocks
0 -1 5 1 Mud Dirt
0 -1 6 1 Mud Dirt
0 -1 7 2 Mud Dirt
0 0 0 4 Mud
0 0 2 4 Mud
0 0 3 4 Mud
0 0 4 1 Dirt
0 0 5 1 Dirt
0 0 6 1 Dirt Rocks
0 0 7 1 Dirt
0 1 0 4 Mud
0 1 1 4 Mud
1 -1 1 4 Mud
1 -1 2 4 Mud
1 -1 3 1 Dirt
1 -1 4 1 Dirt
1 -1 5 1 Dirt Start
1 -1 6 1 Dirt
1 -1 7 2 Dirt
1 0 0 1 Mud Dirt
1 0 1 1 Mud Dirt
1 0 2 1 Dirt Rocks
1 0 3 1 Dirt Trigger('ambush1Trigger')
1 0 4 2 Dirt Trigger('ambush1Trigger')
1 0 5 1 Dirt Trigger('ambush1Summon')
1 0 6 4 Mud
1 0 7 5 Mud
1 1 0 4 Mud
1 1 1 4 Mud
2 -1 3 4 Mud
2 -1 5 5 Mud
2 -1 6 5 Mud
2 0 0 5 Mud
2 0 1 2 Mud Dirt
2 0 2 5 Mud
2 0 3 5 Grass Mud
2 0 5 5 Mud
";
  }
}
