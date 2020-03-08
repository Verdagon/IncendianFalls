using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class DirtRoadLevelControllerExtensions {
    public static void LoadLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocationRet,
        out Location exitLocationRet,
        Game game) {
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

      var entryLocation = levelSuperstate.FindMarkerLocation("entry");
      var exitLocation = levelSuperstate.FindMarkerLocation("exit");
      level.terrain.tiles[exitLocation].components.Add(game.root.EffectDownStairsTTCCreate().AsITerrainTileComponent());

      level.EnterUnit(
        levelSuperstate,
        levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => !loc.Equals(entryLocation), true, true)[0],
        level.time,
        Irkling.Make(game.root));
      level.EnterUnit(
        levelSuperstate,
        levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => !loc.Equals(entryLocation), true, true)[0],
        level.time,
        Baug.Make(game.root));
      level.EnterUnit(
        levelSuperstate,
        levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => !loc.Equals(entryLocation), true, true)[0],
        level.time,
        Chronolisk.Make(game.root));
      level.EnterUnit(
        levelSuperstate,
        levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => !loc.Equals(entryLocation), true, true)[0],
        level.time,
        Draxling.Make(game.root));
      level.EnterUnit(
        levelSuperstate,
        levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => !loc.Equals(entryLocation), true, true)[0],
        level.time,
        Emberfolk.Make(game.root));
      level.EnterUnit(
        levelSuperstate,
        levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => !loc.Equals(entryLocation), true, true)[0],
        level.time,
        IrklingKing.Make(game.root));
      level.EnterUnit(
        levelSuperstate,
        levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => !loc.Equals(entryLocation), true, true)[0],
        level.time,
        LightningTrask.Make(game.root));
      level.EnterUnit(
        levelSuperstate,
        levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => !loc.Equals(entryLocation), true, true)[0],
        level.time,
        MantisBombardier.Make(game.root));
      level.EnterUnit(
        levelSuperstate,
        levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => !loc.Equals(entryLocation), true, true)[0],
        level.time,
        RavagianTrask.Make(game.root));
      level.EnterUnit(
        levelSuperstate,
        levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => !loc.Equals(entryLocation), true, true)[0],
        level.time,
        Spirient.Make(game.root));

      //level.EnterUnit(levelSuperstate, levelSuperstate.FindMarkerLocation("enemy"), level.time, LightningTrask.Make(game.root));

      level.controller = game.root.EffectDirtRoadLevelControllerCreate(level).AsILevelController();

      game.levels.Add(level);
      entryLocationRet = entryLocation;
      exitLocationRet = exitLocation;
    }

    public static string GetName(this DirtRoadLevelController obj) {
      return "DirtRoad";
    }

    public static bool ConsiderCornersAdjacent(this DirtRoadLevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this DirtRoadLevelController obj,
        Game game,
        Superstate superstate,
        string triggerName) {
      game.root.logger.Info("Got simple trigger: " + triggerName);

      if (triggerName == "levelStart") {
        game.player.components.Add(
          game.root.EffectBlastRodCreate().AsIUnitComponent());
      }

      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this DirtRoadLevelController obj,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      game.root.logger.Info("Got simple unit trigger: " + triggerName);
      return new Atharia.Model.Void();
    }

    private static string LEVEL = @"
