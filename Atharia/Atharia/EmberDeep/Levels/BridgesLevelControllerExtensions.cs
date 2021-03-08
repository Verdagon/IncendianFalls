using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class BridgesLevelControllerExtensions {
    public static Atharia.Model.Void Destruct(this BridgesLevelController self) {
      self.Delete();
      return new Atharia.Model.Void();
    }

    public static void LoadLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocation,
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
      int numSpaces = levelSuperstate.NumWalkableLocations(false);

      var exitLocation = levelSuperstate.FindMarkerLocation("exit");
      level.terrain.tiles[exitLocation].components.Add(
        game.root.EffectEmberDeepLevelLinkerTTCCreate(depth + 1).AsITerrainTileComponent());

      level.terrain.tiles[levelSuperstate.FindMarkerLocation("teleport1a")].components.Add(
        game.root.EffectWarperTTCCreate(levelSuperstate.FindMarkerLocation("teleport1b")).AsITerrainTileComponent());
      level.terrain.tiles[levelSuperstate.FindMarkerLocation("teleport1b")].components.Add(
        game.root.EffectWarperTTCCreate(levelSuperstate.FindMarkerLocation("teleport1a")).AsITerrainTileComponent());

      level.terrain.tiles[levelSuperstate.FindMarkerLocation("teleport2a")].components.Add(
        game.root.EffectWarperTTCCreate(levelSuperstate.FindMarkerLocation("teleport2b")).AsITerrainTileComponent());
      level.terrain.tiles[levelSuperstate.FindMarkerLocation("teleport2b")].components.Add(
        game.root.EffectWarperTTCCreate(levelSuperstate.FindMarkerLocation("teleport2a")).AsITerrainTileComponent());

      level.terrain.tiles[levelSuperstate.FindMarkerLocation("item")].components.Add(
        game.root.EffectItemTTCCreate(
          game.root.EffectBlastRodCreate().AsIItem())
        .AsITerrainTileComponent());

      var mordranthLocation = levelSuperstate.FindMarkerLocation("guard");
      var mordranth = Emberfolk.Make(game.root);
      mordranth.components.Add(
        game.root.EffectGuardAICapabilityUCCreate(mordranthLocation, 1).AsIUnitComponent());
      level.EnterUnit(levelSuperstate, mordranthLocation, level.time, mordranth);

      level.controller = game.root.EffectBridgesLevelControllerCreate(level).AsILevelController();

      var entryLoc = levelSuperstate.FindMarkerLocation("entry");

      levelSuperstate.Reconstruct(level);
      levelSuperstate.AddNoUnitZone(entryLoc, 3);

      EmberDeepUnitsAndItems.FillWithUnits(
        game.rand,
        level,
        levelSuperstate,
        (loc) => !loc.Equals(entryLoc),
        /*numIrkling=*/ 10 * numSpaces / 200,
        /*numDraxling=*/ 8 * numSpaces / 200,
        /*numRavagianTrask=*/ 3 * numSpaces / 200,
        /*numBaug=*/ 2 * numSpaces / 200,
        /*numSpirient=*/ 1 * numSpaces / 200,
        /*numIrklingKing=*/ 0 * numSpaces / 200,
        /*numEmberfolk=*/ 0 * numSpaces / 200,
        /*numChronolisk=*/ 0 * numSpaces / 200,
        /*numMantisBombardier=*/ 0 * numSpaces / 200,
        /*numLightningTrask=*/ 0 * numSpaces / 200);

      levelSuperstate.Reconstruct(level);

      EmberDeepUnitsAndItems.PlaceItems(game.rand, level, levelSuperstate, (loc) => !loc.Equals(entryLoc), .04f, 0f);

      game.levels.Add(level);

      entryLocation = entryLoc;
    }

    public static string GetName(this BridgesLevelController obj) {
      return "Bridges";
    }

    public static bool ConsiderCornersAdjacent(this BridgesLevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this BridgesLevelController obj,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        string triggerName) {
      game.root.logger.Info("Got simple trigger: " + triggerName);
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this BridgesLevelController obj,
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
-9 0 4 1 Magma
-9 0 6 1 Magma Fire
-9 0 7 1 Magma
-9 1 0 1 Magma
-9 1 2 1 Magma
-9 1 4 1 Magma
-9 1 7 1 Magma Fire
-8 -2 7 1 Magma
-8 -1 7 1 Magma
-8 0 4 1 Magma
-8 0 5 1 Magma
-8 0 6 1 Magma
-8 0 7 1 Magma
-8 1 0 4 Mud Cave Marker(exit)
-8 1 1 1 Magma Fire
-8 1 2 4 Mud
-8 1 3 1 Magma
-8 1 4 4 Mud Obsidian
-8 1 5 1 Magma
-8 1 6 1 Magma
-8 1 7 4 Mud
-8 2 0 1 Magma
-8 2 1 1 Magma
-8 2 2 1 Magma
-8 2 3 1 Magma
-8 2 4 1 Magma
-8 2 5 1 Magma Fire
-8 2 6 1 Magma
-8 2 7 1 Magma
-7 -2 4 1 Magma
-7 -2 5 1 Magma
-7 -2 6 1 Magma
-7 -2 7 1 Magma Fire
-7 -1 0 1 Magma
-7 -1 1 1 Magma Fire
-7 -1 2 1 Magma
-7 -1 3 1 Magma
-7 -1 4 1 Magma Fire
-7 -1 5 1 Magma
-7 -1 6 1 Magma Fire
-7 -1 7 6 Dirt
-7 0 0 1 Magma
-7 0 1 1 Magma Fire
-7 0 2 1 Magma
-7 0 3 1 Magma
-7 0 4 1 Magma
-7 0 5 1 Magma Fire
-7 0 6 1 Magma
-7 0 7 1 Magma
-7 1 0 4 Mud
-7 1 1 4 Mud Rocks
-7 1 2 4 Mud Obsidian
-7 1 3 4 Mud
-7 1 4 4 Mud Obsidian
-7 1 5 4 Mud
-7 1 6 4 Mud
-7 1 7 4 Mud
-7 2 0 4 Mud Obsidian
-7 2 1 1 Magma Fire
-7 2 2 5 Mud
-7 2 3 1 Magma Fire
-7 2 4 5 Mud Rocks
-7 2 5 5 Mud Obsidian
-7 2 6 5 Mud
-7 2 7 5 Mud
-7 3 0 1 Magma Fire
-7 3 1 1 Magma
-7 3 2 1 Magma
-7 3 4 1 Magma
-7 3 7 1 Magma Fire
-6 -2 2 1 Magma
-6 -2 4 1 Magma
-6 -2 5 1 Magma
-6 -2 6 2 Magma
-6 -2 7 2 Magma
-6 -1 0 6 Dirt
-6 -1 1 1 Magma
-6 -1 2 7 Dirt
-6 -1 3 6 Dirt Rocks
-6 -1 4 7 Dirt
-6 -1 5 6 Mud
-6 -1 6 6 Dirt Rocks
-6 -1 7 6 Dirt Rocks
-6 0 0 6 Mud
-6 0 1 1 Magma
-6 0 2 6 Dirt Rocks
-6 0 3 1 Magma
-6 0 4 6 Dirt
-6 0 5 1 Magma Fire
-6 0 6 1 Magma
-6 0 7 6 Mud Rocks
-6 1 0 2 Magma
-6 1 1 1 Magma
-6 1 2 1 Magma
-6 1 3 2 Magma Fire
-6 1 4 1 Magma
-6 1 5 2 Magma
-6 1 6 2 Magma
-6 1 7 1 Magma Fire
-6 2 0 2 Magma
-6 2 1 4 Mud Rocks
-6 2 2 2 Magma
-6 2 3 5 Dirt
-6 2 4 2 Magma
-6 2 5 5 Mud
-6 2 6 2 Magma
-6 2 7 2 Magma
-6 3 0 5 Mud Obsidian
-6 3 1 5 Dirt
-6 3 2 5 Mud Rocks
-6 3 3 5 Mud
-6 3 4 5 Mud
-6 3 5 1 Magma
-6 3 6 1 Magma Fire
-6 3 7 1 Magma
-6 4 0 1 Magma
-5 -2 2 1 Magma
-5 -2 3 1 Magma Fire
-5 -2 4 1 Magma
-5 -2 5 1 Magma Fire
-5 -2 6 7 Dirt
-5 -2 7 7 Dirt
-5 -1 0 7 Dirt Rocks
-5 -1 1 7 Dirt
-5 -1 2 7 Dirt
-5 -1 3 7 Dirt Rocks
-5 -1 4 7 Dirt
-5 -1 5 7 Mud
-5 -1 6 2 Magma
-5 -1 7 2 Magma Fire
-5 0 0 6 Dirt
-5 0 1 6 Dirt
-5 0 2 6 Mud
-5 0 3 6 Mud
-5 0 4 6 Dirt
-5 0 5 6 Mud Rocks
-5 0 6 5 Mud
-5 0 7 5 Mud
-5 1 0 6 Mud Obsidian
-5 1 1 2 Magma Fire
-5 1 2 1 Magma Fire
-5 1 6 1 Magma
-5 1 7 2 Magma
-5 2 0 2 Magma
-5 2 1 2 Magma
-5 2 2 4 Mud Obsidian
-5 2 3 2 Magma Fire
-5 2 4 4 Mud
-5 2 5 2 Magma
-5 2 6 4 Mud Dirt Obsidian
-5 2 7 4 Mud Marker(item)
-5 3 0 2 Magma
-5 3 1 2 Magma Fire
-5 3 2 2 Magma
-5 3 3 5 Mud
-5 3 4 5 Dirt
-5 3 5 7 Mud
-5 3 6 7 Mud Obsidian
-5 3 7 7 Mud
-5 4 0 1 Magma
-5 4 1 1 Magma
-5 4 2 1 Magma
-5 4 3 1 Magma
-5 4 4 1 Magma
-4 -2 2 1 Magma
-4 -2 3 1 Magma
-4 -2 4 2 Magma Fire
-4 -2 5 2 Magma
-4 -2 6 7 Dirt Rocks
-4 -2 7 2 Magma
-4 -1 0 7 Dirt
-4 -1 1 7 Dirt
-4 -1 2 7 Dirt
-4 -1 3 7 Dirt Rocks
-4 -1 4 1 Magma
-4 -1 5 2 Magma Fire
-4 -1 6 1 Magma
-4 -1 7 1 Magma
-4 0 0 1 Magma
-4 0 1 1 Magma
-4 0 2 1 Magma Fire
-4 0 3 1 Magma
-4 0 4 5 Mud
-4 0 5 5 Dirt
-4 0 6 5 Mud Obsidian
-4 0 7 4 Mud
-4 1 0 5 Mud
-4 1 1 5 Dirt Obsidian
-4 1 2 5 Mud
-4 1 3 2 Magma
-4 1 4 1 Magma
-4 1 5 1 Magma
-4 1 6 2 Magma
-4 1 7 1 Magma Fire
-4 2 0 2 Magma Fire
-4 2 1 2 Magma
-4 2 2 4 Mud
-4 2 3 4 Mud
-4 2 4 4 Mud Rocks
-4 2 5 4 Mud
-4 2 6 4 Mud Obsidian Marker(guard)
-4 2 7 4 Mud
-4 3 0 4 Mud
-4 3 1 4 Mud
-4 3 2 4 Mud Rocks
-4 3 3 2 Magma
-4 3 4 2 Magma
-4 3 5 2 Magma
-4 3 6 2 Magma
-4 3 7 2 Magma
-4 4 0 7 Mud Rocks
-4 4 1 7 Mud Dirt
-4 4 2 7 Mud
-4 4 3 1 Magma
-4 4 4 1 Magma
-4 4 5 1 Magma
-4 4 6 1 Magma
-4 4 7 1 Magma Fire
-3 -2 0 4 Magma
-3 -2 2 4 Magma
-3 -2 3 4 Magma
-3 -2 4 4 Magma
-3 -2 5 2 Magma
-3 -2 6 4 Magma
-3 -2 7 3 Magma
-3 -1 0 7 Dirt
-3 -1 1 7 Dirt
-3 -1 2 7 Dirt Rocks
-3 -1 3 7 Dirt
-3 -1 4 7 Dirt
-3 -1 5 1 Magma
-3 -1 6 1 Magma Fire
-3 -1 7 7 Dirt
-3 0 0 4 Mud Obsidian
-3 0 1 1 Magma
-3 0 2 4 Mud
-3 0 3 4 Mud
-3 0 4 4 Mud Rocks
-3 0 5 4 Mud
-3 0 6 4 Mud Rocks
-3 0 7 4 Mud Obsidian
-3 1 0 5 Dirt
-3 1 1 5 Mud Rocks
-3 1 2 1 Magma
-3 1 3 5 Mud
-3 1 4 1 Magma
-3 1 5 1 Magma Fire
-3 1 6 2 Magma
-3 1 7 1 Magma
-3 2 0 1 Magma
-3 2 1 4 Mud Obsidian
-3 2 2 4 Mud
-3 2 3 4 Mud Obsidian
-3 2 4 5 Mud Rocks
-3 2 5 4 Mud
-3 2 6 5 Mud
-3 2 7 5 Mud Rocks
-3 3 0 4 Mud
-3 3 1 4 Mud
-3 3 2 4 Mud Rocks
-3 3 3 4 Mud
-3 3 4 2 Magma
-3 3 5 2 Magma Fire
-3 3 6 3 Magma
-3 3 7 3 Magma Fire
-3 4 0 7 Dirt Rocks
-3 4 1 7 Mud
-3 4 2 7 Mud Dirt
-3 4 3 7 Mud Obsidian
-3 4 4 7 Mud
-3 4 5 1 Magma
-3 4 6 1 Magma
-3 4 7 1 Magma
-2 -2 0 4 Magma
-2 -2 1 4 Magma
-2 -2 2 3 Magma Fire
-2 -2 3 3 Magma
-2 -2 4 1 Magma
-2 -2 5 2 Magma
-2 -2 6 2 Magma
-2 -1 0 3 Magma Fire
-2 -1 1 4 Magma
-2 -1 2 4 Magma Fire
-2 -1 3 7 Dirt
-2 -1 4 7 Dirt
-2 -1 5 7 Dirt
-2 -1 6 7 Dirt
-2 -1 7 7 Dirt Rocks
-2 0 0 4 Mud Cave Marker(teleport1b)
-2 0 1 4 Mud Rocks
-2 0 2 4 Mud
-2 0 3 4 Mud
-2 0 4 4 Mud
-2 0 5 4 Mud
-2 0 6 2 Magma
-2 0 7 2 Magma Fire
-2 1 0 2 Magma
-2 1 1 1 Magma
-2 1 2 2 Magma Fire
-2 1 3 1 Magma
-2 1 4 7 Dirt Rocks
-2 1 5 7 Dirt Rocks
-2 1 6 7 Dirt
-2 1 7 7 Dirt
-2 2 0 7 Dirt
-2 2 1 1 Magma
-2 2 2 1 Magma Fire
-2 2 3 4 Mud
-2 2 4 3 Mud
-2 2 5 4 Mud
-2 2 6 4 Mud
-2 2 7 4 Mud Obsidian
-2 3 0 5 Mud
-2 3 1 5 Mud Obsidian
-2 3 2 5 Mud
-2 3 3 4 Mud Rocks
-2 3 4 2 Magma
-2 3 5 2 Magma
-2 3 6 2 Magma
-2 3 7 2 Magma
-2 4 0 7 Mud Rocks
-2 4 1 7 Dirt
-2 4 2 7 Dirt
-2 4 3 7 Mud
-2 4 4 7 Dirt
-2 4 5 7 Mud
-2 4 6 1 Magma Fire
-2 4 7 1 Magma Fire
-1 -2 1 4 Magma Fire
-1 -2 2 4 Magma
-1 -2 3 3 Magma
-1 -2 4 2 Magma Fire
-1 -2 7 1 Magma
-1 -1 0 1 Magma
-1 -1 1 2 Magma
-1 -1 2 2 Magma
-1 -1 3 4 Magma
-1 -1 4 3 Magma
-1 -1 5 3 Magma
-1 -1 6 4 Mud
-1 -1 7 4 Mud Rocks
-1 0 0 7 Dirt Rocks
-1 0 1 7 Dirt
-1 0 2 7 Dirt
-1 0 3 7 Dirt Rocks
-1 0 4 7 Dirt
-1 0 5 2 Magma
-1 0 6 7 Dirt
-1 0 7 7 Dirt Rocks
-1 1 0 7 Dirt
-1 1 1 2 Magma
-1 1 2 7 Dirt Rocks
-1 1 3 7 Dirt
-1 1 4 7 Dirt
-1 1 5 7 Dirt
-1 1 6 7 Dirt Rocks
-1 1 7 7 Dirt
-1 2 0 7 Dirt
-1 2 1 7 Dirt
-1 2 2 7 Dirt
-1 2 3 7 Dirt Rocks
-1 2 4 8 Dirt
-1 2 5 8 Dirt
-1 2 6 8 Dirt
-1 2 7 8 Dirt Rocks
-1 3 0 5 Mud Cave Marker(teleport2b)
-1 3 1 5 Mud Rocks
-1 3 2 4 Mud
-1 3 3 5 Mud Obsidian
-1 3 4 2 Magma Fire
-1 3 5 2 Magma
-1 3 6 2 Magma
-1 3 7 9 Dirt
-1 4 0 8 Dirt
-1 4 1 8 Dirt
-1 4 2 8 Mud Rocks
-1 4 3 7 Dirt
-1 4 4 1 Magma Fire
-1 4 5 1 Magma
-1 4 6 1 Magma
-1 4 7 1 Magma
0 -2 3 4 Magma
0 -2 4 4 Magma
0 -2 5 3 Magma
0 -2 6 3 Magma
0 -2 7 4 Magma
0 -1 0 3 Magma Fire
0 -1 1 1 Magma
0 -1 2 4 Magma
0 -1 3 2 Magma Fire
0 -1 4 3 Magma
0 -1 5 3 Magma
0 -1 6 4 Mud
0 -1 7 4 Mud
0 0 0 4 Mud Obsidian
0 0 1 4 Mud Cave Marker(teleport1a)
0 0 2 3 Magma
0 0 3 7 Dirt
0 0 4 8 Dirt
0 0 5 7 Dirt
0 0 6 7 Dirt
0 0 7 8 Dirt
0 1 0 7 Dirt
0 1 1 7 Dirt
0 1 2 7 Dirt Rocks
0 1 3 7 Dirt
0 1 4 4 Magma Fire
0 1 5 7 Dirt
0 1 6 4 Magma
0 1 7 3 Magma
0 2 0 7 Dirt
0 2 1 7 Dirt
0 2 2 7 Dirt Rocks
0 2 3 7 Dirt
0 2 4 3 Magma
0 2 5 4 Magma
0 2 6 3 Magma Fire
0 2 7 3 Magma
0 3 0 9 Dirt Rocks
0 3 1 8 Dirt
0 3 2 9 Dirt
0 3 3 9 Dirt
0 3 4 9 Dirt
0 3 5 9 Dirt Rocks
0 3 6 9 Dirt
0 3 7 9 Dirt
0 4 0 9 Dirt
0 4 1 8 Dirt
0 4 2 8 Dirt
0 4 3 8 Dirt
0 4 4 1 Magma
0 4 5 1 Magma
0 4 6 1 Magma Fire
0 4 7 1 Magma
1 -1 0 4 Magma Fire
1 -1 1 3 Magma
1 -1 2 3 Magma
1 -1 3 3 Magma
1 -1 4 4 Mud Obsidian
1 -1 5 4 Mud Rocks
1 -1 6 4 Mud
1 -1 7 4 Mud
1 0 0 4 Mud Rocks
1 0 1 4 Mud Rocks
1 0 2 3 Magma
1 0 3 3 Magma
1 0 4 4 Magma
1 0 5 4 Magma Fire
1 0 6 4 Magma
1 0 7 4 Magma
1 1 0 7 Dirt
1 1 1 7 Dirt Rocks
1 1 2 7 Dirt
1 1 3 7 Dirt
1 1 4 4 Magma
1 1 5 4 Magma
1 1 6 3 Magma
1 1 7 4 Magma
1 2 0 2 Magma Fire
1 2 1 2 Magma
1 2 2 1 Magma
1 2 3 1 Magma
1 2 4 1 Magma Fire
1 2 5 2 Magma
1 2 6 4 Magma Fire
1 2 7 7 Dirt
1 3 0 5 Mud
1 3 1 5 Mud
1 3 2 5 Dirt
1 3 3 6 Mud Rocks
1 3 4 6 Mud
1 3 5 5 Dirt Cave Marker(teleport2a)
1 3 6 1 Magma
1 3 7 1 Magma
1 4 0 1 Magma
1 4 1 1 Magma
1 4 2 1 Magma
1 4 3 1 Magma Fire
1 4 4 1 Magma Fire
1 4 5 1 Magma
2 -1 4 4 Mud Marker(entry)
2 -1 5 4 Mud
2 -1 6 4 Mud Rocks
2 -1 7 4 Mud
2 0 0 3 Magma
2 0 1 3 Magma
2 0 2 2 Magma
2 0 3 2 Magma
2 0 4 1 Magma Fire
2 0 5 2 Magma
2 0 6 3 Magma
2 1 0 4 Magma
2 1 1 7 Dirt
2 1 2 7 Dirt Rocks
2 1 3 7 Dirt
2 1 4 7 Dirt
2 1 5 7 Dirt Rocks
2 1 6 7 Dirt Rocks
2 1 7 7 Dirt
2 2 0 7 Dirt
2 2 1 3 Magma
2 2 2 7 Dirt
2 2 3 6 Mud
2 2 4 7 Dirt
2 2 5 6 Mud
2 2 6 7 Dirt Rocks
2 2 7 7 Dirt
2 3 0 7 Dirt
2 3 1 6 Mud
2 3 2 5 Dirt
2 3 3 6 Dirt Rocks
2 3 4 6 Dirt
2 3 5 6 Mud Obsidian
2 3 6 1 Magma
2 3 7 1 Magma
2 4 0 1 Magma
2 4 1 1 Magma Fire
3 0 1 3 Magma Fire
3 0 2 4 Magma
3 0 3 1 Magma
3 0 4 2 Magma
3 0 7 3 Magma
3 1 0 2 Magma
3 1 1 4 Magma
3 1 2 4 Magma Fire
3 1 3 4 Magma Fire
3 1 4 4 Magma Fire
3 1 5 7 Dirt
3 1 6 4 Magma
3 1 7 4 Magma
3 2 0 6 Dirt Rocks
3 2 1 7 Dirt
3 2 2 7 Dirt
3 2 3 7 Dirt
3 2 4 7 Dirt Rocks
3 2 5 6 Mud Rocks
3 2 6 6 Dirt
3 2 7 3 Magma Fire
3 3 0 3 Magma
3 3 1 3 Magma
3 3 2 1 Magma Fire
3 3 3 2 Magma
3 3 4 1 Magma
3 3 5 1 Magma
3 3 6 1 Magma
3 4 0 1 Magma Fire
3 4 1 1 Magma
4 0 5 4 Magma
4 0 6 4 Magma Fire
4 1 0 4 Magma Fire
4 1 1 3 Magma
4 1 2 4 Magma
4 1 3 4 Magma
4 1 4 1 Magma
4 1 5 4 Magma
4 1 6 1 Magma
4 2 0 1 Magma
4 2 1 4 Magma
4 2 2 1 Magma Fire
4 2 3 4 Magma
4 2 4 1 Magma
4 2 5 3 Magma
4 2 6 1 Magma
4 3 0 2 Magma
4 3 1 2 Magma
4 3 2 1 Magma
4 3 3 1 Magma Fire
";
  }
}
