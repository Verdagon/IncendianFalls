using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class JumpingCaveLevelControllerExtensions {
    public static Atharia.Model.Void Destruct(this JumpingCaveLevelController self) {
      self.Delete();
      return new Atharia.Model.Void();
    }

    public static void LoadLevel(
        out Level levelRet,
        out LevelSuperstate levelSuperstate,
        out Location entryLocationRet,
        SSContext context,
        Game game,
        Superstate superstate,
        int depth) {
      bool considerCornersAdjacent = false;

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      Pattern pattern = PentagonPattern9.makePentagon9Pattern();
      if (depth == 2) {
        pattern = HexPattern.MakeHexPattern();
      }
      var terrain =
        JumpingCaveTerrainGenerator.Generate(
          context,
          game.root,
          pattern,
          game.rand,
          considerCornersAdjacent,
          12.0f);
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      foreach (var locationAndTile in terrain.tiles) {
        locationAndTile.Value.components.Add(game.root.EffectMudTTCCreate().AsITerrainTileComponent());
      }

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      var floors = new SortedSet<Location>(terrain.tiles.Keys);
      // var borderLocations = terrain.pattern.GetAdjacentLocations(floors, false, true);
      // foreach (var borderLocation in borderLocations) {
      //   if (!terrain.tiles.ContainsKey(borderLocation)) {
      //     var tile = game.root.EffectTerrainTileCreate(
      //         NullITerrainTileEvent.Null, 2, ITerrainTileComponentMutBunch.New(game.root));
      //     tile.components.Add(game.root.EffectCaveWallTTCCreate().AsITerrainTileComponent());
      //     terrain.tiles.Add(borderLocation, tile);
      //   }
      // }

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      var level =
          game.root.EffectLevelCreate(
          new Vec3(0, -8, 16),
              terrain,
              game.root.EffectUnitMutSetCreate(),
              NullILevelController.Null,
              game.time);
      levelSuperstate = new LevelSuperstate(level);

      context.Flare(context.root.GetDeterministicHashCode().ToString());


      var entryAndExitCandidateLocations = floors;
      var wideOpenLocations = level.terrain.pattern.GetInnerLocations(floors, considerCornersAdjacent);
      if (wideOpenLocations.Count >= 2) {
        entryAndExitCandidateLocations = wideOpenLocations;
      }
      var superWideOpenLocations = level.terrain.pattern.GetInnerLocations(wideOpenLocations, considerCornersAdjacent);
      if (superWideOpenLocations.Count >= 2) {
        entryAndExitCandidateLocations = superWideOpenLocations;
      }

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      var entryLocation =
        levelSuperstate.GetNRandomWalkableLocations(
          level.terrain,
          game.rand,
          1,
          (loc) => entryAndExitCandidateLocations.Contains(loc),
          false,
          false)[0];
      Asserts.Assert(wideOpenLocations.Contains(entryLocation), "wat");

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      var exitLocation =
        levelSuperstate.GetNRandomWalkableLocations(
          level.terrain,
          game.rand,
          1,
          (loc) => !loc.Equals(entryLocation),
          false,
          false)[0];
      level.terrain.tiles[exitLocation].components.Add(
        game.root.EffectCaveTTCCreate().AsITerrainTileComponent());
      level.terrain.tiles[exitLocation].components.Add(
        game.root.EffectEmberDeepLevelLinkerTTCCreate(depth + 1).AsITerrainTileComponent());

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      // EmberDeepUnitsAndItems.PlaceRocks(game.rand, level, levelSuperstate);
      //
      // context.Flare(context.root.GetDeterministicHashCode().ToString());

      level.controller = game.root.EffectJumpingCaveLevelControllerCreate(level, depth).AsILevelController();

      // if (depth == 0) {
      //   var nextToExitLocation =
      //     levelSuperstate.GetNRandomWalkableLocations(
      //       level.terrain,
      //       game.rand,
      //       1,
      //       (loc) => level.terrain.pattern.LocationsAreAdjacent(loc, exitLocation, false), true, true)[0];
      //   level.terrain.tiles[nextToExitLocation].components.Add(
      //     game.root.EffectItemTTCCreate(
      //       game.root.EffectSlowRodCreate().AsIItem())
      //     .AsITerrainTileComponent());
      //
      //   EmberDeepUnitsAndItems.PlaceItems(game.rand, level, levelSuperstate, (loc) => !loc.Equals(entryLocation), .02f, 0f);
      // } else {
      //   EmberDeepUnitsAndItems.PlaceItems(game.rand, level, levelSuperstate, (loc) => !loc.Equals(entryLocation), .02f, .02f);
      // }
      //
      // context.Flare(context.root.GetDeterministicHashCode().ToString());
      //
      // levelSuperstate.Reconstruct(level);
      // levelSuperstate.AddNoUnitZone(entryLocation, 3);
      //
      // context.Flare(context.root.GetDeterministicHashCode().ToString());
      //
      // int numSpaces = levelSuperstate.NumWalkableLocations(false);
      // if (depth == 0) {
      //   EmberDeepUnitsAndItems.FillWithUnits(
      //     game.rand,
      //     level,
      //     levelSuperstate,
      //   (loc) => !loc.Equals(entryLocation),
      //     /*numIrkling=*/ 20 * numSpaces / 200,
      //     /*numDraxling=*/ 7 * numSpaces / 200,
      //     /*numRavagianTrask=*/ 2 * numSpaces / 200,
      //     /*numBaug=*/ 4 * numSpaces / 200,
      //     /*numSpirient=*/ 1 * numSpaces / 200,
      //     /*numIrklingKing=*/ 0 * numSpaces / 200,
      //     /*numEmberfolk=*/ 0 * numSpaces / 200,
      //     /*numChronolisk=*/ 0 * numSpaces / 200,
      //     /*numMantisBombardier=*/ 0 * numSpaces / 200,
      //     /*numLightningTrask=*/ 0 * numSpaces / 200);
      // } else if (depth == 2) {
      //   EmberDeepUnitsAndItems.FillWithUnits(
      //     game.rand,
      //     level,
      //     levelSuperstate,
      //   (loc) => !loc.Equals(entryLocation),
      //     /*numIrkling=*/ 10 * numSpaces / 200,
      //     /*numDraxling=*/ 7 * numSpaces / 200,
      //     /*numRavagianTrask=*/ 3 * numSpaces / 200,
      //     /*numBaug=*/ 3 * numSpaces / 200,
      //     /*numSpirient=*/ 2 * numSpaces / 200,
      //     /*numIrklingKing=*/ 1 * numSpaces / 200,
      //     /*numEmberfolk=*/ 2 * numSpaces / 200,
      //     /*numChronolisk=*/ 1 * numSpaces / 200,
      //     /*numMantisBombardier=*/ 0 * numSpaces / 200,
      //     /*numLightningTrask=*/ 0 * numSpaces / 200);
      // } else if (depth == 4) {
      //   EmberDeepUnitsAndItems.FillWithUnits(
      //     game.rand,
      //     level,
      //     levelSuperstate,
      //   (loc) => !loc.Equals(entryLocation),
      //     /*numIrkling=*/ 4 * numSpaces / 200,
      //     /*numDraxling=*/ 4 * numSpaces / 200,
      //     /*numRavagianTrask=*/ 3 * numSpaces / 200,
      //     /*numBaug=*/ 2 * numSpaces / 200,
      //     /*numSpirient=*/ 1 * numSpaces / 200,
      //     /*numIrklingKing=*/ 2 * numSpaces / 200,
      //     /*numEmberfolk=*/ 3 * numSpaces / 200,
      //     /*numChronolisk=*/ 1 * numSpaces / 200,
      //     /*numMantisBombardier=*/ 1 * numSpaces / 200,
      //     /*numLightningTrask=*/ 0);
      // } else if (depth == 6) {
      //   EmberDeepUnitsAndItems.FillWithUnits(
      //     game.rand,
      //     level,
      //     levelSuperstate,
      //   (loc) => !loc.Equals(entryLocation),
      //     /*numIrkling=*/ 2 * numSpaces / 200,
      //     /*numDraxling=*/ 2 * numSpaces / 200,
      //     /*numRavagianTrask=*/ 3 * numSpaces / 200,
      //     /*numBaug=*/ 1 * numSpaces / 200,
      //     /*numSpirient=*/ 0 * numSpaces / 200,
      //     /*numIrklingKing=*/ 4 * numSpaces / 200,
      //     /*numEmberfolk=*/ 5 * numSpaces / 200,
      //     /*numChronolisk=*/ 3 * numSpaces / 200,
      //     /*numMantisBombardier=*/ 3 * numSpaces / 200,
      //     /*numLightningTrask=*/ 1);
      // }

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      levelSuperstate.Reconstruct(level);

      game.levels.Add(level);

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      levelRet = level;
      entryLocationRet = entryLocation;
    }

    public static string GetName(this JumpingCaveLevelController obj) {
      return "Cave";
    }

    public static bool ConsiderCornersAdjacent(this JumpingCaveLevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this JumpingCaveLevelController self,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        string triggerName) {
      if (self.depth == 0 && triggerName == "levelStart") {
        var locationsNextToPlayer = game.level.terrain.GetAdjacentExistingLocations(game.player.location, false);
        var hopToPossibilities = superstate.levelSuperstate.GetNRandomWalkableLocations(game.level.terrain, game.rand, 1, (loc) => locationsNextToPlayer.Contains(loc), true, true);
        if (hopToPossibilities.Count > 0) {
          Actions.Step(game, superstate, game.player, hopToPossibilities[0], true, false);
          game.player.WaitFor();
        }
        game.ShowAside("kylin", "I've made it to Ember Deep! Forward!");
      }
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this JumpingCaveLevelController obj,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      return new Atharia.Model.Void();
    }
  }
}
