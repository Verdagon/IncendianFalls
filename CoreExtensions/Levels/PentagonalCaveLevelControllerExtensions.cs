using System;
using IncendianFalls;

namespace Atharia.Model {
  public static class PentagonalCaveLevelControllerExtensions {
    public static void MakeLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        SSContext context,
        Game game,
        Superstate superstate,
        int depth) {
      var terrain =
          ForestTerrainGenerator.Generate(
              context,
              game.rand,
              PentagonPattern9.makePentagon9Pattern(),
              1000);

      var units = context.root.EffectUnitMutSetCreate();

      level = context.root.EffectLevelCreate(terrain, units, NullILevelController.Null);
      levelSuperstate = new LevelSuperstate(level);

      var controller =
          context.root.EffectPentagonalCaveLevelControllerCreate(
              level, depth);
      level.controller = controller.AsILevelController();
    }

    public static string GetName(this PentagonalCaveLevelController obj) {
      return "Cave" + obj.depth;
    }

    public static bool ConsiderCornersAdjacent(this PentagonalCaveLevelController obj) {
      return false;
    }

    public static Location GetEntryLocation(
        this PentagonalCaveLevelController obj,
        Game game,
        Superstate superstate,
        int entranceIndex) {
      game.root.logger.Error("Replace this");
      return superstate.levelSuperstate.GetRandomWalkableLocation(game.rand, true);
    }

    public static Atharia.Model.Void Generate(
        this PentagonalCaveLevelController cliffLevelController,
        Game game) {
      return new Atharia.Model.Void();
    }
  }
}
