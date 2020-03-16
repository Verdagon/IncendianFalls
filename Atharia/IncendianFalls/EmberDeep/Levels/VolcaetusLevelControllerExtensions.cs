using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class VolcaetusLevelControllerExtensions {
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

      level.terrain.tiles[levelSuperstate.FindSimplePresenceTriggerLocation("volcaetus")].components.Add(
        level.root.EffectItemTTCCreate(
          level.root.EffectGlaiveCreate().AsIItem())
        .AsITerrainTileComponent());

      var entryLocation = levelSuperstate.FindMarkerLocation("entry");
      entryLocationRet = entryLocation;

      level.controller = game.root.EffectVolcaetusLevelControllerCreate(level).AsILevelController();

      game.levels.Add(level);

      entryLocation = levelSuperstate.FindMarkerLocation("entry");
    }

    public static string GetName(this VolcaetusLevelController obj) {
      return "Volcaetus";
    }

    public static bool ConsiderCornersAdjacent(this VolcaetusLevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this VolcaetusLevelController obj,
        Game game,
        Superstate superstate,
        string triggerName) {
      game.root.logger.Info("Got simple trigger: " + triggerName);

      if (triggerName == "line2") {
        game.AddEvent(
          new ShowOverlayEvent(
            "I can finally rescue my brother!",
            "dramatic",
            new ButtonImmList(new List<Button>() { new Button("Hope lives!", "line3") }))
          .AsIGameEvent());
      }
      if (triggerName == "line3") {
        game.AddEvent(
          new ShowOverlayEvent(
            "Congratulations, you have won the game!",
            "dramatic",
            new ButtonImmList(new List<Button>() { new Button("Huzzah!", "_exitGame") }))
          .AsIGameEvent());
      }

      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this VolcaetusLevelController obj,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      game.root.logger.Info("Got simple unit trigger: " + triggerName);

      if (triggeringUnit.NullableIs(game.player) && triggerName == "volcaetus") {
        game.AddEvent(
          new ShowOverlayEvent(
            "This is... this is Volcaetus!\n\nThe black incendium spear!",
            "normal",
            new ButtonImmList(new List<Button>() { new Button("Behold!", "line2") }))
          .AsIGameEvent());
      }

      return new Atharia.Model.Void();
    }

    private static string LEVEL = @"
-3 -2 6 1 Dirt Rocks
-3 -2 7 1 Dirt
-3 -1 0 1 Dirt
-3 -1 2 1 Dirt
-3 -1 6 3 Dirt
-3 -1 7 1 Dirt
-2 -2 6 1 Dirt
-2 -2 7 1 Dirt
-2 -1 0 2 Dirt
-2 -1 1 3 Dirt
-2 -1 2 2 Dirt Rocks
-2 -1 3 1 Dirt
-2 -1 4 1 Dirt
-2 -1 5 1 Dirt
-2 -1 6 2 Dirt Rocks
-2 -1 7 2 Dirt
-2 0 0 3 Dirt
-2 0 1 2 Dirt
-2 0 2 4 Mud
-2 0 3 1 Mud
-2 0 4 4 Mud
-2 0 5 1 Mud
-2 0 6 4 Mud Rocks
-2 0 7 5 ObsidianFloor
-1 -2 6 3 Dirt Rocks
-1 -1 0 1 Dirt
-1 -1 1 1 Dirt Marker(entry)
-1 -1 2 1 Dirt
-1 -1 3 1 Dirt
-1 -1 4 1 Dirt
-1 -1 5 2 Dirt
-1 -1 6 3 Mud
-1 -1 7 3 Mud Rocks
-1 0 0 3 Mud
-1 0 1 3 Mud
-1 0 2 5 ObsidianFloor Obsidian
-1 0 3 5 ObsidianFloor Obsidian
-1 0 4 6 ObsidianFloor
-1 0 5 6 ObsidianFloor
-1 0 6 7 ObsidianFloor
-1 0 7 7 ObsidianFloor
-1 1 0 5 ObsidianFloor
-1 1 1 4 Mud
-1 1 2 5 ObsidianFloor Obsidian
-1 1 3 4 Mud
-1 1 4 4 Mud Rocks
-1 1 7 4 Mud Rocks
0 -1 1 4 Dirt
0 -1 2 2 Dirt
0 -1 3 1 Dirt
0 -1 4 2 Dirt
0 -1 5 2 Dirt
0 -1 6 3 Mud
0 -1 7 2 Dirt Rocks
0 0 0 4 Mud
0 0 1 4 Mud
0 0 2 5 ObsidianFloor
0 0 3 6 ObsidianFloor
0 0 4 7 ObsidianFloor
0 0 5 7 ObsidianFloor
0 0 6 7 ObsidianFloor
0 0 7 7 ObsidianFloor
0 1 0 7 ObsidianFloor
0 1 1 7 ObsidianFloor
0 1 2 7 ObsidianFloor Obsidian
0 1 3 7 ObsidianFloor
0 1 4 7 ObsidianFloor
0 1 5 5 ObsidianFloor
0 1 6 5 ObsidianFloor
0 1 7 5 ObsidianFloor
0 2 0 4 Mud
1 -1 5 1 Dirt Rocks
1 -1 6 1 Dirt
1 0 0 3 Mud
1 0 1 4 Mud
1 0 2 4 Mud
1 0 3 5 ObsidianFloor
1 0 4 6 ObsidianFloor Obsidian
1 0 5 7 ObsidianFloor
1 0 6 7 ObsidianFloor
1 0 7 5 ObsidianFloor
1 1 0 7 ObsidianFloor Obsidian
1 1 1 7 ObsidianFloor
1 1 2 7 ObsidianFloor
1 1 3 7 ObsidianFloor Trigger(volcaetus)
1 1 4 7 ObsidianFloor
1 1 5 7 ObsidianFloor
1 1 6 5 ObsidianFloor Obsidian
1 1 7 5 ObsidianFloor
1 2 0 4 Mud
1 2 1 4 Mud
2 0 1 1 Mud
2 0 3 4 Mud
2 0 4 4 Mud
2 0 5 5 Mud
2 0 6 4 Mud Rocks
2 0 7 4 Mud
2 1 0 5 ObsidianFloor
2 1 1 7 ObsidianFloor
2 1 2 5 ObsidianFloor
2 1 3 7 ObsidianFloor
2 1 4 5 ObsidianFloor
2 1 5 5 ObsidianFloor
2 1 6 4 Mud Rocks
2 1 7 4 Mud
2 2 0 4 Mud
2 2 1 4 Mud Rocks
3 1 1 4 Mud Rocks
3 1 3 4 Mud
3 1 5 4 Mud
";
  }
}
