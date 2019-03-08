using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class GenerationCommon {
    public static void GetMapBounds(
        out float mapMinX,
        out float mapMinY,
        out float mapMaxX,
        out float mapMaxY,
        Terrain terrain) {
      mapMinX = 0;
      mapMinY = 0;
      mapMaxX = 0;
      mapMaxY = 0;

      foreach (var entry in terrain.tiles) {
        var location = entry.Key;
        var center = terrain.pattern.GetTileCenter(location);
        mapMinX = Math.Min(mapMinX, center.x);
        mapMinY = Math.Min(mapMinY, center.y);
        mapMaxX = Math.Max(mapMaxX, center.x);
        mapMaxY = Math.Max(mapMaxY, center.y);
      }
    }

    public static void PlaceStaircase(
        Terrain terrain,
        Location location,
        bool down,
        int portalIndex,
        Level destinationLevel,
        int destinationLevelPortalIndex) {
      var upStaircaseTile = terrain.tiles[location];
      upStaircaseTile.components.Add(
          new StaircaseTTCAsITerrainTileComponent(
              terrain.root.EffectStaircaseTTCCreate(
                  portalIndex, destinationLevel, destinationLevelPortalIndex)));
      upStaircaseTile.components.Add(
          new DecorativeTTCAsITerrainTileComponent(
              terrain.root.EffectDecorativeTTCCreate(down ? "downstairs" : "upstairs")));
    }

    public static Location GetLocationClosestTo(
        Terrain terrain,
        Vec2 targetPos) {
      Location closestLocation = new Location(0, 0, 0);
      float closestLocationDistance =
          terrain.pattern.GetTileCenter(closestLocation)
          .distance(targetPos);

      foreach (var hay in terrain.tiles) {
        var hayLoc = hay.Key;
        var hayCenter = terrain.pattern.GetTileCenter(hayLoc);
        var hayDistance = hayCenter.distance(targetPos);
        if (hayDistance < closestLocationDistance) {
          closestLocation = hayLoc;
          closestLocationDistance = hayDistance;
        }
      }

      return closestLocation;
    }

    public static void PlaceItems(SSContext context, Rand rand, Level level, LevelSuperstate levelSuperstate) {
      List<Location> glaiveLocations =
          levelSuperstate.GetNRandomWalkableLocations(
              rand,
              levelSuperstate.NumWalkableLocations(false) / 20,
              new SortedSet<Location>(),
              false);

      foreach (var itemLocation in glaiveLocations) {
        var rockTile = level.terrain.tiles[itemLocation];
        rockTile.components.Add(
            context.root.EffectItemTTCCreate(
                context.root.EffectGlaiveCreate().AsIItem())
            .AsITerrainTileComponent());
      }

      List<Location> armorLocations =
          levelSuperstate.GetNRandomWalkableLocations(
              rand,
              levelSuperstate.NumWalkableLocations(false) / 20,
              new SortedSet<Location>(), 
              false);

      foreach (var itemLocation in armorLocations) {
        var rockTile = level.terrain.tiles[itemLocation];
        rockTile.components.Add(
            context.root.EffectItemTTCCreate(
                context.root.EffectArmorCreate().AsIItem())
            .AsITerrainTileComponent());
      }
    }

    public static void PlaceRocks(SSContext context, Rand rand, Level level, LevelSuperstate levelSuperstate) {
      List<Location> rockLocations =
          levelSuperstate.GetNRandomWalkableLocations(
              rand,
              levelSuperstate.NumWalkableLocations(false) / 20,
              new SortedSet<Location>(),
              false);

      foreach (var rockLocation in rockLocations) {
        var rockTile = level.terrain.tiles[rockLocation];
        rockTile.components.Add(
            context.root.EffectDecorativeTTCCreate("rocks")
            .AsITerrainTileComponent());
      }
    }

    //public static void PlaceStaircases(SSContext context, Rand rand, Level level, LevelSuperstate levelSuperstate) {
    //  List<Location> staircaseLocations =
    //      levelSuperstate.GetNRandomWalkableLocations(
    //          rand, 2, false);

    //  var upStaircaseLocation = staircaseLocations[0];
    //  var downStaircaseLocation = staircaseLocations[1];

    //  var upStaircaseTile = level.terrain.tiles[upStaircaseLocation];
    //  upStaircaseTile.components.Add(
    //      new UpStaircaseTTCAsITerrainTileComponent(
    //          context.root.EffectUpStaircaseTTCCreate()));

    //  var downStaircaseTile = level.terrain.tiles[downStaircaseLocation];
    //  downStaircaseTile.components.Add(
    //      new DownStaircaseTTCAsITerrainTileComponent(context.root.EffectDownStaircaseTTCCreate()));
    //}

    public static Location GetFurthestLocationInDirection(
        Pattern pattern,
        SortedSet<Location> locs,
        Vec2 direction) {
      Location furthestLoc = SetUtils.GetFirst(locs);
      float furthestDistance =
          pattern.GetTileCenter(furthestLoc)
          .dot(direction);
      foreach (var hayLoc in locs) {
        float hayDistance =
            pattern.GetTileCenter(hayLoc)
            .dot(direction);
        if (hayDistance > furthestDistance) {
          furthestDistance = hayDistance;
          furthestLoc = hayLoc;
        }
      }
      return furthestLoc;
    }

    private static float Normal(float stretchX, float shiftX, float stretchY, float x) {
      return (float)Math.Pow(Math.E, -0.5f * Math.Pow((x - shiftX) / stretchX, 2)) * stretchY;
    }

    private static float Logistic(float stretchX, float shiftX, float stretchY, float x) {
      return 1.0f / (1.0f + (float)Math.Pow(Math.E, (-(x - shiftX) / stretchX))) * stretchY;
    }

    private static int RunWeight(Rand rand, float[] weights) {
      float weightSum = 0;
      float[] upperBounds = new float[weights.Length];
      for (int i = 0; i < weights.Length; i++) {
        weightSum += weights[i];
        upperBounds[i] = weightSum;
      }
      float x = rand.Next(0, weightSum, 10000);
      for (int i = 0; i < weights.Length; i++) {
        if (x <= upperBounds[i]) {
          return i;
        }
      }
      rand.root.logger.Error("Weight distribution gave out of bounds! " + weightSum + " " + x);
      return 0;
    }

    private static int[] RunWeights(Rand rand, float[] weights, int n) {
      int[] results = new int[weights.Length];
      for (int ni = 0; ni < n; ni++) {
        int resultIndex = RunWeight(rand, weights);
        results[resultIndex]++;
      }
      return results;
    }

    public static int[] DetermineUnitsForLevel(
        Rand rand,
        int depth,
        bool inside,
        int numUnits) {

      float aveliskWeight = Normal(8, 0, 1f, depth);
      float ravafaireWeight = Normal(8, 8, 1f, depth);
      float draxlingWeight = Normal(8, 12, 1f, depth);
      float lornixWeight = depth < 7 ? 0 : Logistic(4, 10, 1, depth);
      float yotenWeight = depth < 8 ? 0 : Logistic(2, 14, 1, depth);
      float mordranthWeight = depth < 10 ? 0 : Logistic(1, 14, .5f, depth);

      int numAvelisk = 0;
      int numRavafaire = 0;
      int numDraxling = 0;
      int numLornix = 0;
      int numYoten = 0;
      int numSpiriad = 0;
      int numMordranth = 0;

      int depthAndCave = depth * (inside ? -1 : 1);
      switch (depthAndCave) {
        case -2:
          numDraxling = 6;
          break;
        case -3:
          numLornix = 1;
          break;
        case 4:
          numYoten = 1;
          break;
        case -6:
          numRavafaire = 5;
          numLornix = 1;
          break;
        case 7:
          numSpiriad = 1;
          break;
        case 8:
          numRavafaire = 15;
          break;
        case -9:
          numMordranth = 1;
          break;
        case -10:
          numSpiriad = 1;
          numRavafaire = 10;
          break;
        case 11:
          numYoten = 1;
          numDraxling = 10;
          break;
        case 15:
          numYoten = 4;
          break;
        case 16:
          numSpiriad = 2;
          break;
      }

      int numPreorderedUnits =
          numAvelisk +
          numRavafaire +
          numDraxling +
          numLornix +
          numYoten +
          numSpiriad +
          numMordranth;
      int numUnitsNeeded = numUnits - numPreorderedUnits;

      float[] weights = {
        aveliskWeight,
        ravafaireWeight,
        draxlingWeight,
        lornixWeight,
        yotenWeight,
        mordranthWeight,
      };

      int[] numAddedByUnit = RunWeights(rand, weights, numUnitsNeeded);
      return new int[] {
        numAddedByUnit[0] + numAvelisk,
        numAddedByUnit[1] + numRavafaire,
        numAddedByUnit[2] + numDraxling,
        numAddedByUnit[3] + numLornix,
        numAddedByUnit[4] + numYoten,
        numSpiriad,
        numAddedByUnit[5] + numMordranth
      };
    }

    public static void FillWithUnits(
        SSContext context,
        Game game,
        Level level,
        LevelSuperstate levelSuperstate,
        int depth,
        bool inside) {

      int numUnits = 15;

      int[] numByUnit = DetermineUnitsForLevel(game.rand, depth, inside, numUnits);
      int numAvelisk = numByUnit[0];
      int numRavafaire = numByUnit[1];
      int numDraxling = numByUnit[2];
      int numLornix = numByUnit[3];
      int numYoten = numByUnit[4];
      int numSpiriad = numByUnit[5];
      int numMordranth = numByUnit[6];

      for (int i = 0; i < numAvelisk; i++) {
        var components = IUnitComponentMutBunch.New(context.root);
        components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
        components.Add(context.root.EffectAttackAICapabilityUCCreate().AsIUnitComponent());
        Unit unit =
            context.root.EffectUnitCreate(
                context.root.EffectIUnitEventMutListCreate(),
                true,
                0,
                new Location(0, 0, 0),
                "avelisk",
                5, 5,
                0, 0,
                600,
                game.time + 10,
                components,
                IItemMutBunch.New(context.root),
                false,
                5);
        level.EnterUnit(game, levelSuperstate, unit, level, 0);
      }

      for (int i = 0; i < numRavafaire; i++) {
        var components = IUnitComponentMutBunch.New(context.root);
        components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
        components.Add(context.root.EffectAttackAICapabilityUCCreate().AsIUnitComponent());
        Unit unit =
            context.root.EffectUnitCreate(
                context.root.EffectIUnitEventMutListCreate(),
                true,
                0,
                new Location(0, 0, 0),
                "novafaire",
                8, 8,
                0, 0,
                600,
                game.time + 10,
                components,
                IItemMutBunch.New(context.root),
                false,
                8);
        level.EnterUnit(game, levelSuperstate, unit, level, 0);
      }

      for (int i = 0; i < numDraxling; i++) {
        var components = IUnitComponentMutBunch.New(context.root);
        components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
        components.Add(context.root.EffectAttackAICapabilityUCCreate().AsIUnitComponent());
        Unit unit =
            context.root.EffectUnitCreate(
                context.root.EffectIUnitEventMutListCreate(),
                true,
                0,
                new Location(0, 0, 0),
                "draxling",
                5, 5,
                0, 0,
                300,
                game.time + 10,
                components,
                IItemMutBunch.New(context.root),
                false,
                5);
        level.EnterUnit(game, levelSuperstate, unit, level, 0);
      }

      for (int i = 0; i < numLornix; i++) {
        var components = IUnitComponentMutBunch.New(context.root);
        components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
        components.Add(context.root.EffectAttackAICapabilityUCCreate().AsIUnitComponent());
        components.Add(context.root.EffectBideAICapabilityUCCreate().AsIUnitComponent());
        Unit unit =
            context.root.EffectUnitCreate(
                context.root.EffectIUnitEventMutListCreate(),
                true,
                0,
                new Location(0, 0, 0),
                "lornix",
                50, 50,
                0, 0,
                300,
                game.time + 10,
                components,
                IItemMutBunch.New(context.root),
                false,
                5);
        level.EnterUnit(game, levelSuperstate, unit, level, 0);
      }

      for (int i = 0; i < numYoten; i++) {
        var components = IUnitComponentMutBunch.New(context.root);
        components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
        components.Add(context.root.EffectAttackAICapabilityUCCreate().AsIUnitComponent());
        Unit unit =
            context.root.EffectUnitCreate(
                context.root.EffectIUnitEventMutListCreate(),
                true,
                0,
                new Location(0, 0, 0),
                "yoten",
                50, 50,
                0, 0,
                300,
                game.time + 10,
                components,
                IItemMutBunch.New(context.root),
                false,
                15);
        level.EnterUnit(game, levelSuperstate, unit, level, 0);
      }

      for (int i = 0; i < numSpiriad; i++) {
        var components = IUnitComponentMutBunch.New(context.root);
        components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
        components.Add(context.root.EffectAttackAICapabilityUCCreate().AsIUnitComponent());
        Unit unit =
            context.root.EffectUnitCreate(
                context.root.EffectIUnitEventMutListCreate(),
                true,
                0,
                new Location(0, 0, 0),
                "spiriad",
                25, 25,
                0, 0,
                600,
                game.time + 10,
                components,
                IItemMutBunch.New(context.root),
                false,
                65);
        level.EnterUnit(game, levelSuperstate, unit, level, 0);
      }

      for (int i = 0; i < numMordranth; i++) {
        var components = IUnitComponentMutBunch.New(context.root);
        components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
        components.Add(context.root.EffectAttackAICapabilityUCCreate().AsIUnitComponent());
        components.Add(context.root.EffectBideAICapabilityUCCreate().AsIUnitComponent());
        Unit unit =
            context.root.EffectUnitCreate(
                context.root.EffectIUnitEventMutListCreate(),
                true,
                0,
                new Location(0, 0, 0),
                "mordranth",
                60, 60,
                0, 0,
                600,
                game.time + 10,
                components,
                IItemMutBunch.New(context.root),
                false,
                12);
        level.EnterUnit(game, levelSuperstate, unit, level, 0);
      }
    }

  }


}
