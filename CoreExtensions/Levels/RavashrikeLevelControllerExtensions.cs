using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class RavashrikeLevelControllerExtensions {
    public static void MakeLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        SSContext context,
        Game game,
        Superstate superstate,
        int depth) {
      var terrain = CircleTerrainGenerator.Generate(context, game.rand);

      var units = context.root.EffectUnitMutSetCreate();

      level =
          context.root.EffectLevelCreate(
              terrain, units, depth, NullILevelController.Null);
      levelSuperstate = new LevelSuperstate(level);

      GenerationCommon.PlaceRocks(context, game.rand, level, levelSuperstate);
      GenerationCommon.PlaceItems(context, game.rand, level, levelSuperstate);
      //GenerationCommon.PlaceStaircases(context, game.rand, level, levelSuperstate);

      var controller = context.root.EffectRavashrikeLevelControllerCreate(level);
      level.controller = controller.AsILevelController();

      var enemyLocation =
          levelSuperstate.GetNRandomWalkableLocations(
              level.terrain, game.rand, 1, true, true)[0];

      var components = IUnitComponentMutBunch.New(context.root);
      components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(context.root.EffectAttackAICapabilityUCCreate().AsIUnitComponent());
      components.Add(context.root.EffectBideAICapabilityUCCreate().AsIUnitComponent());
      Unit enemy =
          context.root.EffectUnitCreate(
              context.root.EffectIUnitEventMutListCreate(),
              true,
              0,
              enemyLocation,
              "Ravashrike",
              300, 300,
              100, 100,
              600,
              game.time + 10,
              components,
              false,
              27);
      level.EnterUnit(game, levelSuperstate, enemy, Level.Null, 0);
    }

    //  Level level;
    //  if (squareLevelsOnly || rand.Next() % 2 == 0) {
    //    level = MakeSquareLevel(context, rand, currentTime, name);
    //  } else {
    //    level = MakePentagonalLevel(context, rand, currentTime, name);
    //  }

    //  var walkableLocations = new WalkableLocations(level.terrain, level.units);

    //  return level;
    //}

    public static string GetName(this RavashrikeLevelController obj) {
      return "Bridge";
    }

    public static bool ConsiderCornersAdjacent(this RavashrikeLevelController obj) {
      return true;
    }

    public static Location GetEntryLocation(
        this RavashrikeLevelController obj,
        Game game,
        LevelSuperstate levelSuperstate,
        Level fromLevel, int fromLevelPortalIndex) {
      foreach (var locationAndTile in obj.level.terrain.tiles) {
        var staircase = locationAndTile.Value.components.GetOnlyStaircaseTTCOrNull();
        if (staircase.Exists()) {
          if (staircase.destinationLevel.NullableIs(fromLevel) &&
              staircase.destinationLevelPortalIndex == fromLevelPortalIndex) {
            return locationAndTile.Key;
          }
        }
      }
      if (!fromLevel.NullableIs(obj.level)) {
        game.root.logger.Error("Couldnt figure out where to place unit!");
      }
      return levelSuperstate.GetNRandomWalkableLocations(
          obj.level.terrain, game.rand, 1, true, true)[0];
    }

    public static Atharia.Model.Void Generate(
        this RavashrikeLevelController cliffLevelController,
        Game game) {
      return new Atharia.Model.Void();
    }
  }
}
