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

    //public static void PlaceStaircase(
    //    Terrain terrain,
    //    Location location,
    //    bool down,
    //    Level destinationLevel,
    //    Location destinationLevelLocation) {
    //  var upStaircaseTile = terrain.tiles[location];
    //  upStaircaseTile.components.Add(
    //    terrain.root.EffectLevelLinkTTCCreate(destinationLevel, destinationLevelLocation).AsITerrainTileComponent());
    //  if (down) {
    //    upStaircaseTile.components.Add(
    //      terrain.root.EffectDownStairsTTCCreate().AsITerrainTileComponent());
    //  } else {
    //    upStaircaseTile.components.Add(
    //      terrain.root.EffectUpStairsTTCCreate().AsITerrainTileComponent());
    //  }
    //}

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
        IItem item) {
      foreach (var entryAdjLoc in level.terrain.GetAdjacentExistingLocations(entryLoc, false)) {
        if (level.terrain.tiles[entryAdjLoc].IsWalkable() &&
            level.terrain.GetElevationDifference(entryLoc, entryAdjLoc) <= 2) {
          level.terrain.tiles[entryAdjLoc].components.Add(
            level.root.EffectItemTTCCreate(item).AsITerrainTileComponent());
          return;
        }
      }
      level.root.logger.Error("Couldn't place item!");
      item.Destruct();
    }

    public static void PlaceItems(
        Rand rand,
        Level level,
        LevelSuperstate levelSuperstate,
        int depth,
        Location entryLoc,
        float healthPotionDensity,
        float manaPotionDensity) {
      if (LevelHasSpecial(depth, .21f)) {
        PlaceItemNextToEntry(
            level,
            entryLoc,
            level.root.EffectGlaiveCreate().AsIItem());
      }
      if (LevelHasSpecial(depth, .34f)) {
        PlaceItemNextToEntry(
            level,
            entryLoc,
            level.root.EffectArmorCreate().AsIItem());
      }
      if (LevelHasSpecial(depth, .53f)) {
        PlaceItemNextToEntry(
            level,
            entryLoc,
            level.root.EffectInertiaRingCreate().AsIItem());
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
          level.root.EffectItemTTCCreate(
            level.root.EffectHealthPotionCreate().AsIItem())
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
          level.root.EffectItemTTCCreate(
            level.root.EffectManaPotionCreate().AsIItem())
            .AsITerrainTileComponent());
      }
    }

    public static void PlaceRocks(Rand rand, Level level, LevelSuperstate levelSuperstate) {
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
            level.root.EffectRocksTTCCreate().AsITerrainTileComponent());
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
        int depth,
        int numUnits) {

      float levelGameFraction = (float)depth / TOTAL_NUM_LEVELS_BEFORE_BOSS;

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

      if (LevelHasSpecial(depth, .1f)) {
        numDraxling += NUM_UNITS_PER_LEVEL / 12;
      }
      if (LevelHasSpecial(depth, .19f)) {
        numLornix += NUM_UNITS_PER_LEVEL / 20;
      }
      if (LevelHasSpecial(depth, .28f)) {
        numYoten += NUM_UNITS_PER_LEVEL / 20;
      }
      if (LevelHasSpecial(depth, .37f)) {
        numNovafaire += NUM_UNITS_PER_LEVEL / 4;
        numLornix += NUM_UNITS_PER_LEVEL / 20;
        numYoten += NUM_UNITS_PER_LEVEL / 20;
      }
      if (LevelHasSpecial(depth, .46f)) {
        numSpiriad += NUM_UNITS_PER_LEVEL / 20;
      }
      if (LevelHasSpecial(depth, .55f)) {
        numNovafaire += NUM_UNITS_PER_LEVEL;
      }
      if (LevelHasSpecial(depth, .64f)) {
        numMordranth += NUM_UNITS_PER_LEVEL / 20;
      }
      if (LevelHasSpecial(depth, .73f)) {
        numSpiriad += NUM_UNITS_PER_LEVEL / 10;
        numNovafaire += NUM_UNITS_PER_LEVEL / 2;
      }
      if (LevelHasSpecial(depth, .82f)) {
        numYoten += NUM_UNITS_PER_LEVEL / 10;
        numDraxling += NUM_UNITS_PER_LEVEL / 2;
      }
      if (LevelHasSpecial(depth, .91f)) {
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
        Game game, 
        Level level,
        LevelSuperstate levelSuperstate
        
        ) {
      var components = IUnitComponentMutBunch.New(level.root);
      components.Add(level.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(level.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      Unit unit =
          level.root.EffectUnitCreate(
              level.root.EffectIUnitEventMutListCreate(),
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
      var location =
        levelSuperstate.GetNRandomWalkableLocations(
            level.terrain, game.rand, 1, true, true)[0];
      level.EnterUnit(levelSuperstate, unit, location);
    }

    public static void PlaceNovafaire(
        
        Game game,
        Level level,
        LevelSuperstate levelSuperstate) {
      var components = IUnitComponentMutBunch.New(level.root);
      components.Add(level.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(level.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(level.root.EffectBideAICapabilityUCCreate(0).AsIUnitComponent());
      Unit unit =
          level.root.EffectUnitCreate(
              level.root.EffectIUnitEventMutListCreate(),
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
      var location =
        levelSuperstate.GetNRandomWalkableLocations(
            level.terrain, game.rand, 1, true, true)[0];
      level.EnterUnit(levelSuperstate, unit, location);
    }

    public static void PlaceDraxling(
        
        Game game,
        Level level,
        LevelSuperstate levelSuperstate
        
        ) {
      var components = IUnitComponentMutBunch.New(level.root);
      components.Add(level.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(level.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      Unit unit =
          level.root.EffectUnitCreate(
              level.root.EffectIUnitEventMutListCreate(),
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
      var location =
        levelSuperstate.GetNRandomWalkableLocations(
            level.terrain, game.rand, 1, true, true)[0];
      level.EnterUnit(levelSuperstate, unit, location);
    }

    public static void PlaceLornix(
        
        Game game,
        Level level,
        LevelSuperstate levelSuperstate
        
        ) {
      var components = IUnitComponentMutBunch.New(level.root);
      components.Add(level.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(level.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(level.root.EffectBideAICapabilityUCCreate(0).AsIUnitComponent());
      Unit unit =
          level.root.EffectUnitCreate(
              level.root.EffectIUnitEventMutListCreate(),
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
      var location =
        levelSuperstate.GetNRandomWalkableLocations(
            level.terrain, game.rand, 1, true, true)[0];
      level.EnterUnit(levelSuperstate, unit, location);
    }

    public static void PlaceYoten(
        
        Game game,
        Level level,
        LevelSuperstate levelSuperstate
        
        ) {
      var components = IUnitComponentMutBunch.New(level.root);
      components.Add(level.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(level.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      Unit unit =
          level.root.EffectUnitCreate(
              level.root.EffectIUnitEventMutListCreate(),
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
      var location =
        levelSuperstate.GetNRandomWalkableLocations(
            level.terrain, game.rand, 1, true, true)[0];
      level.EnterUnit(levelSuperstate, unit, location);
    }

    public static void PlaceSpiriad(
        
        Game game,
        Level level,
        LevelSuperstate levelSuperstate
        
        ) {
      var components = IUnitComponentMutBunch.New(level.root);
      components.Add(level.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(level.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      Unit unit =
          level.root.EffectUnitCreate(
              level.root.EffectIUnitEventMutListCreate(),
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
      var location =
        levelSuperstate.GetNRandomWalkableLocations(
            level.terrain, game.rand, 1, true, true)[0];
      level.EnterUnit(levelSuperstate, unit, location);
    }


    public static void PlaceMordranth(
        
        Game game,
        Level level,
        LevelSuperstate levelSuperstate
        
        ) {
      var components = IUnitComponentMutBunch.New(level.root);
      components.Add(level.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(level.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(level.root.EffectBideAICapabilityUCCreate(0).AsIUnitComponent());
      Unit unit =
          level.root.EffectUnitCreate(
              level.root.EffectIUnitEventMutListCreate(),
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
      var location =
        levelSuperstate.GetNRandomWalkableLocations(
            level.terrain, game.rand, 1, true, true)[0];
      level.EnterUnit(levelSuperstate, unit, location);
    }

    public static void FillWithUnits(
        
        Game game,
        Level level,
        LevelSuperstate levelSuperstate,
        int depth) {

      int[] numByUnit = DetermineUnitsForLevel(game.rand, depth, NUM_UNITS_PER_LEVEL);
      int numAvelisk = numByUnit[0];
      int numNovafaire = numByUnit[1];
      int numDraxling = numByUnit[2];
      int numLornix = numByUnit[3];
      int numYoten = numByUnit[4];
      int numSpiriad = numByUnit[5];
      int numMordranth = numByUnit[6];

      for (int i = 0; i < numAvelisk; i++) {
        PlaceAvelisk(game, level, levelSuperstate);
      }

      for (int i = 0; i < numNovafaire; i++) {
        PlaceNovafaire(game, level, levelSuperstate);
      }

      for (int i = 0; i < numDraxling; i++) {
        PlaceDraxling(game, level, levelSuperstate);
      }

      for (int i = 0; i < numLornix; i++) {
        PlaceLornix(game, level, levelSuperstate);
      }

      for (int i = 0; i < numYoten; i++) {
        PlaceYoten(game, level, levelSuperstate);
      }

      for (int i = 0; i < numSpiriad; i++) {
        PlaceSpiriad(game, level, levelSuperstate);
      }

      for (int i = 0; i < numMordranth; i++) {
        PlaceMordranth(game, level, levelSuperstate);
      }
    }

  }


}
