using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class GenerationCommon {
    public static readonly int TOTAL_NUM_LEVELS_BEFORE_BOSS = 7;
    public static readonly int NUM_UNITS_PER_LEVEL = 30;

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
      if (down) {
        upStaircaseTile.components.Add(
          terrain.root.EffectDownstairsTTCCreate().AsITerrainTileComponent());
      } else {
        upStaircaseTile.components.Add(
          terrain.root.EffectUpstairsTTCCreate().AsITerrainTileComponent());
      }
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

    private static void PlaceItemNextToEntry(
        Level level,
        Location entryLoc,
        ITerrainTileComponent itc) {
      foreach (var entryAdjLoc in level.terrain.GetAdjacentExistingLocations(entryLoc, false)) {
        if (level.terrain.tiles[entryAdjLoc].IsWalkable() &&
            level.terrain.GetElevationDifference(entryLoc, entryAdjLoc) <= 2) {
          level.terrain.tiles[entryAdjLoc].components.Add(itc);
          return;
        }
      }
      level.root.logger.Error("Couldn't place item!");
      itc.Destruct();
    }

    public static void PlaceItems(
        SSContext context,
        Rand rand,
        Level level,
        LevelSuperstate levelSuperstate,
        int levelIndex,
        Location entryLoc,
        float healthPotionDensity,
        float manaPotionDensity) {
      if (LevelHasSpecial(levelIndex, .21f)) {
        PlaceItemNextToEntry(
            level,
            entryLoc,
            context.root.EffectGlaiveCreate().AsITerrainTileComponent());
      }
      if (LevelHasSpecial(levelIndex, .34f)) {
        PlaceItemNextToEntry(
            level,
            entryLoc,
            context.root.EffectArmorCreate().AsITerrainTileComponent());
      }
      if (LevelHasSpecial(levelIndex, .53f)) {
        PlaceItemNextToEntry(
            level,
            entryLoc,
            context.root.EffectInertiaRingCreate().AsITerrainTileComponent());
      }


      List<Location> healthLocs =
          levelSuperstate.GetNRandomWalkableLocations(
              level.terrain,
              rand,
              (int)(levelSuperstate.NumWalkableLocations(false) * healthPotionDensity),
              true,
              false);

      foreach (var healthLoc in healthLocs) {
        var rockTile = level.terrain.tiles[healthLoc];
        rockTile.components.Add(
            context.root.EffectHealthPotionCreate()
            .AsITerrainTileComponent());
      }

      List<Location> manaLocs =
          levelSuperstate.GetNRandomWalkableLocations(
            level.terrain,
              rand,
              (int)(levelSuperstate.NumWalkableLocations(false) * manaPotionDensity),
              true, 
              false);

      foreach (var manaLoc in manaLocs) {
        var rockTile = level.terrain.tiles[manaLoc];
        rockTile.components.Add(
            context.root.EffectManaPotionCreate()
            .AsITerrainTileComponent());
      }
    }

    public static void PlaceRocks(SSContext context, Rand rand, Level level, LevelSuperstate levelSuperstate) {
      List<Location> rockLocations =
          levelSuperstate.GetNRandomWalkableLocations(
              level.terrain,
              rand,
              levelSuperstate.NumWalkableLocations(false) / 20,
              true,
              false);

      foreach (var rockLocation in rockLocations) {
        var rockTile = level.terrain.tiles[rockLocation];
        rockTile.components.Add(
            context.root.EffectRocksTTCCreate().AsITerrainTileComponent());
      }
    }

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
      return 1.0f / (1.0f + (float)Math.Pow(Math.E, -(x - shiftX) / stretchX)) * stretchY;
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

    private static bool LevelHasSpecial(int levelIndex, float specialGameFraction) {
      float levelGameFractionStart = (float)levelIndex / TOTAL_NUM_LEVELS_BEFORE_BOSS;
      float levelGameFractionEnd = (float)(levelIndex + 1) / TOTAL_NUM_LEVELS_BEFORE_BOSS;
      return levelGameFractionStart <= specialGameFraction &&
          specialGameFraction < levelGameFractionEnd;
    }

    public static int[] DetermineUnitsForLevel(
        Rand rand,
        int levelIndex,
        int numUnits) {

      float levelGameFraction = (float)levelIndex / TOTAL_NUM_LEVELS_BEFORE_BOSS;

      float aveliskWeight = Normal(.4f, 0, 1f, levelGameFraction);
      float novafaireWeight = Normal(.4f, .4f, 1f, levelGameFraction);
      float draxlingWeight = Normal(.4f, .5f, 1f, levelGameFraction);
      float lornixWeight = levelGameFraction < .35 ? 0 : Logistic(.2f, .3f, 1, levelGameFraction);
      float yotenWeight = levelGameFraction < .4 ? 0 : Logistic(.2f, .5f, 1, levelGameFraction);
      float mordranthWeight = levelGameFraction < .5 ? 0 : Logistic(.2f, .7f, 1.0f, levelGameFraction);

      int numAvelisk = 0;
      int numNovafaire = 0;
      int numDraxling = 0;
      int numLornix = 0;
      int numYoten = 0;
      int numSpiriad = 0;
      int numMordranth = 0;

      if (LevelHasSpecial(levelIndex, .1f)) {
        numDraxling += NUM_UNITS_PER_LEVEL / 12;
      }
      if (LevelHasSpecial(levelIndex, .19f)) {
        numLornix += NUM_UNITS_PER_LEVEL / 20;
      }
      if (LevelHasSpecial(levelIndex, .28f)) {
        numYoten += NUM_UNITS_PER_LEVEL / 20;
      }
      if (LevelHasSpecial(levelIndex, .37f)) {
        numNovafaire += NUM_UNITS_PER_LEVEL / 4;
        numLornix += NUM_UNITS_PER_LEVEL / 20;
        numYoten += NUM_UNITS_PER_LEVEL / 20;
      }
      if (LevelHasSpecial(levelIndex, .46f)) {
        numSpiriad += NUM_UNITS_PER_LEVEL / 20;
      }
      if (LevelHasSpecial(levelIndex, .55f)) {
        numNovafaire += NUM_UNITS_PER_LEVEL;
      }
      if (LevelHasSpecial(levelIndex, .64f)) {
        numMordranth += NUM_UNITS_PER_LEVEL / 20;
      }
      if (LevelHasSpecial(levelIndex, .73f)) {
        numSpiriad += NUM_UNITS_PER_LEVEL / 10;
        numNovafaire += NUM_UNITS_PER_LEVEL / 2;
      }
      if (LevelHasSpecial(levelIndex, .82f)) {
        numYoten += NUM_UNITS_PER_LEVEL / 10;
        numDraxling += NUM_UNITS_PER_LEVEL / 2;
      }
      if (LevelHasSpecial(levelIndex, .91f)) {
        numSpiriad += NUM_UNITS_PER_LEVEL / 10;
      }

      int numPreorderedUnits =
          numAvelisk +
          numNovafaire +
          numDraxling +
          numLornix +
          numYoten +
          numSpiriad +
          numMordranth;
      int numUnitsNeeded = numUnits - numPreorderedUnits;

      float[] weights = {
        aveliskWeight,
        novafaireWeight,
        draxlingWeight,
        lornixWeight,
        yotenWeight,
        mordranthWeight,
      };

      int[] numAddedByUnit = RunWeights(rand, weights, numUnitsNeeded);
      return new int[] {
        numAddedByUnit[0] + numAvelisk,
        numAddedByUnit[1] + numNovafaire,
        numAddedByUnit[2] + numDraxling,
        numAddedByUnit[3] + numLornix,
        numAddedByUnit[4] + numYoten,
        numSpiriad,
        numAddedByUnit[5] + numMordranth
      };
    }

    public static void PlaceAvelisk(
        SSContext context,
        Game game, 
        Level level,
        LevelSuperstate levelSuperstate,
        Level fromLevel,
        int fromLevelPortalIndex) {
      var components = IUnitComponentMutBunch.New(context.root);
      components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(context.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      Unit unit =
          context.root.EffectUnitCreate(
              context.root.EffectIUnitEventMutListCreate(),
              true,
              0,
              new Location(0, 0, 0),
              "avelisk",
              9, 9,
              0, 0,
              600,
              game.time + 10,
              components,
              false,
              5);
      level.EnterUnit(game, levelSuperstate, unit, fromLevel, fromLevelPortalIndex);
    }

    public static void PlaceNovafaire(
        SSContext context,
        Game game,
        Level level,
        LevelSuperstate levelSuperstate,
        Level fromLevel,
        int fromLevelPortalIndex) {
      var components = IUnitComponentMutBunch.New(context.root);
      components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(context.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(context.root.EffectBideAICapabilityUCCreate(0).AsIUnitComponent());
      Unit unit =
          context.root.EffectUnitCreate(
              context.root.EffectIUnitEventMutListCreate(),
              true,
              0,
              new Location(0, 0, 0),
              "novafaire",
              27, 27,
              0, 0,
              600,
              game.time + 10,
              components,
              false,
              8);
      level.EnterUnit(game, levelSuperstate, unit, fromLevel, fromLevelPortalIndex);
    }

    public static void PlaceDraxling(
        SSContext context,
        Game game,
        Level level,
        LevelSuperstate levelSuperstate,
        Level fromLevel,
        int fromLevelPortalIndex) {
      var components = IUnitComponentMutBunch.New(context.root);
      components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(context.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
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
              false,
              3);
      level.EnterUnit(game, levelSuperstate, unit, fromLevel, fromLevelPortalIndex);
    }

    public static void PlaceLornix(
        SSContext context,
        Game game,
        Level level,
        LevelSuperstate levelSuperstate,
        Level fromLevel,
        int fromLevelPortalIndex) {
      var components = IUnitComponentMutBunch.New(context.root);
      components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(context.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(context.root.EffectBideAICapabilityUCCreate(0).AsIUnitComponent());
      Unit unit =
          context.root.EffectUnitCreate(
              context.root.EffectIUnitEventMutListCreate(),
              true,
              0,
              new Location(0, 0, 0),
              "lornix",
              120, 120,
              0, 0,
              600,
              game.time + 10,
              components,
              false,
              36);
      level.EnterUnit(game, levelSuperstate, unit, fromLevel, fromLevelPortalIndex);
    }

    public static void PlaceYoten(
        SSContext context,
        Game game,
        Level level,
        LevelSuperstate levelSuperstate,
        Level fromLevel,
        int fromLevelPortalIndex) {
      var components = IUnitComponentMutBunch.New(context.root);
      components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(context.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      Unit unit =
          context.root.EffectUnitCreate(
              context.root.EffectIUnitEventMutListCreate(),
              true,
              0,
              new Location(0, 0, 0),
              "yoten",
              100, 100,
              0, 0,
              600,
              game.time + 10,
              components,
              false,
              30);
      level.EnterUnit(game, levelSuperstate, unit, fromLevel, fromLevelPortalIndex);
    }

    public static void PlaceSpiriad(
        SSContext context,
        Game game,
        Level level,
        LevelSuperstate levelSuperstate,
        Level fromLevel,
        int fromLevelPortalIndex) {
      var components = IUnitComponentMutBunch.New(context.root);
      components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(context.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      Unit unit =
          context.root.EffectUnitCreate(
              context.root.EffectIUnitEventMutListCreate(),
              true,
              0,
              new Location(0, 0, 0),
              "spiriad",
              50, 50,
              0, 0,
              600,
              game.time + 10,
              components,
              false,
              130);
      level.EnterUnit(game, levelSuperstate, unit, fromLevel, fromLevelPortalIndex);
    }


    public static void PlaceMordranth(
        SSContext context,
        Game game,
        Level level,
        LevelSuperstate levelSuperstate,
        Level fromLevel,
        int fromLevelPortalIndex) {
      var components = IUnitComponentMutBunch.New(context.root);
      components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(context.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(context.root.EffectBideAICapabilityUCCreate(0).AsIUnitComponent());
      Unit unit =
          context.root.EffectUnitCreate(
              context.root.EffectIUnitEventMutListCreate(),
              true,
              0,
              new Location(0, 0, 0),
              "mordranth",
              120, 120,
              0, 0,
              600,
              game.time + 10,
              components,
              false,
              48);
      level.EnterUnit(game, levelSuperstate, unit, fromLevel, fromLevelPortalIndex);
    }

    public static void FillWithUnits(
        SSContext context,
        Game game,
        Level level,
        LevelSuperstate levelSuperstate,
        int levelIndex) {

      int[] numByUnit = DetermineUnitsForLevel(game.rand, levelIndex, NUM_UNITS_PER_LEVEL);
      int numAvelisk = numByUnit[0];
      int numNovafaire = numByUnit[1];
      int numDraxling = numByUnit[2];
      int numLornix = numByUnit[3];
      int numYoten = numByUnit[4];
      int numSpiriad = numByUnit[5];
      int numMordranth = numByUnit[6];

      for (int i = 0; i < numAvelisk; i++) {
        PlaceAvelisk(context, game, level, levelSuperstate, level, 0);
      }

      for (int i = 0; i < numNovafaire; i++) {
        PlaceNovafaire(context, game, level, levelSuperstate, level, 0);
      }

      for (int i = 0; i < numDraxling; i++) {
        PlaceDraxling(context, game, level, levelSuperstate, level, 0);
      }

      for (int i = 0; i < numLornix; i++) {
        PlaceLornix(context, game, level, levelSuperstate, level, 0);
      }

      for (int i = 0; i < numYoten; i++) {
        PlaceYoten(context, game, level, levelSuperstate, level, 0);
      }

      for (int i = 0; i < numSpiriad; i++) {
        PlaceSpiriad(context, game, level, levelSuperstate, level, 0);
      }

      for (int i = 0; i < numMordranth; i++) {
        PlaceMordranth(context, game, level, levelSuperstate, level, 0);
      }
    }

  }


}
