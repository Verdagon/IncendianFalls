using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class CliffLevelControllerExtensions {
    public static void MakeLevel(
        out Level cliffLevel,
        out LevelSuperstate levelSuperstate,
        SSContext context,
        Game game,
        Superstate superstate,
        Level levelAbove,
        int depth) {

      bool waterfallTopLeftToBottomRight = game.rand.Next() % 2 == 0;
      CliffTerrainGenerator.GenerateWithWaterfall(
          out Terrain terrain,
          out List<CliffTerrainGenerator.CliffHalf> cliffHalves,
          context,
          game.rand,
          PentagonPattern9.makePentagon9Pattern(),
          //1000,
          300,
          waterfallTopLeftToBottomRight);

      var units = context.root.EffectUnitMutSetCreate();

      cliffLevel =
          context.root.EffectLevelCreate(
              terrain, units, depth, NullILevelController.Null);
      levelSuperstate = new LevelSuperstate(cliffLevel);


      SquareCaveLevelControllerExtensions.MakeLevel(
          out Level caveLevel,
          out LevelSuperstate caveLevelSuperstate,
          context,
          game,
          superstate,
          cliffLevel,
          2,
          cliffLevel,
          3,
          depth);

      Location highestLocationInHighHalf =
          GenerationCommon.GetFurthestLocationInDirection(
              terrain.pattern,
              cliffHalves[0].walkableLocs,
              new Vec2(0, 1));
      GenerationCommon.PlaceStaircase(
          terrain, highestLocationInHighHalf, false, 0, levelAbove, 1);

      Location lowestLocationInLowHalf =
          GenerationCommon.GetFurthestLocationInDirection(
              terrain.pattern,
              cliffHalves[1].walkableLocs,
              new Vec2(0, -1));
      GenerationCommon.PlaceStaircase(
          terrain, lowestLocationInLowHalf, true, 1, Level.Null, 0);

      PlaceCave(terrain, game.rand, cliffHalves[0].rooms, 2, caveLevel, 0);

      PlaceCave(terrain, game.rand, cliffHalves[1].rooms, 3, caveLevel, 1);

      var controller =
          context.root.EffectCliffLevelControllerCreate(
              cliffLevel, depth);
      cliffLevel.controller = controller.AsILevelController();
    }

    private static void PlaceCave(
        Terrain terrain,
        Rand rand,
        SortedDictionary<int, Room> rooms,
        int portalIndex,
        Level destinationLevel,
        int destinationLevelPortalIndex) {
      var randomLowHalfRoomNumber =
          SetUtils.GetRandom(rand.Next(), new SortedSet<int>(rooms.Keys));
      var randomLowHalfRoom = rooms[randomLowHalfRoomNumber];
      var highestSpaceInLowHalfRoom =
          GenerationCommon.GetFurthestLocationInDirection(
              terrain.pattern, randomLowHalfRoom.floors, new Vec2(0, 1));
      var lowHalfCaveTile = terrain.tiles[highestSpaceInLowHalfRoom];
      lowHalfCaveTile.components.Add(
          new StaircaseTTCAsITerrainTileComponent(
              terrain.root.EffectStaircaseTTCCreate(
                  portalIndex, destinationLevel, destinationLevelPortalIndex)));
      lowHalfCaveTile.components.Add(
          new DecorativeTTCAsITerrainTileComponent(
              terrain.root.EffectDecorativeTTCCreate("cave")));
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
        LevelSuperstate levelSuperstate,
        Level fromLevel, int fromLevelPortalIndex) {
      foreach (var locationAndTile in obj.level.terrain.tiles) {
        var staircase = locationAndTile.Value.components.GetOnlyStaircaseTTCOrNull();
        if (staircase.Exists()) {
          if (staircase.destinationLevel.Exists() &&
              staircase.destinationLevel.NullableIs(fromLevel) &&
              staircase.destinationLevelPortalIndex == fromLevelPortalIndex) {
            game.root.logger.Warning("found! returning " + locationAndTile.Key);
            return locationAndTile.Key;
          }
        }
      }
      game.root.logger.Error("Couldnt figure out where to place unit!");
      return levelSuperstate.GetRandomWalkableLocation(game.rand, true);
    }

    public static Atharia.Model.Void Generate(
        this CliffLevelController cliffLevelController,
        Game game) {
      return new Atharia.Model.Void();
    }
  }
}
