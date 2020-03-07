using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class CaveLevelControllerExtensions {
    public static void LoadLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocationRet,
        Game game,
        Superstate superstate,
        int depth) {
      bool considerCornersAdjacent = false;

      var terrain =
        CellularAutomataTerrainGenerator.Generate(
          game.root,
          PentagonPattern9.makePentagon9Pattern(),
          game.rand,
          considerCornersAdjacent,
          8.0f);
      foreach (var locationAndTile in terrain.tiles) {
        locationAndTile.Value.components.Add(game.root.EffectMudTTCCreate().AsITerrainTileComponent());
      }

      var locations = new SortedSet<Location>();
      foreach (var locationAndTile in terrain.tiles) {
        locations.Add(locationAndTile.Key);
      }
      var borderLocations = terrain.pattern.GetAdjacentLocations(locations, false, true);
      foreach (var borderLocation in borderLocations) {
        if (!terrain.tiles.ContainsKey(borderLocation)) {
          var tile = game.root.EffectTerrainTileCreate(3, ITerrainTileComponentMutBunch.New(game.root));
          tile.components.Add(game.root.EffectCaveWallTTCCreate().AsITerrainTileComponent());
          terrain.tiles.Add(borderLocation, tile);
        }
      }

      level =
          game.root.EffectLevelCreate(
          new Vec3(0, -8, 16),
              terrain,
              game.root.EffectUnitMutSetCreate(),
              NullILevelController.Null,
              game.time);
      levelSuperstate = new LevelSuperstate(level);

      var entryAndExitLocations = levelSuperstate.GetNRandomWalkableLocations(level.terrain, game.rand, 2,
              (loc) => true, false, false);
      var entryLocation = entryAndExitLocations[0];
      var exitLocation = entryAndExitLocations[1];
      level.terrain.tiles[exitLocation].components.Add(
        game.root.EffectCaveTTCCreate().AsITerrainTileComponent());
      level.terrain.tiles[exitLocation].components.Add(
        game.root.EffectEmberDeepLevelLinkerTTCCreate(depth + 1).AsITerrainTileComponent());

      EmberDeepUnitsAndItems.PlaceRocks(game.rand, level, levelSuperstate);

      level.controller = game.root.EffectCaveLevelControllerCreate(level).AsILevelController();

      if (depth < 2) {
        EmberDeepUnitsAndItems.PlaceItems(game.rand, level, levelSuperstate, (loc) => !loc.Equals(entryLocation), .008f, 0f);
      } else {
        EmberDeepUnitsAndItems.PlaceItems(game.rand, level, levelSuperstate, (loc) => !loc.Equals(entryLocation), .008f, .005f);
      }

      if (depth < 2) {
        EmberDeepUnitsAndItems.FillWithUnits(
          game,
          level,
          levelSuperstate,
          (loc) => true,
          /*numIrkling=*/ 20,
          /*numDraxling=*/ 7,
          /*numRavagianTrask=*/ 2,
          /*numBaug=*/ 4,
          /*numSpirient=*/ 1,
          /*numIrklingKing=*/ 0,
          /*numEmberfolk=*/ 0,
          /*numChronolisk=*/ 0,
          /*numMantisBombardier=*/ 0,
          /*numLightningTrask=*/ 0);
      } else if (depth < 4) {
        EmberDeepUnitsAndItems.FillWithUnits(
          game,
          level,
          levelSuperstate,
          (loc) => true,
          /*numIrkling=*/ 10,
          /*numDraxling=*/ 7,
          /*numRavagianTrask=*/ 3,
          /*numBaug=*/ 3,
          /*numSpirient=*/ 2,
          /*numIrklingKing=*/ 1,
          /*numEmberfolk=*/ 2,
          /*numChronolisk=*/ 1,
          /*numMantisBombardier=*/ 0,
          /*numLightningTrask=*/ 0);
      } else if (depth < 6) {
        EmberDeepUnitsAndItems.FillWithUnits(
          game,
          level,
          levelSuperstate,
          (loc) => true,
          /*numIrkling=*/ 4,
          /*numDraxling=*/ 4,
          /*numRavagianTrask=*/ 3,
          /*numBaug=*/ 2,
          /*numSpirient=*/ 1,
          /*numIrklingKing=*/ 2,
          /*numEmberfolk=*/ 3,
          /*numChronolisk=*/ 1,
          /*numMantisBombardier=*/ 1,
          /*numLightningTrask=*/ 0);
      } else if (depth < 8) {
        EmberDeepUnitsAndItems.FillWithUnits(
          game,
          level,
          levelSuperstate,
          (loc) => true,
          /*numIrkling=*/ 2,
          /*numDraxling=*/ 2,
          /*numRavagianTrask=*/ 3,
          /*numBaug=*/ 1,
          /*numSpirient=*/ 0,
          /*numIrklingKing=*/ 4,
          /*numEmberfolk=*/ 5,
          /*numChronolisk=*/ 3,
          /*numMantisBombardier=*/ 3,
          /*numLightningTrask=*/ 0);
      }

      game.levels.Add(level);

      entryLocationRet = entryLocation;
    }

    public static string GetName(this CaveLevelController obj) {
      return "Cave";
    }

    public static bool ConsiderCornersAdjacent(this CaveLevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this CaveLevelController obj,
        Game game,
        Superstate superstate,
        string triggerName) {
      if (triggerName == "firstLevelStart") {
        game.events.Add(
          new ShowOverlayEvent(
            100, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            0,// fadeInEnd
            7000, // fadeOutStart
            7000, // fadeOutEnd,
            "introLine1Done",

            "To undo my brother's stasis, I need to follow the caves until I find something made of black incendium.",
            new Color(255, 64, 0, 255), // textColor
            1000, // textFadeInStartS
            2000, // textFadeInEndS
            6000, // textFadeOutStartS
            7000, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() { }))
          .AsIGameEvent());
      }
      if (triggerName == "introLine1Done") {
        var locationsNextToPlayer = game.level.terrain.GetAdjacentExistingLocations(game.player.location, false);
        var hopToPossibilities = superstate.levelSuperstate.GetNRandomWalkableLocations(game.level.terrain, game.rand, 1, (loc) => locationsNextToPlayer.Contains(loc), true, true);
        if (hopToPossibilities.Count > 0) {
          Actions.Step(game, superstate, game.player, hopToPossibilities[0], true);
        }
        game.events.Add(new WaitEvent(1000, "playerEntryHopDone").AsIGameEvent());
      }
      if (triggerName == "playerEntryHopDone") {
        game.events.Add(
          new ShowOverlayEvent(
            50, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            500,// fadeInEnd
            3000, // fadeOutStart
            3500, // fadeOutEnd,
            "objectiveMusingDone",

            "Let's go!",
            new Color(255, 64, 0, 255), // textColor
            500, // textFadeInStartS
            1000, // textFadeInEndS
            2500, // textFadeOutStartS
            3000, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() { }))
          .AsIGameEvent());
      }
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this CaveLevelController obj,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      return new Atharia.Model.Void();
    }
  }
}
