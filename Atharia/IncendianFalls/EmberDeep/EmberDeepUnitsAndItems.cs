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
        LocationPredicate locationFilter,
        float healthPotionDensity,
        float manaPotionDensity) {
      List<Location> healthLocs =
          levelSuperstate.GetNRandomWalkableLocations(
              level.terrain,
              rand,
              (int)(levelSuperstate.NumWalkableLocations(false) * healthPotionDensity),
              locationFilter,
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
              locationFilter,
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

    public static void FillWithUnits(
        Game game,
        Level level,
        LevelSuperstate levelSuperstate,
        LocationPredicate locationFilter,
        int numIrkling,
        int numDraxling,
        int numRavagianTrask,
        int numBaug,
        int numSpirient,
        int numIrklingKing,
        int numEmberfolk,
        int numChronolisk,
        int numMantisBombardier,
        int numLightningTrask) {
      for (int i = 0; i < numIrkling; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, locationFilter, true, true)[0],
          game.time + 10,
          Irkling.Make(level.root));
      }
      for (int i = 0; i < numDraxling; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, locationFilter, true, true)[0],
          game.time + 10,
          Draxling.Make(level.root));
      }
      for (int i = 0; i < numRavagianTrask; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, locationFilter, true, true)[0],
          game.time + 10,
          RavagianTrask.Make(level.root));
      }
      for (int i = 0; i < numBaug; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, locationFilter, true, true)[0],
          game.time + 10,
          Baug.Make(level.root));
      }
      for (int i = 0; i < numSpirient; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, locationFilter, true, true)[0],
          game.time + 10,
          Spirient.Make(level.root));
      }
      for (int i = 0; i < numIrklingKing; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, locationFilter, true, true)[0],
          game.time + 10,
          IrklingKing.Make(level.root));
      }
      for (int i = 0; i < numEmberfolk; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, locationFilter, true, true)[0],
          game.time + 10,
          Emberfolk.Make(level.root));
      }
      for (int i = 0; i < numChronolisk; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, locationFilter, true, true)[0],
          game.time + 10,
          Chronolisk.Make(level.root));
      }
      for (int i = 0; i < numMantisBombardier; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, locationFilter, true, true)[0],
          game.time + 10,
          MantisBombardier.Make(level.root));
      }
      for (int i = 0; i < numLightningTrask; i++) {
        level.EnterUnit(
          levelSuperstate,
          levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 1, locationFilter, true, true)[0],
          game.time + 10,
          LightningTrask.Make(level.root));
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
