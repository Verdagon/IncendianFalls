using System;
using IncendianFalls;

namespace Atharia.Model {
  public static class SquareCaveLevelControllerExtensions {
    public static void MakeLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        SSContext context,
        Game game,
        Superstate superstate,
        int depth) {
      var terrain =
          DungeonTerrainGenerator.Generate(
              context,
              80,
              20,
              game.rand);

      var units = context.root.EffectUnitMutSetCreate();

      level = context.root.EffectLevelCreate(terrain, units, NullILevelController.Null);
      levelSuperstate = new LevelSuperstate(level);

      var controller =
          context.root.EffectSquareCaveLevelControllerCreate(
              level, depth);
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

    public static string GetName(this SquareCaveLevelController obj) {
      return "Cave" + obj.depth;
    }

    public static bool ConsiderCornersAdjacent(this SquareCaveLevelController obj) {
      return true;
    }

    public static Location GetEntryLocation(
        this SquareCaveLevelController obj,
        Game game,
        Superstate superstate,
        int entranceIndex) {
      game.root.logger.Error("Replace this");
      return superstate.levelSuperstate.GetRandomWalkableLocation(game.rand, true);
    }

    public static Atharia.Model.Void Generate(
        this SquareCaveLevelController cliffLevelController,
        Game game) {
      return new Atharia.Model.Void();
    }
  }
}
