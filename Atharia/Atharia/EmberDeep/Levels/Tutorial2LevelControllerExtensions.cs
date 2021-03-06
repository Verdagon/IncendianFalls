﻿using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class Tutorial2LevelControllerExtensions {
    public static Atharia.Model.Void Destruct(this Tutorial2LevelController self) {
      self.Delete();
      return new Atharia.Model.Void();
    }

    public static void LoadLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocation,
        out Location exitLocation,
        Game game) {
      level =
        game.root.EffectLevelCreate(
          new Vec3(0, -8, 16),
          game.root.EffectTerrainCreate(
            PentagonPattern9.makePentagon9Pattern(),
            false,
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

      var summonLocations = new SortedSet<Location>(levelSuperstate.FindMarkersLocations("summon", 2));

      var traskLocations = SetUtils.GetRandomN(summonLocations, game.rand, 3, 2);
      foreach (var traskLocation in traskLocations) {
        var randNum = game.rand.Next() % 6;
        Unit unit = RavagianTrask.Make(game.root);
        int time = level.time + 300;
        level.EnterUnit(levelSuperstate, traskLocation, time, unit);
        summonLocations.Remove(traskLocation);
      }

      foreach (var summonLocation in summonLocations) {
        var randNum = game.rand.Next() % 5;
        Unit unit;
        if (randNum == 1) {
          unit = Baug.Make(game.root);
        } else {
          unit = Irkling.Make(game.root);
        }
        int time = level.time + 300;
        level.EnterUnit(levelSuperstate, summonLocation, time, unit);
      }

      level.controller = game.root.EffectTutorial2LevelControllerCreate(level).AsILevelController();

      game.levels.Add(level);

      entryLocation = levelSuperstate.FindMarkerLocation("start");
      exitLocation = levelSuperstate.FindMarkerLocation("exit");
    }

    public static string GetName(this Tutorial2LevelController obj) {
      return "Tutorial";
    }

    public static bool ConsiderCornersAdjacent(this Tutorial2LevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this Tutorial2LevelController obj,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        string triggerName) {
      game.root.logger.Error("Got simple trigger: " + triggerName);

      if (triggerName == "levelStart") {
        game.player.hp = game.player.maxHp;
        var sorcerous = game.player.components.GetOnlySorcerousUCOrNull();
        if (sorcerous.Exists()) {
          sorcerous.mp = sorcerous.maxMp;
        }
        foreach (var item in game.player.components.GetAllIItem()) {
          game.player.components.Remove(item.AsIUnitComponent());
          item.Destruct();
        }
        //game.player.components.Add(game.root.EffectBlastRodCreate().AsIUnitComponent());

        game.ShowAside("Ambush Challenge");
        game.Wait(1000);
        game.ShowDialogue("kylin", "An ambush! Perhaps I can use the terrain and chronomancy to survive.", "For valor!");
        game.ShowAside("Hint: to survive ambushes, you often need several past selves at once.");
      }

      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this Tutorial2LevelController obj,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      game.root.logger.Info("Got simple unit trigger: " + triggerName);
      return new Atharia.Model.Void();
    }
    
    private static string LEVEL = @"
-4 -1 4 5 Mud
-4 -1 7 5 Mud
-3 -1 0 7 Mud
-3 -1 2 7 Mud
-3 -1 4 4 Mud
-3 -1 5 5 Mud
-3 -1 6 1 Mud
-3 -1 7 1 Magma
-3 0 0 4 Mud
-3 0 2 5 Mud
-3 0 4 6 Mud
-3 0 7 7 Mud
-2 -1 0 7 Mud
-2 -1 1 6 Mud
-2 -1 2 1 Mud
-2 -1 3 3 Mud
-2 -1 4 2 Mud Rocks Marker(summon)
-2 -1 5 2 Mud
-2 -1 6 3 Mud Marker(summon)
-2 -1 7 3 Dirt Rocks
-2 0 0 3 Mud
-2 0 1 1 Mud
-2 0 2 3 Dirt Marker(summon)
-2 0 3 2 Mud
-2 0 4 3 Mud
-2 0 5 2 Mud
-2 0 6 1 Magma
-2 0 7 2 Mud Marker(summon)
-2 1 0 6 Mud
-1 -1 2 7 Mud
-1 -1 3 5 Mud
-1 -1 4 1 Magma
-1 -1 5 2 Mud
-1 -1 6 2 Mud
-1 -1 7 3 Mud Rocks
-1 0 0 2 Mud
-1 0 1 3 Mud Marker(summon)
-1 0 2 1 Magma
-1 0 3 2 Mud Marker(summon)
-1 0 4 3 Mud
-1 0 5 3 Mud
-1 0 6 2 Mud Rocks
-1 0 7 2 Mud
-1 1 0 1 Mud Cave Marker(exit)
-1 1 1 6 Mud
-1 1 2 7 Mud
0 -1 2 6 Mud
0 -1 3 7 Mud
0 -1 4 2 Mud
0 -1 5 3 Dirt
0 -1 6 2 Dirt
0 -1 7 3 Mud
0 0 0 1 Magma
0 0 1 3 Mud
0 0 2 2 Mud
0 0 3 2 Mud
0 0 4 3 Mud Marker(summon)
0 0 5 3 Dirt Rocks Marker(summon)
0 0 6 2 Mud
0 0 7 3 Mud Marker(summon)
0 1 0 1 Magma
0 1 1 2 Mud Marker(summon)
0 1 2 2 Mud
0 1 3 1 Mud
0 1 4 3 Mud
0 1 5 6 Mud
0 1 6 7 Mud
0 1 7 7 Mud
1 -1 3 7 Mud
1 -1 4 6 Mud
1 -1 5 3 Mud HealthPotion
1 -1 6 2 Mud Rocks
1 -1 7 6 Mud
1 0 0 3 Mud
1 0 1 2 Dirt
1 0 2 3 Dirt Marker(start)
1 0 3 3 Mud Rocks
1 0 4 2 Dirt
1 0 5 1 Magma
1 0 6 1 Magma
1 0 7 2 Dirt
1 1 0 2 Mud Marker(summon)
1 1 1 3 Mud
1 1 2 3 Mud Rocks
1 1 3 3 Dirt Marker(summon)
1 1 4 2 Mud
1 1 5 2 Mud
1 1 6 7 Mud
1 1 7 6 Mud
2 0 0 7 Mud
2 0 1 2 Mud HealthPotion
2 0 2 6 Mud
2 0 3 2 Mud
2 0 4 2 Mud HealthPotion
2 0 5 3 Mud
2 0 6 3 Dirt
2 0 7 2 Mud Rocks HealthPotion
2 1 0 3 Mud Rocks
2 1 1 3 Mud
2 1 2 2 Mud
2 1 3 3 Mud Marker(summon)
2 1 4 2 Mud Marker(summon)
2 1 5 3 Dirt
2 1 6 7 Mud
2 1 7 7 Mud
3 0 3 7 Mud
3 0 5 7 Mud
3 0 6 6 Mud
3 1 0 2 Mud
3 1 1 2 Dirt
3 1 2 7 Mud
3 1 3 3 Mud
3 1 4 7 Mud
3 1 5 6 Mud
4 1 1 6 Mud
4 1 2 7 Mud
4 1 3 7 Mud
";
  }
}
