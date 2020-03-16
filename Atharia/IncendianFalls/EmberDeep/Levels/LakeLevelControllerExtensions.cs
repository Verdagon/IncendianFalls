using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class LakeLevelControllerExtensions {
    public static void LoadLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocationRet,
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
          game.time);

      var geomancy =
        Vivifier.Vivify(level, Vivifier.ParseGeomancy(LEVEL));
      if (geomancy.Count > 0) {
        Asserts.Assert(false, Vivifier.PrintMembers(geomancy));
      }

      levelSuperstate = new LevelSuperstate(level);

      level.terrain.tiles[levelSuperstate.FindMarkerLocation("exit")].components.Add(
        game.root.EffectEmberDeepLevelLinkerTTCCreate(depth + 1).AsITerrainTileComponent());

      level.terrain.tiles[levelSuperstate.FindMarkerLocation("armor")].components.Add(
        level.root.EffectItemTTCCreate(
          level.root.EffectArmorCreate().AsIItem())
        .AsITerrainTileComponent());

      var mysteriousManLoc = levelSuperstate.FindMarkerLocation("mysteriousMan");
      level.EnterUnit(levelSuperstate, mysteriousManLoc, int.MaxValue, MysteriousMan.Make(level.root));

      var entryLocation = levelSuperstate.FindMarkerLocation("entry");
      entryLocationRet = entryLocation;

      EmberDeepUnitsAndItems.PlaceItems(game.rand, level, levelSuperstate, (loc) => !loc.Equals(entryLocation), .002f, .002f);

      level.controller = game.root.EffectLakeLevelControllerCreate(level).AsILevelController();

      game.levels.Add(level);
    }

    public static string GetName(this LakeLevelController obj) {
      return "Lake";
    }

    public static bool ConsiderCornersAdjacent(this LakeLevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this LakeLevelController obj,
        Game game,
        Superstate superstate,
        string triggerName) {
      game.root.logger.Info("Got simple trigger: " + triggerName);


      if (triggerName == "levelStart") {
        game.AddEvent(new WaitEvent(1500, "startCamera").AsIGameEvent());
      }
      if (triggerName == "startCamera") {
        game.AddEvent(
          new FlyCameraEvent(
            superstate.levelSuperstate.FindMarkerLocation("cameraPanTo"),
            new Vec3(0, 8, 8),
            1000,
            "cameraReachedPanTo")
          .AsIGameEvent());
      }
      if (triggerName == "cameraReachedPanTo") {
        game.AddEvent(
          new WaitEvent(1000, "cameraWaitDone").AsIGameEvent());
      }
      if (triggerName == "cameraWaitDone") {
        game.AddEvent(
          new FlyCameraEvent(
            superstate.levelSuperstate.FindMarkerLocation("entry"),
            new Vec3(0, 8, 8),
            1500,
            "cameraDone")
          .AsIGameEvent());
      }
      if (triggerName == "cameraDone") {
        game.player.nextActionTime = game.level.time;

        game.AddEvent(
          new ShowOverlayEvent(
            "It's eerily silent in here.",
            "aside",
            new ButtonImmList(new List<Button>() { }))
          //new ShowOverlayEvent(
          //  40, // sizePercent
          //  new Color(16, 16, 16, 224), // backgroundColor
          //  300,// fadeInEnd
          //  2800, // fadeOutStart
          //  3100, // fadeOutEnd,
          //  "",

          //  "It's eerily silent in here.",
          //  new Color(255, 64, 0, 255), // textColor
          //  300, // textFadeInStartS
          //  600, // textFadeInEndS
          //  2500, // textFadeOutStartS
          //  2800, // textFadeOutEndS
          //  true, // topAligned
          //  true, // leftAligned

          //  new ButtonImmList(new List<Button>() { }))
          .AsIGameEvent());
      }

      if (triggerName == "line2") {
        game.AddEvent(
          new ShowOverlayEvent(
            "He slowly turns and looks at you, into your eyes, through them, as if into your soul.",
            "normal",
            new ButtonImmList(new List<Button>() { new Button("...", "line3") }))
          //new ShowOverlayEvent(
          //  50, // sizePercent
          //  new Color(16, 16, 16, 224), // backgroundColor
          //  0,// fadeInEnd
          //  3800, // fadeOutStart
          //  3800, // fadeOutEnd,
          //  "line3",

          //  "He slowly turns and looks at you, into your eyes, through them, as if into your soul.",
          //  new Color(255, 64, 0, 255), // textColor
          //  0, // textFadeInStartS
          //  300, // textFadeInEndS
          //  3500, // textFadeOutStartS
          //  3800, // textFadeOutEndS
          //  true, // topAligned
          //  true, // leftAligned

          //  new ButtonImmList(new List<Button>() { }))
          .AsIGameEvent());
      }

      if (triggerName == "line3") {
        game.AddEvent(
          new ShowOverlayEvent(
            "He turns away and is still again.",
            "normal",
            new ButtonImmList(new List<Button>() { new Button("...", "") }))
          //new ShowOverlayEvent(
          //  50, // sizePercent
          //  new Color(16, 16, 16, 224), // backgroundColor
          //  0,// fadeInEnd
          //  2200, // fadeOutStart
          //  2500, // fadeOutEnd,
          //  "",

          //  "He turns away and is still again.",
          //  new Color(255, 64, 0, 255), // textColor
          //  0, // textFadeInStartS
          //  300, // textFadeInEndS
          //  1900, // textFadeOutStartS
          //  2200, // textFadeOutEndS
          //  true, // topAligned
          //  true, // leftAligned

          //  new ButtonImmList(new List<Button>() { }))
          .AsIGameEvent());
      }


      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this LakeLevelController obj,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      game.root.logger.Info("Got simple unit trigger: " + triggerName);

      if (triggeringUnit.NullableIs(game.player) && triggerName == "nextToMysteriousMan") {
        superstate.levelSuperstate.RemoveSimplePresenceTriggers("nextToMysteriousMan", 1);

        game.AddEvent(
          new ShowOverlayEvent(
            "You see a man, still as a statue.",
            "normal",
            new ButtonImmList(new List<Button>() { new Button("...", "line2") }))
          //new ShowOverlayEvent(
          //  50, // sizePercent
          //  new Color(16, 16, 16, 224), // backgroundColor
          //  300,// fadeInEnd
          //  2800, // fadeOutStart
          //  2800, // fadeOutEnd,
          //  "line2",

          //  "You see a man, still as a statue.",
          //  new Color(255, 64, 0, 255), // textColor
          //  300, // textFadeInStartS
          //  600, // textFadeInEndS
          //  2500, // textFadeOutStartS
          //  2800, // textFadeOutEndS
          //  true, // topAligned
          //  true, // leftAligned

          //  new ButtonImmList(new List<Button>() { }))
          .AsIGameEvent());
      }


      return new Atharia.Model.Void();
    }

    private static string LEVEL = @"
