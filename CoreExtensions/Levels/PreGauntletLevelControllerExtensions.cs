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
          CircleTerrainGenerator.Generate(context, game.rand, 4.0f);

      var units = context.root.EffectUnitMutSetCreate();

      level =
          context.root.EffectLevelCreate(
              terrain, units, depth, NullILevelController.Null, game.time);
      levelSuperstate = new LevelSuperstate(level);

      game.levels.Add(level);

      GenerationCommon.PlaceRocks(context, game.rand, level, levelSuperstate);
      GenerationCommon.PlaceItems(context, game.rand, level, levelSuperstate, levelIndex, new Location(0, 0, 0));

      var controller = context.root.EffectPreGauntletLevelControllerCreate(level);
      level.controller = controller.AsILevelController();

      GenerationCommon.PlaceStaircase(terrain, new Location(0, 0, 0), true, 0, Level.Null, 0);
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
      return levelSuperstate.GetNRandomWalkableLocations(
          obj.level.terrain, game.rand, 1, true, true)[0];
    }

    public static Atharia.Model.Void Generate(
        this PreGauntletLevelController cliffLevelController,
        Game game) {
      return new Atharia.Model.Void();
    }
  }
}
