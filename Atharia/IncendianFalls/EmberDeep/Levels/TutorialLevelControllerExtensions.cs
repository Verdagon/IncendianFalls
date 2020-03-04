﻿using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class TutorialLevelControllerExtensions {
    public static void LoadLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocation,
        out Location exitLocation,
        Root root) {
      level =
        root.EffectLevelCreate(
          new Vec3(0, -8, 16),
          root.EffectTerrainCreate(
            PentagonPattern9.makePentagon9Pattern(),
            0.3f,
            root.EffectTerrainTileByLocationMutMapCreate()),
          root.EffectUnitMutSetCreate(),
          NullILevelController.Null,
          0);

      levelSuperstate = new LevelSuperstate(level);

      var geomancy =
        Vivifier.Vivify(level, levelSuperstate, Vivifier.ParseGeomancy(LEVEL));

      if (geomancy.Count > 0) {
        Asserts.Assert(false, Vivifier.PrintMembers(geomancy));
      }

      level.controller = root.EffectTutorialLevelControllerCreate(level).AsILevelController();

      entryLocation = levelSuperstate.FindMarkerLocation("start");
      exitLocation = levelSuperstate.FindMarkerLocation("exit");
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

      if (triggerName == "ambush1b") {
        game.root.logger.Error("showing overlay!");
        game.events.Add(
          new ShowOverlayEvent(
            50, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            100, // fadeInEnd
            0, // fadeOutStart
            0, // fadeOutEnd,
            "",

            "You see an Irkling!\n\nTo attack, click on it while next to it.",
            new Color(255, 255, 255, 255), // textColor
            100, // textFadeInStartS
            200, // textFadeInEndS
            0, // textFadeOutStartS
            0, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() {
              new Button("OK!", new Color(64, 64, 64, 255), "")
            }))
          .AsIGameEvent());

        //game.events.Add(
        //  new FlyCameraEvent(new Location(0, 0, 0), new Vec3(0, -10, 20), 1500, "cameraMovementDone")
        //  .AsIGameEvent());
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
      if (triggeringUnit.Is(game.player) && triggerName == "ambush1Trigger") {
        superstate.levelSuperstate.RemoveSimplePresenceTriggers("ambush1Trigger", 1);
        var summonLocation = superstate.levelSuperstate.FindMarkerLocation("ambush1Summon");
        Vivifier.AddIrkling(game.level, superstate.levelSuperstate, summonLocation);
        game.events.Add(new WaitEvent(300, "ambush1b").AsIGameEvent());
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
-7 -2 4 1 Dirt Marker(ambush4Rocks)
-7 -2 5 4 Mud
-7 -2 6 1 Dirt
-7 -2 7 1 Dirt Marker(ambush4Summon)
-7 -1 0 1 Dirt
-7 -1 1 4 Mud
-7 -1 2 1 Dirt Cave Marker(exit)
-7 -1 3 4 Mud
-7 -1 4 4 Mud
-6 -3 6 4 Mud
-6 -3 7 4 Mud
-6 -2 0 4 Mud
-6 -2 1 4 Mud
-6 -2 2 1 Dirt
-6 -2 3 1 Dirt
-6 -2 4 4 Mud
-6 -2 5 1 Dirt Trigger(ambush4Trigger)
-6 -2 6 1 Dirt Marker(ambush4IntendedClonePlacement)
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
-5 -2 3 1 Dirt Trigger(ambush4Warning)
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
-4 -2 7 1 Dirt Marker(ambush3Summon)
-3 -2 1 4 Mud
-3 -2 3 4 Mud
-3 -2 4 4 Mud
-3 -2 5 1 Dirt
-3 -2 6 1 Dirt Marker(ambush3Summon)
-3 -2 7 4 Mud
-3 -1 0 1 Dirt
-3 -1 1 4 Mud
-3 -1 2 1 Dirt Trigger(ambush3Trigger)
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
-2 -1 3 1 Dirt Trigger(ambush3Trigger)
-2 -1 4 4 Mud
-2 -1 5 1 Dirt
-2 -1 6 1 Dirt
-2 -1 7 4 Mud
-2 0 0 1 Dirt Marker(ambush2Summon)
-2 0 1 1 Dirt
-2 0 2 1 Dirt Trigger(ambush2Trigger)
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
1 -1 5 1 Dirt Marker(start)
1 -1 6 1 Dirt
1 -1 7 2 Dirt
1 0 0 1 Mud Dirt
1 0 1 1 Mud Dirt
1 0 2 1 Dirt Rocks
1 0 3 1 Dirt Trigger(ambush1Trigger)
1 0 4 2 Dirt Trigger(ambush1Trigger)
1 0 5 1 Dirt Marker(ambush1Summon)
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
2 0 3 5 Mud
2 0 5 5 Mud
";
  }
}
