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
        int depth,
        int levelIndex) {
      var terrain = CircleTerrainGenerator.Generate(context, PentagonPattern9.makePentagon9Pattern(), game.rand, 8.0f);

      var units = context.root.EffectUnitMutSetCreate();

      level =
          context.root.EffectLevelCreate(
              terrain, units, depth, NullILevelController.Null, game.time);
      levelSuperstate = new LevelSuperstate(level);

      game.levels.Add(level);

      GenerationCommon.PlaceRocks(context, game.rand, level, levelSuperstate);
      GenerationCommon.PlaceItems(
          context, game.rand, level, levelSuperstate, levelIndex, new Location(0, 0, 0), .03f, .02f);
      //GenerationCommon.PlaceStaircases(context, game.rand, level, levelSuperstate);

      var controller = context.root.EffectRavashrikeLevelControllerCreate(level);
      level.controller = controller.AsILevelController();

      var enemyLocation =
          levelSuperstate.GetNRandomWalkableLocations(
              level.terrain, game.rand, 1, true, true)[0];

      var components = IUnitComponentMutBunch.New(context.root);
      components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(context.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(context.root.EffectBideAICapabilityUCCreate(0).AsIUnitComponent());
      Unit enemy =
          context.root.EffectUnitCreate(
              context.root.EffectIUnitEventMutListCreate(),
              true,
              0,
              enemyLocation,
              "Ravashrike",
              600, 600,
              100, 100,
              250,
              game.time + 10,
              components,
              false,
              14);
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
      return "Ravashrike Lair";
    }

    public static bool ConsiderCornersAdjacent(this RavashrikeLevelController obj) {
      return false;
    }

    public static Location GetEntryLocation(
        this RavashrikeLevelController obj,
        Game game,
        LevelSuperstate levelSuperstate,
        Level fromLevel, int fromLevelPortalIndex) {
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
