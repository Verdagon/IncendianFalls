using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class IncendianFallsUnitsAndItems {
    public static readonly int TOTAL_NUM_LEVELS_BEFORE_BOSS = 7;
    public static readonly int NUM_UNITS_PER_LEVEL = 30;

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
            level.root.EffectSpeedRingCreate().AsIItem());
      }


      List<Location> healthLocs =
          levelSuperstate.GetNRandomWalkableLocations(
              level.terrain,
              rand,
              (int)(levelSuperstate.NumWalkableLocations(false) * healthPotionDensity),
              (loc) => true,
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
              (loc) => true,
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
              (loc) => true,
              true,
              false);

      foreach (var rockLocation in rockLocations) {
        var rockTile = level.terrain.tiles[rockLocation];
        rockTile.components.Add(
            level.root.EffectRocksTTCCreate().AsITerrainTileComponent());
      }
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
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1,
              (loc) => true, true, true)[0],
          game.time + 10,
          Avelisk.Make(level.root));
      }

      for (int i = 0; i < numNovafaire; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => true, true, true)[0],
          game.time + 10,
          Novafaire.Make(level.root));
      }

      for (int i = 0; i < numDraxling; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => true, true, true)[0],
          game.time + 10,
          Draxling.Make(level.root));
      }

      for (int i = 0; i < numLornix; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => true, true, true)[0],
          game.time + 10,
          Lornix.Make(level.root));
      }

      for (int i = 0; i < numYoten; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => true, true, true)[0],
          game.time + 10,
          Yoten.Make(level.root));
      }

      for (int i = 0; i < numSpiriad; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => true, true, true)[0],
          game.time + 10,
          Spiriad.Make(level.root));
      }

      for (int i = 0; i < numMordranth; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, (loc) => true, true, true)[0],
          game.time + 10,
          Mordranth.Make(level.root));
      }
    }
  }
}
