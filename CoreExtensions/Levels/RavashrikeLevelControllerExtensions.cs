using System;
using IncendianFalls;

namespace Atharia.Model {
  public static class RavashrikeLevelControllerExtensions {
    public static void MakeLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        SSContext context,
        Game game,
        Superstate superstate) {
      var terrain =
          ForestTerrainGenerator.Generate(
              context,
              game.rand,
              PentagonPattern9.makePentagon9Pattern(),
              400);

      var units = context.root.EffectUnitMutSetCreate();

      level = context.root.EffectLevelCreate(terrain, units, NullILevelController.Null);
      levelSuperstate = new LevelSuperstate(level);

      GenerationCommon.PlaceRocks(context, game.rand, level, levelSuperstate);
      GenerationCommon.PlaceItems(context, game.rand, level, levelSuperstate);
      GenerationCommon.PlaceStaircases(context, game.rand, level, levelSuperstate);

      var controller = context.root.EffectRavashrikeLevelControllerCreate(level);
      level.controller = controller.AsILevelController();

      var enemyLocation = levelSuperstate.GetRandomWalkableLocation(game.rand, true);

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
              IItemMutBunch.New(context.root),
              false);
      level.EnterUnit(game, levelSuperstate, enemy);
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
        Superstate superstate,
        int entranceIndex) {
      game.root.logger.Error("Replace this");
      return superstate.levelSuperstate.GetRandomWalkableLocation(game.rand, true);
    }

    public static Atharia.Model.Void Generate(
        this RavashrikeLevelController cliffLevelController,
        Game game) {
      return new Atharia.Model.Void();
    }
  }
}
