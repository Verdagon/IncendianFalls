using System;
using IncendianFalls;

namespace Atharia.Model {
  public static class CliffLevelControllerExtensions {
    public static void MakeLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        SSContext context,
        Game game,
        Superstate superstate,
        int depth) {
      var terrain =
          CliffTerrainGenerator.Generate(
              context,
              game.rand,
              PentagonPattern9.makePentagon9Pattern(),
              1000);

      var units = context.root.EffectUnitMutSetCreate();

      level = context.root.EffectLevelCreate(terrain, units, NullILevelController.Null);
      levelSuperstate = new LevelSuperstate(level);

      var controller =
          context.root.EffectCliffLevelControllerCreate(
              level, depth);
      level.controller = controller.AsILevelController();
    }

    public static string GetName(this CliffLevelController obj) {
      return "Cliff" + obj.depth;
    }

    public static bool ConsiderCornersAdjacent(this CliffLevelController obj) {
      return false;
    }

    public static Location GetEntryLocation(
        this CliffLevelController obj,
        Game game,
        Superstate superstate,
        int entranceIndex) {
      game.root.logger.Error("Replace this");
      return superstate.levelSuperstate.GetRandomWalkableLocation(game.rand, true);
    }

    public static Atharia.Model.Void Generate(
        this CliffLevelController cliffLevelController,
        Game game) {
      return new Atharia.Model.Void();
    }
  }
}
