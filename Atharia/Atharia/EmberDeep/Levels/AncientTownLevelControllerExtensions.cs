﻿using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class AncientTownLevelControllerExtensions {
    public static Atharia.Model.Void Destruct(this AncientTownLevelController self) {
      self.Delete();
      return new Atharia.Model.Void();
    }

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

      level.terrain.tiles[levelSuperstate.FindMarkerLocation("exit")].components.Add(
        game.root.EffectEmberDeepLevelLinkerTTCCreate(depth + 1).AsITerrainTileComponent());

      foreach (var loc in levelSuperstate.FindMarkersLocations("lightningTrask", 3)) {
        var lightningTrask = LightningTrask.Make(level.root);
        lightningTrask.components.Add(
          level.root.EffectGuardAICapabilityUCCreate(loc, 4).AsIUnitComponent());
        level.EnterUnit(levelSuperstate, loc, level.time, lightningTrask);
      }

      var entryLocation = levelSuperstate.FindMarkerLocation("entry");

      levelSuperstate.AddNoUnitZone(entryLocation, 5);

      int numSpaces = levelSuperstate.NumWalkableLocations(false);
      EmberDeepUnitsAndItems.FillWithUnits(
        game.rand,
        level,
        levelSuperstate,
        (loc) => !loc.Equals(entryLocation),
        /*numIrkling=*/ 0 * numSpaces / 200,
        /*numDraxling=*/ 10 * numSpaces / 200,
        /*numRavagianTrask=*/ 3 * numSpaces / 200,
        /*numBaug=*/ 0 * numSpaces / 200,
        /*numSpirient=*/ 1 * numSpaces / 200,
        /*numIrklingKing=*/ 3 * numSpaces / 200,
        /*numEmberfolk=*/ 5 * numSpaces / 200,
        /*numChronolisk=*/ 3 * numSpaces / 200,
        /*numMantisBombardier=*/ 2 * numSpaces / 200,
        /*numLightningTrask=*/ 0 * numSpaces / 200);

      EmberDeepUnitsAndItems.PlaceItems(game.rand, level, levelSuperstate, (loc) => !loc.Equals(entryLocation), .04f, .04f);

      level.controller = game.root.EffectAncientTownLevelControllerCreate(level).AsILevelController();

      game.levels.Add(level);
      entryLocationRet = entryLocation;
    }

    public static string GetName(this AncientTownLevelController obj) {
      return "AncientTown";
    }

    public static Atharia.Model.Void SimpleTrigger(
        this AncientTownLevelController obj,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        string triggerName) {
      game.root.logger.Info("Got simple trigger: " + triggerName);

      if (triggerName == "levelStart") {
        game.EnterCinematic();
        game.Wait(1500);
        game.FlyCameraTo(1000, superstate.levelSuperstate.FindMarkerLocation("cameraPanTo"));
        game.Wait(800);
        game.FlyCameraTo(1500, superstate.levelSuperstate.FindMarkerLocation("entry"));
        game.player.nextActionTime = game.level.time;
        game.ExitCinematic();
        game.ShowAside("kylin", "Some sort of ancient abandoned town...?");
      }

      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this AncientTownLevelController obj,
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
-8 0 7 24 Mud
-7 -1 6 23 Mud
-7 -1 7 21 Floor
-7 0 0 22 Mud
-7 0 2 23 Mud
-7 0 3 26 Mud
-7 0 4 24 Mud
-7 0 5 23 Mud
-7 0 6 22 Floor
-7 0 7 22 Floor
-7 1 0 25 Mud
-7 1 2 28 Mud
-7 1 7 22 Mud
-6 -2 6 15 Floor
-6 -2 7 15 Floor
-6 -1 0 15 Floor
-6 -1 2 17 Mud
-6 -1 4 17 Mud
-6 -1 5 19 Mud
-6 -1 6 21 Floor
-6 -1 7 16 Mud
-6 0 0 21 Floor
-6 0 1 21 Floor
-6 0 2 21 Floor
-6 0 3 20 Mud
-6 0 4 19 Mud
-6 0 5 21 Mud
-6 0 6 18 Rocks Dirt
-6 0 7 17 Rocks Dirt
-6 1 0 22 Floor
-6 1 1 22 Floor
-6 1 2 22 Floor
-6 1 3 23 Mud
-6 1 4 22 Floor
-6 1 5 25 Mud
-6 1 6 21 Mud
-6 1 7 19 Mud
-6 2 0 24 Mud
-6 2 2 23 Mud
-6 2 4 25 Mud
-6 2 7 22 Mud
-5 -3 6 3 Mud
-5 -3 7 1 Rocks Dirt
-5 -2 0 5 Mud
-5 -2 2 6 Mud
-5 -2 3 9 Mud
-5 -2 4 8 Mud
-5 -2 5 10 Mud
-5 -2 6 10 Dirt Rocks
-5 -2 7 8 Mud
-5 -1 0 15 Floor
-5 -1 1 15 Floor
-5 -1 2 15 Floor
-5 -1 3 15 Floor
-5 -1 4 15 Floor
-5 -1 5 15 Floor
-5 -1 6 15 Mud
-5 -1 7 14 Mud
-5 0 0 16 Rocks Dirt
-5 0 1 21 Floor
-5 0 2 16 Rocks Dirt
-5 0 3 21 Floor
-5 0 4 16 Rocks Dirt
-5 0 5 17 Mud
-5 0 6 17 Rocks Dirt
-5 0 7 16 Rocks Dirt
-5 1 0 17 Rocks Dirt
-5 1 1 19 Mud
-5 1 2 17 Rocks Dirt
-5 1 3 18 Mud
-5 1 4 17 Rocks Dirt
-5 1 5 18 Mud
-5 1 6 17 Rocks Dirt
-5 1 7 17 Rocks Dirt
-5 2 0 16 Mud Rocks
-5 2 1 20 Mud
-5 2 2 21 Floor
-5 2 3 21 Floor
-5 2 4 21 Floor
-5 2 5 21 Floor
-5 2 6 21 Mud
-5 2 7 19 Mud
-5 3 0 23 Mud
-5 3 2 25 Mud
-4 -3 6 1 Rocks Dirt
-4 -3 7 1 Rocks Dirt
-4 -2 0 2 Rocks Dirt
-4 -2 1 2 Mud
-4 -2 2 3 Rocks Dirt
-4 -2 3 3 Mud
-4 -2 4 4 Rocks Dirt
-4 -2 5 6 Mud
-4 -2 6 7 Mud
-4 -2 7 6 Rocks Dirt
-4 -1 0 8 Dirt Rocks
-4 -1 1 9 Dirt Rocks
-4 -1 2 9 Mud
-4 -1 3 10 Mud
-4 -1 4 10 Mud
-4 -1 5 12 Mud
-4 -1 6 12 Mud
-4 -1 7 11 Mud
-4 0 0 14 Mud
-4 0 1 15 Mud
-4 0 2 14 Mud
-4 0 3 15 Mud
-4 0 4 15 Mud
-4 0 5 15 Mud
-4 0 6 15 Rocks Dirt
-4 0 7 14 Rocks Dirt
-4 1 0 16 Mud
-4 1 1 17 Mud
-4 1 2 17 Mud
-4 1 3 17 Mud
-4 1 4 17 Mud
-4 1 5 17 Mud
-4 1 6 17 Mud
-4 1 7 16 Mud
-4 2 0 16 Mud
-4 2 1 16 Rocks Dirt
-4 2 2 17 Mud
-4 2 3 21 Floor
-4 2 4 21 Floor
-4 2 5 21 Floor
-4 2 6 17 Mud
-4 2 7 16 Mud
-4 3 0 16 Mud
-4 3 1 18 Mud
-4 3 2 16 Mud
-4 3 3 19 Mud
-4 3 4 16 Mud
-4 3 5 25 Mud
-4 3 6 16 Mud
-4 3 7 16 Mud
-3 -3 6 1 Mud
-3 -3 7 1 Mud
-3 -2 0 2 Mud
-3 -2 1 2 Rocks Dirt Marker(entry)
-3 -2 2 3 Mud
-3 -2 3 3 Rocks Dirt
-3 -2 4 4 Mud
-3 -2 5 4 Rocks Dirt
-3 -2 6 6 Rocks Dirt
-3 -2 7 5 Mud
-3 -1 0 7 Rocks Dirt
-3 -1 1 8 Dirt Rocks
-3 -1 2 12 Floor
-3 -1 3 12 Floor
-3 -1 4 12 Floor
-3 -1 5 12 Floor
-3 -1 6 11 Mud
-3 -1 7 12 Floor
-3 0 0 11 Rocks Dirt
-3 0 1 12 Rocks Dirt
-3 0 2 12 Mud
-3 0 3 12 Rocks Dirt
-3 0 4 10 Dirt
-3 0 5 14 Rocks Dirt
-3 0 6 10 Dirt
-3 0 7 10 Dirt
-3 1 0 14 Mud
-3 1 1 15 Mud
-3 1 2 15 Mud
-3 1 3 15 Mud
-3 1 4 15 Mud
-3 1 5 16 Mud
-3 1 6 19 Floor
-3 1 7 19 Floor
-3 2 0 19 Floor
-3 2 1 17 Mud
-3 2 2 19 Floor
-3 2 3 17 Mud
-3 2 4 16 Mud
-3 2 5 16 Mud
-3 2 6 16 Mud
-3 2 7 16 Mud
-3 3 0 16 Mud
-3 3 1 16 Mud Marker(lightningTrask)
-3 3 2 16 Mud Marker(lightningTrask)
-3 3 3 16 Mud
-3 3 4 16 Mud Marker(lightningTrask)
-3 3 5 16 Mud
-3 3 6 16 Mud
-3 3 7 16 Mud
-3 4 0 16 Mud
-3 4 1 16 Mud Cave Marker(exit)
-2 -2 0 1 Mud
-2 -2 1 1 Mud
-2 -2 2 6 Floor
-2 -2 3 3 Mud
-2 -2 4 6 Floor
-2 -2 5 4 Mud
-2 -2 6 5 Mud
-2 -2 7 6 Floor
-2 -1 0 6 Rocks Dirt
-2 -1 1 6 Rocks Dirt
-2 -1 2 6 Rocks Dirt
-2 -1 3 7 Rocks Dirt
-2 -1 4 8 Rocks Dirt
-2 -1 5 12 Floor
-2 -1 6 9 Rocks Dirt
-2 -1 7 8 Rocks Dirt Marker(cameraPanTo)
-2 0 0 9 Rocks Dirt
-2 0 1 10 Rocks Dirt
-2 0 2 10 Rocks Dirt
-2 0 3 10 Dirt
-2 0 4 10 Dirt
-2 0 5 10 Dirt
-2 0 6 10 Dirt
-2 0 7 10 Dirt
-2 1 0 10 Dirt
-2 1 1 10 Dirt
-2 1 2 10 Dirt
-2 1 3 10 Dirt
-2 1 4 10 Dirt
-2 1 5 16 Mud
-2 1 6 16 Mud
-2 1 7 16 Mud
-2 2 0 19 Mud Floor
-2 2 1 19 Floor
-2 2 2 19 Dirt Mud Floor
-2 2 3 19 Floor
-2 2 4 19 Floor
-2 2 5 19 Floor
-2 2 6 19 Floor
-2 2 7 19 Floor
-2 3 0 16 Mud
-2 3 1 16 Mud
-2 3 2 16 Mud
-2 3 3 16 Mud Marker(lightningTrask)
-2 3 4 17 Mud
-2 3 5 16 Mud
-2 3 6 20 Mud
-2 3 7 19 Mud
-2 4 0 22 Mud
-2 4 1 21 Mud
-2 4 2 25 Mud
-2 4 3 23 Mud
-1 -2 0 1 Mud
-1 -2 1 1 Mud
-1 -2 2 1 Dirt Rocks
-1 -2 3 6 Floor
-1 -2 4 6 Floor
-1 -2 5 6 Floor
-1 -2 6 6 Floor
-1 -2 7 6 Floor
-1 -1 0 5 Mud
-1 -1 1 5 Mud
-1 -1 2 6 Dirt Rocks
-1 -1 3 6 Rocks Dirt
-1 -1 4 6 Mud
-1 -1 5 7 Rocks Dirt
-1 -1 6 8 Mud
-1 -1 7 5 Mud
-1 0 0 9 Rocks Dirt
-1 0 1 9 Rocks Dirt
-1 0 2 9 Rocks Dirt
-1 0 3 10 Rocks Dirt
-1 0 4 10 Rocks Dirt
-1 0 5 10 Dirt
-1 0 6 10 Dirt
-1 0 7 10 Rocks Dirt
-1 1 0 10 Dirt
-1 1 1 10 Dirt
-1 1 2 10 Dirt
-1 1 3 10 Dirt
-1 1 4 10 Dirt
-1 1 5 10 Dirt
-1 1 6 16 Mud
-1 1 7 14 Mud
-1 2 0 14 Mud
-1 2 1 14 Mud
-1 2 2 14 Dirt Rocks
-1 2 3 14 Dirt Rocks
-1 2 4 14 Dirt Rocks
-1 2 5 14 Dirt Rocks
-1 2 6 15 Mud
-1 2 7 15 Mud
-1 3 0 15 Mud
-1 3 1 15 Mud
-1 3 2 17 Mud
-1 3 3 17 Mud
-1 3 4 17 Mud
-1 3 5 17 Mud
-1 3 6 22 Floor
-1 3 7 22 Floor
-1 4 0 23 Mud
-1 4 1 22 Mud
-1 4 2 26 Mud
-1 4 3 27 Mud
0 -2 1 1 Mud
0 -2 2 1 Mud
0 -2 3 1 Dirt Rocks
0 -2 4 2 Mud
0 -2 5 1 Dirt Rocks
0 -2 6 2 Dirt Rocks
0 -2 7 3 Mud
0 -1 0 4 Dirt Rocks
0 -1 1 4 Mud
0 -1 2 4 Dirt Rocks
0 -1 3 5 Dirt Rocks
0 -1 4 6 Mud
0 -1 5 6 Mud
0 -1 6 5 Mud
0 -1 7 5 Mud
0 0 0 4 Mud Rocks
0 0 1 9 Floor
0 0 2 9 Floor
0 0 3 9 Floor
0 0 4 9 Floor
0 0 5 9 Rocks Dirt
0 0 6 9 Rocks Dirt
0 0 7 9 Mud
0 1 0 10 Rocks Dirt
0 1 1 10 Dirt
0 1 2 11 Mud
0 1 3 10 Dirt
0 1 4 11 Mud
0 1 5 10 Dirt
0 1 6 13 Mud
0 1 7 12 Mud
0 2 0 14 Mud
0 2 1 14 Mud
0 2 2 14 Dirt Rocks
0 2 3 14 Dirt Rocks
0 2 4 15 Dirt Rocks
0 2 5 15 Dirt Rocks
0 2 6 15 Mud
0 2 7 15 Mud
0 3 0 15 Mud
0 3 1 15 Mud
0 3 2 17 Dirt Rocks
0 3 3 17 Mud
0 3 4 17 Dirt Rocks
0 3 5 17 Dirt Rocks
0 3 6 22 Floor
0 3 7 22 Floor
0 4 0 22 Floor
0 4 1 24 Mud
0 4 2 26 Mud
0 4 3 27 Mud
1 -2 3 1 Mud
1 -2 4 1 Mud
1 -2 5 2 Mud
1 -2 6 5 Mud Dirt Floor
1 -2 7 1 Mud
1 -1 0 5 Mud Dirt Floor
1 -1 1 4 Dirt Rocks
1 -1 2 5 Mud Dirt Floor
1 -1 3 4 Mud
1 -1 4 4 Mud
1 -1 5 5 Mud
1 -1 6 6 Mud
1 -1 7 5 Mud
1 0 0 5 Mud Rocks
1 0 1 4 Mud Rocks
1 0 2 9 Floor
1 0 3 9 Floor
1 0 4 9 Floor
1 0 5 9 Floor
1 0 6 9 Mud
1 0 7 9 Dirt Rocks
1 1 0 10 Mud
1 1 1 10 Rocks Dirt
1 1 2 11 Dirt Rocks
1 1 3 11 Rocks Dirt
1 1 4 12 Dirt Rocks
1 1 5 12 Dirt Rocks
1 1 6 13 Dirt Rocks
1 1 7 12 Dirt Rocks
1 2 0 13 Mud
1 2 1 14 Mud
1 2 2 14 Mud
1 2 3 15 Dirt Rocks
1 2 4 15 Dirt Rocks
1 2 5 15 Dirt Rocks
1 2 6 15 Dirt Rocks
1 2 7 15 Dirt Rocks
1 3 0 15 Dirt Rocks
1 3 1 15 Mud
1 3 2 17 Dirt Rocks
1 3 3 17 Dirt Rocks
1 3 4 18 Mud
1 3 5 17 Mud
1 3 6 22 Floor
1 3 7 23 Mud
1 4 0 24 Mud
1 4 1 22 Mud Floor
1 4 2 27 Mud
1 4 3 28 Mud
2 -1 0 1 Mud
2 -1 1 6 Floor
2 -1 2 1 Dirt Rocks
2 -1 3 6 Floor
2 -1 4 6 Floor
2 -1 5 6 Floor
2 -1 6 5 Mud
2 -1 7 4 Mud
2 0 0 6 Mud
2 0 1 6 Mud Rocks
2 0 2 6 Dirt Rocks
2 0 3 7 Mud
2 0 4 7 Dirt Rocks
2 0 5 8 Mud
2 0 6 8 Dirt Rocks
2 0 7 8 Rocks Dirt
2 1 0 9 Dirt Rocks
2 1 1 10 Dirt Rocks
2 1 2 10 Dirt Rocks
2 1 3 11 Dirt Rocks
2 1 4 11 Dirt Rocks
2 1 5 12 Dirt Rocks
2 1 6 12 Mud
2 1 7 12 Dirt Rocks
2 2 0 13 Dirt Rocks
2 2 1 13 Dirt Rocks
2 2 2 14 Dirt Rocks
2 2 3 14 Dirt Rocks
2 2 4 15 Dirt Rocks
2 2 5 15 Dirt Rocks
2 2 6 15 Dirt Rocks
2 2 7 15 Mud
2 3 0 17 Mud
2 3 1 15 Mud
2 3 2 17 Mud
2 3 3 17 Mud
2 3 4 21 Floor
2 3 5 19 Mud
2 3 6 24 Mud
2 3 7 21 Floor
2 4 0 26 Mud
2 4 1 27 Mud
3 -1 1 1 Mud
3 -1 2 1 Mud
3 -1 3 2 Dirt Rocks
3 -1 4 2 Mud
3 -1 5 3 Dirt Rocks
3 -1 6 3 Dirt Rocks
3 -1 7 3 Mud
3 0 0 5 Dirt Rocks
3 0 1 5 Dirt Rocks
3 0 2 6 Dirt Rocks
3 0 3 6 Dirt Rocks
3 0 4 6 Mud
3 0 5 7 Dirt Rocks
3 0 6 8 Mud
3 0 7 10 Floor
3 1 0 9 Rocks Dirt
3 1 1 9 Rocks Dirt
3 1 2 10 Dirt Rocks
3 1 3 10 Mud
3 1 4 13 Mud Floor
3 1 5 11 Mud
3 1 6 12 Dirt Rocks
3 1 7 13 Mud Floor
3 2 0 13 Dirt Rocks
3 2 1 17 Floor
3 2 2 17 Floor
3 2 3 17 Floor
3 2 4 17 Floor
3 2 5 17 Floor
3 2 6 17 Floor
3 2 7 17 Floor
3 3 0 17 Mud
3 3 1 17 Mud Dirt Rocks
3 3 2 17 Mud
3 3 3 17 Dirt Rocks Mud
3 3 4 21 Floor
3 3 5 21 Floor
3 3 6 25 Mud
3 3 7 24 Mud
3 4 0 26 Mud
3 4 1 28 Mud
4 -1 3 1 Mud
4 -1 4 1 Mud
4 -1 5 2 Mud
4 -1 6 2 Mud
4 -1 7 1 Mud
4 0 0 3 Dirt Rocks
4 0 1 4 Dirt Rocks
4 0 2 4 Dirt Rocks
4 0 3 5 Dirt Rocks
4 0 4 6 Dirt Rocks
4 0 5 6 Mud
4 0 6 10 Floor
4 0 7 10 Floor
4 1 0 10 Floor
4 1 1 8 Mud
4 1 2 8 Mud
4 1 3 9 Dirt Rocks
4 1 4 13 Mud Floor
4 1 5 13 Mud Floor
4 1 6 13 Mud Floor
4 1 7 13 Mud Floor
4 2 0 12 Mud
4 2 1 13 Dirt Rocks
4 2 2 13 Dirt Rocks
4 2 3 14 Mud
4 2 4 15 Mud
4 2 5 17 Floor
4 2 6 15 Mud
4 2 7 20 Floor
4 3 0 18 Mud
4 3 1 17 Mud
4 3 2 19 Mud
4 3 3 18 Mud
4 3 4 27 Mud
4 3 5 21 Floor
4 3 6 28 Mud
4 4 0 26 Mud
4 4 1 27 Mud
5 0 0 1 Dirt Rocks
5 0 1 2 Dirt Rocks
5 0 2 2 Dirt Rocks
5 0 3 3 Dirt Rocks
5 0 4 3 Mud
5 0 5 5 Mud
5 0 6 10 Floor
5 0 7 6 Mud
5 1 0 10 Floor
5 1 1 10 Floor
5 1 2 10 Floor
5 1 3 9 Mud
5 1 4 9 Mud
5 1 5 13 Mud Floor
5 1 6 11 Mud
5 1 7 10 Mud
5 2 0 12 Mud
5 2 1 12 Mud
5 2 2 15 Mud
5 2 3 15 Dirt Rocks
5 2 4 20 Floor
5 2 5 20 Floor
5 2 6 20 Floor
5 2 7 20 Floor
5 3 0 20 Floor
5 3 1 20 Mud
5 3 2 24 Mud
5 3 3 22 Mud
5 3 5 23 Mud
6 0 3 1 Dirt Rocks
6 0 5 5 Mud
6 1 0 8 Mud
6 1 1 7 Mud
6 1 2 8 Mud
6 1 3 8 Mud
6 1 4 10 Mud
6 1 5 10 Mud
6 2 0 13 Mud
6 2 1 11 Mud
6 2 2 13 Mud
6 2 3 15 Mud
6 2 5 18 Mud
6 3 1 23 Mud
";
  }
}
