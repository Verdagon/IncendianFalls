using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class RavashrikeLevelControllerExtensions {
    public static void MakeLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocation,
        Game game,
        Superstate superstate,
        int depth,
        int levelIndex) {
      var terrain = CircleTerrainGenerator.Generate(game.root, PentagonPattern9.makePentagon9Pattern(), game.rand, 8.0f);

      var units = game.root.EffectUnitMutSetCreate();

      level =
          game.root.EffectLevelCreate(
          new Vec3(0, -8, 16),
              terrain, units, NullILevelController.Null, game.time);
      levelSuperstate = new LevelSuperstate(level);

      game.levels.Add(level);

      GenerationCommon.PlaceRocks(game.rand, level, levelSuperstate);
      GenerationCommon.PlaceItems(
          game.rand, level, levelSuperstate, levelIndex, new Location(0, 0, 0), .03f, .02f);
      //GenerationCommon.PlaceStaircases(context, game.rand, level, levelSuperstate);

      var controller = game.root.EffectRavashrikeLevelControllerCreate(level);
      level.controller = controller.AsILevelController();

      var enemyLocation =
          levelSuperstate.GetNRandomWalkableLocations(
              level.terrain, game.rand, 1, true, true)[0];

      var components = IUnitComponentMutBunch.New(game.root);
      components.Add(game.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(game.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(game.root.EffectBideAICapabilityUCCreate(0).AsIUnitComponent());
      Unit enemy =
          game.root.EffectUnitCreate(
              game.root.EffectIUnitEventMutListCreate(),
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
      level.EnterUnit(game, levelSuperstate, enemy, enemyLocation);

      entryLocation = new Location(0, 0, 0);
    }

    public static string GetName(this RavashrikeLevelController obj) {
      return "Ravashrike Lair";
    }

    public static bool ConsiderCornersAdjacent(this RavashrikeLevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this RavashrikeLevelController obj,
        Game game,
        Superstate superstate,
        string triggerName) {
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this RavashrikeLevelController obj,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      return new Atharia.Model.Void();
    }
  }
}