-9 -1 2 3 Mud Rocks
-9 -1 4 3 Mud
-9 -1 5 3 Mud
-9 -1 6 3 Mud Dirt
-9 -1 7 2 Mud
-9 0 0 2 Mud Dirt Obsidian
-9 0 2 2 Mud Dirt
-9 0 4 4 Mud Obsidian
-9 0 7 4 Mud
-8 -2 6 3 Mud Obsidian
-8 -2 7 3 Mud
-8 -1 0 3 Mud Dirt
-8 -1 1 3 Mud
-8 -1 2 4 Mud Dirt
-8 -1 3 2 Mud
-8 -1 4 4 Mud Rocks
-8 -1 5 2 Mud Dirt
-8 -1 6 1 Mud
-8 -1 7 2 Mud
-8 0 0 5 Mud Rocks
-8 0 1 4 Mud
-8 0 2 10 Mud Obsidian
-8 0 3 8 Mud
-8 0 4 9 Mud Dirt Obsidian
-8 0 5 6 Mud
-8 0 6 4 Mud
-8 0 7 1 Mud Cave Marker(exit)
-8 1 0 2 Mud Dirt Rocks
-8 1 2 1 Mud
-7 -2 4 3 Mud
-7 -2 5 3 Mud
-7 -2 6 3 Mud
-7 -2 7 5 Mud Obsidian
-7 -1 0 3 Mud
-7 -1 1 5 Mud Dirt Obsidian
-7 -1 2 5 Mud
-7 -1 3 6 Mud Obsidian
-7 -1 4 6 Mud Rocks
-7 -1 5 3 Mud
-7 -1 6 2 Mud Dirt
-7 -1 7 3 Mud Obsidian
-7 0 0 1 Mud
-7 0 1 4 Mud Dirt
-7 0 2 2 Mud Rocks
-7 0 3 6 Mud
-7 0 4 1 Mud
-7 0 5 3 Mud
-7 0 6 1 Mud
-7 0 7 1 Mud Dirt Marker(armor)
-7 1 0 2 Mud Dirt Obsidian
-7 1 1 1 Mud
-7 1 2 1 Mud
-7 1 3 1 Mud Obsidian
-7 1 4 1 Mud Dirt Obsidian
-7 1 5 1 Mud
-7 1 6 1 Mud
-7 1 7 1 Mud
-6 -2 4 9 Mud
-6 -2 5 7 Mud
-6 -2 6 5 Mud
-6 -2 7 8 Mud Obsidian
-6 -1 0 1 Mud
-6 -1 1 1 Mud Rocks
-6 -1 2 1 Mud Dirt
-6 -1 3 2 Mud
-6 -1 4 1 Mud
-6 -1 5 4 Mud
-6 -1 6 1 Mud Dirt
-6 -1 7 2 Mud Rocks
-6 0 0 1 Mud
-6 0 1 1 Mud Dirt Obsidian
-6 0 2 1 Mud Obsidian
-6 0 3 2 Mud Obsidian
-6 0 4 2 Mud Dirt Rocks
-6 0 5 1 Mud
-6 0 6 1 Mud
-6 0 7 1 Mud Obsidian
-6 1 0 2 Mud Rocks
-6 1 1 2 Mud Dirt
-6 1 2 4 Mud
-6 1 3 3 Mud Dirt
-6 1 4 5 Mud Dirt
-6 1 5 3 Mud
-6 1 6 3 Mud
-6 1 7 3 Mud Dirt
-6 2 0 1 Mud Obsidian
-6 2 1 1 Mud
-6 2 2 1 Mud Dirt
-5 -2 4 8 Mud
-5 -2 5 12 Mud
-5 -2 6 6 Mud Dirt
-5 -2 7 5 Mud
-5 -1 0 2 Mud
-5 -1 1 1 Mud
-5 -1 2 1 Mud Obsidian
-5 -1 3 2 Mud Dirt
-5 -1 4 1 Mud Rocks
-5 -1 5 1 Mud
-5 -1 6 2 Mud
-5 -1 7 1 Mud
-5 0 0 2 Mud Dirt Obsidian
-5 0 1 1 Mud
-5 0 2 1 Mud
-5 0 3 1 Mud Rocks
-5 0 4 1 Mud Dirt
-5 0 5 1 Mud
-5 0 6 1 Mud
-5 0 7 1 Mud Dirt
-5 1 0 1 Mud
-5 1 1 2 Mud Dirt Rocks
-5 1 2 1 Mud Dirt
-5 1 3 5 Mud Obsidian
-5 1 4 3 Mud Rocks
-5 1 5 5 Mud Rocks
-5 1 6 1 Mud
-5 1 7 1 Magma
-5 2 0 1 Mud Rocks
-5 2 1 1 Mud Dirt
-5 2 2 1 Mud Dirt
-5 2 3 1 Mud
-4 -2 4 3 Mud
-4 -2 5 3 Mud Obsidian
-4 -2 6 3 Mud Rocks
-4 -2 7 2 Mud
-4 -1 0 3 Mud
-4 -1 1 1 Mud Obsidian
-4 -1 2 3 Mud
-4 -1 3 1 Mud Rocks
-4 -1 4 1 Mud Dirt
-4 -1 5 2 Mud
-4 -1 6 2 Mud Obsidian
-4 -1 7 1 Mud
-4 0 0 5 Mud
-4 0 1 1 Mud
-4 0 2 9 Mud Dirt
-4 0 3 3 Mud
-4 0 4 6 Mud
-4 0 5 3 Mud Obsidian
-4 0 6 1 Magma
-4 0 7 1 Magma
-4 1 0 1 Magma
-4 1 1 1 Mud Dirt Rocks
-4 1 2 1 Magma Fire
-4 1 3 1 Magma
-4 1 4 1 Magma
-4 1 5 1 Magma
-4 1 6 1 Magma Fire
-4 1 7 1 Magma Fire
-4 2 0 1 Magma
-4 2 1 1 Magma
-4 2 2 1 Magma
-4 2 3 1 Magma
-4 2 4 1 Magma Fire
-4 2 5 1 Magma
-4 2 6 1 Magma
-4 2 7 1 Magma
-3 -2 2 8 Mud Obsidian
-3 -2 3 10 Mud Obsidian
-3 -2 4 7 Mud
-3 -2 5 5 Mud
-3 -2 6 1 Mud Rocks
-3 -2 7 1 Dirt
-3 -1 0 1 Mud Dirt
-3 -1 1 4 Mud
-3 -1 2 2 Mud
-3 -1 3 4 Mud Obsidian
-3 -1 4 2 Mud Dirt
-3 -1 5 1 Mud
-3 -1 6 1 Mud Rocks
-3 -1 7 1 Mud
-3 0 0 1 Mud Dirt
-3 0 1 6 Mud Dirt Obsidian
-3 0 2 1 Mud
-3 0 3 8 Mud
-3 0 4 1 Mud Obsidian
-3 0 5 6 Mud Rocks
-3 0 6 1 Magma Fire
-3 0 7 1 Magma
-3 1 0 1 Magma Fire
-3 1 1 1 Magma
-3 1 2 1 Magma
-3 1 3 1 Magma
-3 1 4 1 Magma
-3 1 5 1 Magma
-3 1 6 1 Magma
-3 1 7 1 Magma
-3 2 0 1 Magma
-3 2 1 1 Magma
-3 2 2 1 Magma
-3 2 3 1 Magma
-3 2 4 1 Magma
-3 2 5 1 Magma
-3 2 6 1 Magma
-3 2 7 1 Magma
-3 3 0 1 Magma Fire
-3 3 1 1 Magma
-3 3 2 1 Magma
-2 -2 2 7 Mud
-2 -2 3 9 Mud
-2 -2 4 3 Mud Rocks
-2 -2 5 4 Mud
-2 -2 6 1 Mud
-2 -2 7 1 Mud
-2 -1 0 2 Mud
-2 -1 1 1 Mud
-2 -1 2 2 Mud Rocks
-2 -1 3 1 Mud Obsidian
-2 -1 4 2 Mud Rocks
-2 -1 5 1 Dirt
-2 -1 6 1 Mud
-2 -1 7 1 Mud
-2 0 0 1 Mud Trigger(nextToMysteriousMan)
-2 0 1 1 Mud Rocks
-2 0 2 1 Mud Obsidian Trigger(nextToMysteriousMan)
-2 0 3 1 Mud
-2 0 4 1 Magma
-2 0 5 1 Magma
-2 0 6 1 Magma Fire
-2 0 7 1 Magma
-2 1 0 1 Magma
-2 1 1 1 Magma
-2 1 2 1 Magma Fire
-2 1 3 1 Magma
-2 1 4 1 Magma
-2 1 5 1 Magma
-2 1 6 1 Magma
-2 1 7 1 Magma
-2 2 0 1 Magma
-2 2 1 1 Magma Fire
-2 2 2 1 Magma
-2 2 3 1 Magma Fire
-2 2 4 1 Magma
-2 2 5 1 Magma
-2 2 6 1 Magma
-2 2 7 1 Magma
-2 3 0 1 Magma
-2 3 1 1 Magma
-2 3 2 1 Magma
-2 3 3 1 Magma
-2 3 4 1 Magma
-2 3 5 1 Magma
-2 3 7 1 Magma
-1 -2 3 10 Mud
-1 -2 4 5 Mud
-1 -2 5 2 Mud
-1 -2 6 2 Dirt
-1 -2 7 1 Mud Obsidian
-1 -1 0 2 Mud
-1 -1 1 1 Dirt Rocks
-1 -1 2 3 Mud
-1 -1 3 2 Mud
-1 -1 4 5 Mud
-1 -1 5 5 Mud
-1 -1 6 3 Dirt Rocks
-1 -1 7 7 Mud
-1 0 0 1 Mud
-1 0 1 1 Dirt Trigger(nextToMysteriousMan)
-1 0 2 1 Mud Trigger(nextToMysteriousMan)
-1 0 3 1 Obsidian Mud Marker(mysteriousMan)
-1 0 4 1 Magma Fire
-1 0 5 1 Magma
-1 0 6 1 Magma
-1 0 7 1 Magma
-1 1 0 1 Magma
-1 1 1 1 Magma
-1 1 2 1 Magma
-1 1 3 1 Magma
-1 1 4 1 Magma
-1 1 5 1 Magma
-1 1 6 1 Magma
-1 1 7 1 Magma Fire
-1 2 0 1 Magma
-1 2 1 1 Magma
-1 2 2 1 Magma
-1 2 3 1 Magma
-1 2 4 1 Magma
-1 2 5 1 Magma
-1 2 6 1 Magma
-1 2 7 1 Magma
-1 3 0 1 Magma
-1 3 1 1 Magma
-1 3 2 1 Magma
-1 3 3 1 Magma
-1 3 4 1 Magma
-1 3 5 1 Magma
-1 3 6 1 Magma
-1 3 7 1 Magma
-1 4 0 1 Magma
0 -2 4 1 Mud
0 -2 5 2 Mud
0 -2 6 2 Mud
0 -2 7 4 Mud Rocks
0 -1 0 2 Mud Rocks
0 -1 1 1 Mud
0 -1 2 2 Mud
0 -1 3 2 Mud Obsidian
0 -1 4 6 Mud Rocks
0 -1 5 7 Dirt Rocks
0 -1 6 7 Dirt
0 -1 7 4 Dirt
0 0 0 4 Mud
0 0 1 1 Mud Obsidian
0 0 2 1 Magma
0 0 3 1 Magma
0 0 4 1 Magma
0 0 5 1 Magma
0 0 6 1 Magma
0 0 7 1 Magma
0 1 0 1 Magma Fire
0 1 1 1 Magma
0 1 2 1 Magma
0 1 3 1 Magma
0 1 4 1 Magma
0 1 5 1 Magma
0 1 6 1 Magma
0 1 7 1 Magma
0 2 0 1 Magma
0 2 1 1 Magma
0 2 2 1 Magma
0 2 3 1 Magma Fire
0 2 4 1 Magma
0 2 5 1 Magma
0 2 6 1 Magma
0 2 7 1 Magma
0 3 0 1 Magma Fire
0 3 1 1 Magma
0 3 2 1 Magma
0 3 3 1 Magma
0 3 4 1 Magma
0 3 5 1 Magma
0 3 6 1 Magma Fire
0 3 7 1 Magma
0 4 0 1 Magma
0 4 1 1 Magma
0 4 2 1 Magma
1 -2 5 6 Mud
1 -2 6 7 Dirt
1 -2 7 7 Mud
1 -1 0 5 Dirt Obsidian
1 -1 1 2 Dirt
1 -1 2 1 Mud
1 -1 3 1 Mud Obsidian
1 -1 4 1 Dirt Obsidian
1 -1 5 1 Mud
1 -1 6 1 Mud Obsidian
1 -1 7 1 Mud
1 0 0 1 Mud
1 0 1 3 Mud Rocks
1 0 2 1 Magma
1 0 3 1 Magma
1 0 4 1 Magma Fire
1 0 5 1 Magma Fire
1 0 6 1 Magma
1 0 7 1 Magma
1 1 0 1 Magma
1 1 1 1 Magma
1 1 2 1 Magma
1 1 3 1 Magma
1 1 4 1 Magma Fire
1 1 5 1 Magma
1 1 6 1 Magma
1 1 7 1 Magma
1 2 0 1 Magma
1 2 1 1 Magma
1 2 2 1 Magma Fire
1 2 3 1 Magma
1 2 4 1 Magma
1 2 5 1 Magma
1 2 6 1 Magma
1 2 7 1 Magma
1 3 0 1 Magma
1 3 1 1 Magma
1 3 2 1 Magma
1 3 3 1 Magma
1 3 4 1 Magma
1 3 5 1 Magma
1 3 6 1 Magma
1 3 7 1 Magma
1 4 0 1 Magma
1 4 1 1 Magma
1 4 2 1 Magma
1 4 3 1 Magma
1 4 4 1 Magma Fire
2 -2 6 2 Mud
2 -1 0 7 Mud Obsidian
2 -1 1 7 Mud
2 -1 2 2 Mud
2 -1 3 2 Mud Rocks
2 -1 4 1 Dirt Rocks
2 -1 5 1 Mud
2 -1 6 1 Mud
2 -1 7 1 Mud
2 0 0 2 Mud Rocks
2 0 1 1 Dirt
2 0 2 1 Obsidian Mud
2 0 3 1 Magma
2 0 4 1 Magma
2 0 5 1 Magma
2 0 6 1 Magma
2 0 7 1 Magma
2 1 0 1 Magma Fire
2 1 1 1 Magma
2 1 2 1 Magma
2 1 3 1 Magma
2 1 4 1 Magma
2 1 5 1 Magma Fire
2 1 6 1 Magma
2 1 7 1 Magma
2 2 0 1 Magma
2 2 1 1 Magma
2 2 2 1 Magma
2 2 3 1 Magma
2 2 4 1 Magma
2 2 5 1 Magma
2 2 6 1 Magma
2 2 7 1 Magma Fire
2 3 0 1 Magma
2 3 1 1 Magma
2 3 2 1 Magma
2 3 3 1 Magma
2 3 4 1 Magma
2 3 5 1 Magma
2 3 6 1 Magma
2 3 7 1 Magma
2 4 0 1 Magma Fire
2 4 1 1 Magma
2 4 2 1 Magma
2 4 3 1 Magma
2 4 4 1 Magma
2 4 5 1 Magma
3 -1 0 1 Mud Obsidian
3 -1 1 1 Mud
3 -1 2 1 Mud
3 -1 3 1 Dirt
3 -1 4 1 Mud Obsidian
3 -1 5 1 Mud
3 -1 6 1 Mud Rocks
3 -1 7 2 Mud
3 0 0 1 Mud Obsidian
3 0 1 2 Dirt
3 0 2 1 Mud
3 0 3 1 Mud
3 0 4 1 Mud
3 0 5 1 Magma Fire
3 0 6 1 Magma
3 0 7 1 Magma
3 1 0 1 Magma
3 1 1 1 Magma
3 1 2 1 Magma
3 1 3 1 Magma
3 1 4 1 Magma Marker(cameraPanTo)
3 1 5 1 Magma
3 1 6 1 Magma
3 1 7 1 Magma
3 2 0 1 Magma Fire
3 2 1 1 Magma
3 2 2 1 Magma
3 2 3 1 Magma
3 2 4 1 Magma
3 2 5 1 Magma Fire
3 2 6 1 Magma Fire
3 2 7 1 Magma
3 3 0 1 Magma
3 3 1 1 Magma
3 3 2 1 Magma
3 3 3 1 Magma Fire
3 3 4 1 Magma
3 3 5 1 Magma
3 3 6 1 Magma
3 3 7 1 Magma
3 4 0 1 Magma Fire
3 4 1 1 Magma Fire
3 4 2 1 Magma
3 4 3 1 Magma
3 4 4 1 Magma
3 4 5 1 Magma
4 -1 1 1 Mud
4 -1 2 1 Mud Rocks
4 -1 3 1 Mud
4 -1 4 1 Mud
4 -1 5 2 Mud
4 -1 6 2 Mud Rocks
4 -1 7 1 Mud
4 0 0 2 Dirt Obsidian
4 0 1 1 Mud Rocks
4 0 2 3 Mud
4 0 3 1 Mud
4 0 4 7 Dirt Obsidian
4 0 5 3 Mud Rocks
4 0 6 6 Mud
4 0 7 6 Mud
4 1 0 1 Magma
4 1 1 1 Magma Fire
4 1 2 1 Magma
4 1 3 1 Magma
4 1 4 1 Magma
4 1 5 1 Magma
4 1 6 1 Magma Fire
4 1 7 1 Magma Fire
4 2 0 1 Magma
4 2 1 1 Magma
4 2 2 1 Magma
4 2 3 1 Magma
4 2 4 1 Magma
4 2 5 1 Magma
4 2 6 1 Magma
4 2 7 1 Magma Fire
4 3 0 1 Magma
4 3 1 1 Magma
4 3 2 1 Magma
4 3 3 1 Magma
4 3 4 1 Magma
4 3 5 1 Magma
4 3 6 1 Magma
4 3 7 1 Magma
4 4 0 1 Magma
4 4 1 1 Magma
4 4 2 1 Magma
4 4 3 1 Magma
4 4 4 1 Magma Fire
4 4 5 1 Magma
5 -1 5 1 Mud
5 -1 6 2 Mud
5 0 0 1 Mud Rocks
5 0 1 1 Mud
5 0 2 2 Mud
5 0 3 3 Mud Obsidian
5 0 4 4 Mud Rocks
5 0 5 5 Mud
5 0 6 5 Mud Obsidian
5 0 7 3 Mud
5 1 0 4 Mud Rocks
5 1 1 1 Magma
5 1 2 1 Magma Fire
5 1 3 1 Magma
5 1 4 1 Magma Fire
5 1 5 1 Magma
5 1 6 1 Magma
5 1 7 1 Magma
5 2 0 1 Magma
5 2 1 1 Magma
5 2 2 1 Magma
5 2 3 1 Magma
5 2 4 1 Magma
5 2 5 1 Magma
5 2 6 1 Magma
5 2 7 1 Magma
5 3 0 1 Magma
5 3 1 1 Magma
5 3 2 1 Magma
5 3 3 1 Magma
5 3 4 1 Magma
5 3 5 1 Magma
5 3 6 1 Magma
5 3 7 1 Magma
5 4 0 1 Magma Fire
5 4 1 1 Magma
5 4 2 1 Magma
5 4 3 1 Magma
5 4 4 1 Magma
5 4 5 1 Magma Fire
6 0 1 1 Mud Cave Marker(entry)
6 0 2 1 Mud
6 0 3 2 Mud Obsidian
6 0 4 1 Mud
6 0 5 3 Mud Obsidian
6 0 6 3 Mud Rocks
6 0 7 1 Mud
6 1 0 2 Mud
6 1 1 1 Mud
6 1 2 1 Mud Rocks
6 1 3 1 Mud
6 1 4 1 Mud
6 1 5 1 Magma Fire
6 1 6 1 Magma
6 1 7 2 Mud
6 2 0 1 Magma
6 2 1 1 Magma
6 2 2 1 Magma
6 2 3 1 Magma Fire
6 2 4 1 Magma
6 2 5 1 Magma
6 2 6 1 Magma
6 2 7 1 Magma
6 3 0 1 Magma Fire
6 3 1 1 Magma
6 3 2 1 Magma
6 3 3 1 Magma Fire
6 3 4 1 Magma
6 3 5 1 Magma
6 3 6 1 Magma
6 3 7 1 Magma
6 4 0 1 Magma
6 4 1 1 Magma
6 4 2 1 Magma Fire
6 4 3 1 Magma
6 4 4 1 Magma
6 4 5 1 Magma
7 0 5 1 Mud Rocks
7 0 6 1 Mud
7 1 0 1 Mud Obsidian
7 1 1 1 Mud Rocks
7 1 2 1 Mud
7 1 3 1 Mud Obsidian
7 1 4 2 Mud Rocks
7 1 5 4 Mud
7 1 6 6 Mud
7 1 7 6 Mud Rocks
7 2 0 2 Mud
7 2 1 1 Magma
7 2 2 1 Mud Obsidian
7 2 3 1 Magma
7 2 4 1 Magma
7 2 5 1 Magma
7 2 6 1 Magma
7 2 7 1 Magma
7 3 0 1 Magma
7 3 1 1 Magma
7 3 2 1 Magma
7 3 3 1 Magma
7 3 4 1 Magma
7 3 5 1 Magma
7 3 6 1 Magma
7 3 7 1 Magma Fire
7 4 0 1 Magma
7 4 1 1 Magma
7 4 2 1 Magma
7 4 3 1 Magma
7 4 4 1 Magma
7 4 5 1 Magma
8 1 1 4 Mud
8 1 3 2 Mud
8 1 4 1 Mud
8 1 5 1 Mud
8 1 6 1 Mud
8 2 0 4 Mud
8 2 1 4 Mud
8 2 2 3 Mud Rocks
8 2 3 4 Mud
8 2 4 1 Magma
8 2 5 1 Magma
8 2 6 1 Magma
8 2 7 1 Magma Fire
8 3 0 1 Magma Fire
8 3 1 1 Magma
8 3 2 1 Magma
8 3 3 1 Magma
8 3 4 1 Magma
8 3 5 1 Magma Fire
8 3 6 1 Magma
8 3 7 1 Magma Fire
8 4 0 1 Magma
8 4 1 1 Magma
8 4 2 1 Magma
8 4 3 1 Magma Fire
9 3 1 1 Magma
9 3 3 1 Magma
9 3 5 1 Magma
9 4 1 1 Magma
";
  }
}
