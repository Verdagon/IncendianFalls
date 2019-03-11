using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class GauntletLevelControllerExtensions {
    public static void MakeLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        SSContext context,
        Game game,
        Superstate superstate,
        Level fromLevel,
        int depth,
        int levelIndex) {
      var terrain =
          CircleTerrainGenerator.Generate(context, game.rand, 8.0f);

      var units = context.root.EffectUnitMutSetCreate();

      level =
          context.root.EffectLevelCreate(
              terrain, units, depth, NullILevelController.Null, game.time);
      levelSuperstate = new LevelSuperstate(level);

      game.levels.Add(level);

      GenerationCommon.PlaceRocks(context, game.rand, level, levelSuperstate);
      GenerationCommon.PlaceItems(context, game.rand, level, levelSuperstate, levelIndex, new Location(0, 0, 0));
      //GenerationCommon.PlaceStaircases(context, game.rand, level, levelSuperstate);

      var controller = context.root.EffectGauntletLevelControllerCreate(level);
      level.controller = controller.AsILevelController();

      GenerationCommon.PlaceStaircase(terrain, new Location(0, 0, 0), false, 0, fromLevel, 0);

      for (int i = 0; i < 5; i++) {
        GenerationCommon.PlaceAvelisk(context, game, level, levelSuperstate, level, 0);
      }
      for (int i = 0; i < 8; i++) {
        GenerationCommon.PlaceNovafaire(context, game, level, levelSuperstate, level, 0);
      }
      for (int i = 0; i < 4; i++) {
        GenerationCommon.PlaceDraxling(context, game, level, levelSuperstate, level, 0);
      }
      for (int i = 0; i < 3; i++) {
        GenerationCommon.PlaceLornix(context, game, level, levelSuperstate, level, 0);
      }
      for (int i = 0; i < 3; i++) {
        GenerationCommon.PlaceYoten(context, game, level, levelSuperstate, level, 0);
      }
      for (int i = 0; i < 3; i++) {
        GenerationCommon.PlaceSpiriad(context, game, level, levelSuperstate, level, 0);
      }
      for (int i = 0; i < 4; i++) {
        GenerationCommon.PlaceMordranth(context, game, level, levelSuperstate, level, 0);
      }
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

    public static string GetName(this GauntletLevelController obj) {
      return "Gauntlet Lair";
    }

    public static bool ConsiderCornersAdjacent(this GauntletLevelController obj) {
      return false;
    }

    public static Location GetEntryLocation(
        this GauntletLevelController obj,
        Game game,
        LevelSuperstate levelSuperstate,
        Level fromLevel, int fromLevelPortalIndex) {
      return levelSuperstate.GetNRandomWalkableLocations(
          obj.level.terrain, game.rand, 1, true, true)[0];
    }

    public static Atharia.Model.Void Generate(
        this GauntletLevelController cliffLevelController,
        Game game) {
      return new Atharia.Model.Void();
    }
  }
}