-5 -3 4 1 Grass
-5 -3 6 1 Grass Tree
-5 -3 7 1 Grass Rocks
-4 -3 4 1 Mud
-4 -3 5 1 Grass
-4 -3 6 1 Grass Rocks
-4 -3 7 1 Mud
-4 -2 0 1 Grass
-4 -2 1 1 Grass
-4 -2 2 2 Grass
-4 -2 4 2 Grass
-4 -2 7 3 Grass
-3 -3 4 1 Grass Tree
-3 -3 5 1 Mud
-3 -3 6 1 Mud Marker(entry)
-3 -3 7 1 Grass
-3 -2 0 1 Mud
-3 -2 1 1 Grass Tree
-3 -2 2 1 Mud
-3 -2 3 1 Grass Tree
-3 -2 4 1 Grass
-3 -2 5 1 Grass
-3 -2 6 2 Grass
-3 -2 7 1 Grass
-3 -1 0 1 Grass
-2 -3 4 2 Grass
-2 -3 5 2 Grass
-2 -3 6 2 Grass
-2 -3 7 1 Grass
-2 -2 0 1 Grass Tree
-2 -2 1 1 Mud Rocks
-2 -2 2 1 Mud
-2 -2 3 1 Mud
-2 -2 4 1 Mud Rocks
-2 -2 5 2 Grass Rocks
-2 -2 6 2 Grass Tree
-2 -2 7 2 Grass Rocks
-2 -1 0 1 Grass
-2 -1 1 1 Grass
-2 -1 2 1 Grass Tree
-2 -1 4 1 Grass Tree
-2 -1 6 2 Grass
-2 -1 7 1 Grass
-2 0 0 1 Grass
-2 0 2 1 Grass Tree
-1 -3 4 1 Grass
-1 -3 5 1 Grass Tree
-1 -3 6 1 Mud
-1 -3 7 1 Mud
-1 -2 0 1 Mud Rocks
-1 -2 1 2 Grass Tree
-1 -2 2 1 Mud
-1 -2 3 1 Mud
-1 -2 4 1 Mud
-1 -2 5 1 Mud
-1 -2 6 1 Mud
-1 -2 7 1 Mud
-1 -1 0 1 Grass
-1 -1 1 1 Grass
-1 -1 2 1 Grass
-1 -1 3 1 Grass
-1 -1 4 2 Grass Rocks
-1 -1 5 2 Grass
-1 -1 6 3 Grass
-1 -1 7 2 Grass Tree
-1 0 0 4 Grass
-1 0 1 2 Grass Rocks
-1 0 2 2 Grass
-1 0 3 1 Grass
-1 0 4 2 Grass Rocks
-1 0 5 1 Grass Tree
-1 0 7 2 Grass
0 -2 0 1 Mud
0 -2 1 1 Mud
0 -2 2 2 Grass
0 -2 3 1 Mud
0 -2 4 1 Grass Rocks
0 -2 5 1 Mud
0 -2 6 1 Grass Tree
0 -2 7 1 Grass
0 -1 0 1 Mud Rocks
0 -1 1 1 Mud
0 -1 2 1 Mud
0 -1 3 1 Mud
0 -1 4 1 Mud
0 -1 5 1 Mud
0 -1 6 1 Mud
0 -1 7 1 Mud
0 0 0 1 Grass
0 0 1 2 Grass
0 0 2 1 Grass Tree
0 0 3 1 Grass
0 0 4 1 Grass Tree
0 0 5 2 Grass
0 0 6 2 Grass
0 0 7 1 Grass
1 -2 3 1 Grass
1 -2 5 1 Grass Tree
1 -2 6 1 Grass
1 -1 0 2 Grass
1 -1 1 1 Grass
1 -1 2 2 Grass Tree
1 -1 3 1 Grass Rocks
1 -1 4 1 Grass
1 -1 5 1 Grass Rocks
1 -1 6 1 Grass
1 -1 7 2 Grass Tree
1 0 0 1 Mud
1 0 1 1 Mud Rocks
1 0 2 1 Mud Marker(enemy)
1 0 3 1 Mud Marker(exit)
1 0 4 1 Mud
1 0 5 1 Mud
1 0 6 1 Mud Rocks
1 0 7 1 Mud
2 -1 1 2 Grass Rocks
2 -1 3 1 Grass
2 -1 4 1 Grass
2 -1 5 2 Grass
2 -1 6 2 Grass
2 -1 7 1 Grass
2 0 0 2 Grass Rocks
2 0 1 1 Grass
2 0 2 1 Grass
2 0 3 1 Grass Tree
2 0 4 2 Grass
2 0 5 1 Grass
2 0 6 1 Grass
2 0 7 2 Grass
3 -1 5 1 Grass Rocks
3 -1 6 1 Grass
3 0 0 1 Grass Tree
3 0 1 2 Grass Tree
3 0 2 1 Grass
3 0 3 1 Grass
3 0 4 1 Grass
3 0 5 2 Grass
3 0 6 2 Grass Tree
3 0 7 1 Grass Rocks
3 1 0 1 Grass
3 1 1 1 Grass Rocks
4 0 1 1 Grass
4 0 3 1 Grass
4 0 5 1 Grass
4 0 6 1 Grass
4 1 0 1 Grass
4 1 1 1 Grass
";
  }
}
