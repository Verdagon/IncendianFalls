using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class EmberDeepUnitsAndItems {
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

      float irklingWeight = Normal(.4f, 0, 1f, levelGameFraction);
      float baugWeight = Normal(.4f, .4f, 1f, levelGameFraction);
      float kwargWeight = Normal(.4f, .5f, 1f, levelGameFraction);
      float irklingKingWeight = levelGameFraction < .35 ? 0 : Logistic(.2f, .3f, 1, levelGameFraction);
      float emberfolkWeight = levelGameFraction < .4 ? 0 : Logistic(.2f, .5f, 1, levelGameFraction);
      float mantisBombardierWeight = levelGameFraction < .5 ? 0 : Logistic(.2f, .7f, 1.0f, levelGameFraction);

      int numIrkling = 0;
      int numBaug = 0;
      int numKwarg = 0;
      int numIrklingKing = 0;
      int numEmberfolk = 0;
      int numEtherDrake = 0;
      int numMantisBombardier = 0;

      if (LevelHasSpecial(depth, .1f)) {
        numKwarg += NUM_UNITS_PER_LEVEL / 12;
      }
      if (LevelHasSpecial(depth, .19f)) {
        numIrklingKing += NUM_UNITS_PER_LEVEL / 20;
      }
      if (LevelHasSpecial(depth, .28f)) {
        numEmberfolk += NUM_UNITS_PER_LEVEL / 20;
      }
      if (LevelHasSpecial(depth, .37f)) {
        numBaug += NUM_UNITS_PER_LEVEL / 4;
        numIrklingKing += NUM_UNITS_PER_LEVEL / 20;
        numEmberfolk += NUM_UNITS_PER_LEVEL / 20;
      }
      if (LevelHasSpecial(depth, .46f)) {
        numEtherDrake += NUM_UNITS_PER_LEVEL / 20;
      }
      if (LevelHasSpecial(depth, .55f)) {
        numBaug += NUM_UNITS_PER_LEVEL;
      }
      if (LevelHasSpecial(depth, .64f)) {
        numMantisBombardier += NUM_UNITS_PER_LEVEL / 20;
      }
      if (LevelHasSpecial(depth, .73f)) {
        numEtherDrake += NUM_UNITS_PER_LEVEL / 10;
        numBaug += NUM_UNITS_PER_LEVEL / 2;
      }
      if (LevelHasSpecial(depth, .82f)) {
        numEmberfolk += NUM_UNITS_PER_LEVEL / 10;
        numKwarg += NUM_UNITS_PER_LEVEL / 2;
      }
      if (LevelHasSpecial(depth, .91f)) {
        numEtherDrake += NUM_UNITS_PER_LEVEL / 10;
      }

      int numPreorderedUnits =
          numIrkling +
          numBaug +
          numKwarg +
          numIrklingKing +
          numEmberfolk +
          numEtherDrake +
          numMantisBombardier;
      int numUnitsNeeded = numUnits - numPreorderedUnits;

      float[] weights = {
        irklingWeight,
        baugWeight,
        kwargWeight,
        irklingKingWeight,
        emberfolkWeight,
        mantisBombardierWeight,
      };

      int[] numAddedByUnit = RunWeights(rand, weights, numUnitsNeeded);
      return new int[] {
        numAddedByUnit[0] + numIrkling,
        numAddedByUnit[1] + numBaug,
        numAddedByUnit[2] + numKwarg,
        numAddedByUnit[3] + numIrklingKing,
        numAddedByUnit[4] + numEmberfolk,
        numEtherDrake,
        numAddedByUnit[5] + numMantisBombardier
      };
    }

    public static void FillWithUnits(
        Game game,
        Level level,
        LevelSuperstate levelSuperstate,
        int depth) {
      int[] numByUnit = DetermineUnitsForLevel(game.rand, depth, NUM_UNITS_PER_LEVEL);
      int numIrkling = numByUnit[0];
      int numBaug = numByUnit[1];
      int numKwarg = numByUnit[2];
      int numIrklingKing = numByUnit[3];
      int numEmberfolk = numByUnit[4];
      int numEtherDrake = numByUnit[5];
      int numMantisBombardier = numByUnit[6];

      for (int i = 0; i < numIrkling; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, true, true)[0],
          game.time + 10,
          Irkling.Make(level.root));
      }

      for (int i = 0; i < numBaug; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, true, true)[0],
          game.time + 10,
          Baug.Make(level.root));
      }

      //for (int i = 0; i < numKwarg; i++) {
      //  level.EnterUnit(
      //    levelSuperstate,
      //    levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, true, true)[0],
      //    game.time + 10,
      //    Kwarg.Make(level.root));
      //}

      //for (int i = 0; i < numIrklingKing; i++) {
      //  level.EnterUnit(
      //    levelSuperstate,
      //    levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, true, true)[0],
      //    game.time + 10,
      //    IrklingKing.Make(level.root));
      //}

      //for (int i = 0; i < numEmberfolk; i++) {
      //  level.EnterUnit(
      //    levelSuperstate,
      //    levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, true, true)[0],
      //    game.time + 10,
      //    Emberfolk.Make(level.root));
      //}

      //for (int i = 0; i < numEtherDrake; i++) {
      //  level.EnterUnit(
      //    levelSuperstate,
      //    levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, true, true)[0],
      //    game.time + 10,
      //    EtherDrake.Make(level.root));
      //}

      //for (int i = 0; i < numMantisBombardier; i++) {
      //  level.EnterUnit(
      //    levelSuperstate,
      //    levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, true, true)[0],
      //    game.time + 10,
      //    MantisBombardier.Make(level.root));
      //}
    }
  }
}
