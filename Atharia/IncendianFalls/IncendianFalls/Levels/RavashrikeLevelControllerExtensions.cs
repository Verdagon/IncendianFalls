using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class RavashrikeLevelControllerExtensions {
    public static Atharia.Model.Void Destruct(this RavashrikeLevelController self) {
      self.Delete();
      return new Atharia.Model.Void();
    }

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

      IncendianFallsUnitsAndItems.PlaceRocks(game.rand, level, levelSuperstate);
      IncendianFallsUnitsAndItems.PlaceItems(
          game.rand, level, levelSuperstate, levelIndex, new Location(0, 0, 0), .03f, .02f);
      //GenerationCommon.PlaceStaircases(context, game.rand, level, levelSuperstate);

      var controller = game.root.EffectRavashrikeLevelControllerCreate(level);
      level.controller = controller.AsILevelController();

      var enemyLocation =
          levelSuperstate.GetNRandomWalkableLocations(
              level.terrain, game.rand, 1, (loc) => true, true, true)[0];

      level.EnterUnit(
        levelSuperstate,
        enemyLocation,
        level.time,
        Ravashrike.Make(game.root));

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
