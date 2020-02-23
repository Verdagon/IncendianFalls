using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class PreGauntletLevelControllerExtensions {
    public static void MakeLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        SSContext context,
        Game game,
        Superstate superstate,
        int depth,
        int levelIndex) {
      var terrain =
          CircleTerrainGenerator.Generate(context, PentagonPattern9.makePentagon9Pattern(), game.rand, 4.0f);

      var units = context.root.EffectUnitMutSetCreate();

      level =
          context.root.EffectLevelCreate(
              terrain, units, depth, NullILevelController.Null, game.time);
      levelSuperstate = new LevelSuperstate(level);

      game.levels.Add(level);

      var controller = context.root.EffectPreGauntletLevelControllerCreate(level);
      level.controller = controller.AsILevelController();

      GenerationCommon.PlaceStaircase(terrain, new Location(0, 0, 0), true, 0, Level.Null, 0);

      GenerationCommon.PlaceItems(
          context, game.rand, level, levelSuperstate, levelIndex, new Location(0, 0, 0), .1f, .1f);
    }

    public static string GetName(this PreGauntletLevelController obj) {
      return "PreGauntlet Lair";
    }

    public static bool ConsiderCornersAdjacent(this PreGauntletLevelController obj) {
      return false;
    }

    public static Location GetEntryLocation(
        this PreGauntletLevelController obj,
        Game game,
        LevelSuperstate levelSuperstate,
        Level fromLevel, int fromLevelPortalIndex) {
      var level = obj.level;
      if (fromLevel.NullableIs(level)) {
        return levelSuperstate.GetNRandomWalkableLocations(
            obj.level.terrain, game.rand, 1, true, true)[0];
      } else {
        return new Location(0, 0, 0);
      }
    }

    public static Atharia.Model.Void Generate(
        this PreGauntletLevelController cliffLevelController,
        Game game) {
      return new Atharia.Model.Void();
    }
  }
}
