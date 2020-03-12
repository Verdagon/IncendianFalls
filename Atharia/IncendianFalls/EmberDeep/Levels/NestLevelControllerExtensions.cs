using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class NestLevelControllerExtensions {
    private static (SortedSet<Location>, SortedSet<Location>) GetFloorsAndNearbyWalls(Terrain terrain) {
      // This level contains floors where we want floors.
      // The stuff around the floors should be walls though.
      // Let's find the stuff around the floors.
      var floors = new SortedSet<Location>();
      foreach (var locationAndTile in terrain.tiles) {
        floors.Add(locationAndTile.Key);
      }
      // The spaces next to (but not including) floors are the "immediate walls".
      var immediateWalls = terrain.pattern.GetAdjacentLocations(floors, false, true);

      var floorsAndImmediateWalls = new SortedSet<Location>();
      SetUtils.AddAll(floorsAndImmediateWalls, floors);
      SetUtils.AddAll(floorsAndImmediateWalls, immediateWalls);

      // The spaces next to the near walls, which arent floors already, are the "far walls".
      var farWalls = terrain.pattern.GetAdjacentLocations(floorsAndImmediateWalls, false, true);

      var walls = new SortedSet<Location>();
      SetUtils.AddAll(walls, immediateWalls);
      SetUtils.AddAll(walls, farWalls);

      return (floors, walls);
    }

    public static void LoadLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocationRet,
        Game game,
        Superstate superstate,
        int depth) {
      bool considerCornersAdjacent = true;

      level =
        game.root.EffectLevelCreate(
          new Vec3(0, -8, 16),
          game.root.EffectTerrainCreate(
            PentagonPattern9.makePentagon9Pattern(),
            0.3f,
            game.root.EffectTerrainTileByLocationMutMapCreate()),
          game.root.EffectUnitMutSetCreate(),
          NullILevelController.Null,
          game.time);

      var geomancy =
        Vivifier.Vivify(level, Vivifier.ParseGeomancy(LEVEL));
      var (ambushAreaFloors, ambushAreaWalls) = GetFloorsAndNearbyWalls(level.terrain);

      foreach (var wall in ambushAreaWalls) {
        level.terrain.tiles.Add(
          wall,
          level.root.EffectTerrainTileCreate(
              game.root.EffectITerrainTileEventMutListCreate(), 0, ITerrainTileComponentMutBunch.New(level.root)));
      }

      CellularAutomataTerrainGenerator.AddCircle(level.terrain, new Location(0, 0, 0), 16.0f);

      TerrainUtils.randify(game.rand, level.terrain, 2);

      for (int i = 0; i < 2; i++) {
        foreach (var floor in ambushAreaFloors) {
          level.terrain.tiles[floor].elevation = 1;
        }
        foreach (var wall in ambushAreaWalls) {
          level.terrain.tiles[wall].elevation = 0;
        }

        CellularAutomataTerrainGenerator.CellularAutomataModeIteration(level.terrain, considerCornersAdjacent);

        foreach (var floor in ambushAreaFloors) {
          level.terrain.tiles[floor].elevation = 1;
        }
        foreach (var wall in ambushAreaWalls) {
          level.terrain.tiles[wall].elevation = 0;
        }
      }

      // This will remove the 0-tiles and connect things.
      CellularAutomataTerrainGenerator.FinishUp(game.rand, level.terrain, considerCornersAdjacent);

      // Now fill the floors with mud
      foreach (var locationAndTile in level.terrain.tiles) {
        locationAndTile.Value.components.Add(game.root.EffectMudTTCCreate().AsITerrainTileComponent());
      }

      // Now add borders
      var locations = new SortedSet<Location>();
      foreach (var locationAndTile in level.terrain.tiles) {
        locations.Add(locationAndTile.Key);
      }
      var borderLocations = level.terrain.pattern.GetAdjacentLocations(locations, false, true);
      foreach (var borderLocation in borderLocations) {
        if (!level.terrain.tiles.ContainsKey(borderLocation)) {
          var tile = game.root.EffectTerrainTileCreate(
              game.root.EffectITerrainTileEventMutListCreate(), 3, ITerrainTileComponentMutBunch.New(game.root));
          tile.components.Add(game.root.EffectCaveWallTTCCreate().AsITerrainTileComponent());
          level.terrain.tiles.Add(borderLocation, tile);
        }
      }

      levelSuperstate = new LevelSuperstate(level);

      var entryLocation = levelSuperstate.FindMarkerLocation("start");
      entryLocationRet = entryLocation;

      var exitLocation = levelSuperstate.FindMarkerLocation("exit");
      level.terrain.tiles[exitLocation].components.Add(
        level.root.EffectEmberDeepLevelLinkerTTCCreate(depth + 1).AsITerrainTileComponent());
      level.terrain.tiles[exitLocation].components.Add(
        level.root.EffectCaveTTCCreate().AsITerrainTileComponent());

      EmberDeepUnitsAndItems.PlaceRocks(game.rand, level, levelSuperstate);

      EmberDeepUnitsAndItems.PlaceItems(game.rand, level, levelSuperstate, (loc) => !loc.Equals(entryLocation), .01f, .01f);

      level.terrain.tiles[levelSuperstate.FindMarkerLocation("sword")].components.Add(
        level.root.EffectItemTTCCreate(
          level.root.EffectGlaiveCreate().AsIItem())
        .AsITerrainTileComponent());

      levelSuperstate.Reconstruct(level);
      levelSuperstate.AddNoUnitZone(entryLocation, 3);

      int numSpacesInRestOfLevel = levelSuperstate.NumWalkableLocations(false) - ambushAreaFloors.Count;
      EmberDeepUnitsAndItems.FillWithUnits(
        game.rand,
        level,
        levelSuperstate,
        (loc) => !ambushAreaFloors.Contains(loc),
        /*numIrkling=*/ 3 * numSpacesInRestOfLevel / 200,
        /*numDraxling=*/ 3 * numSpacesInRestOfLevel / 200,
        /*numRavagianTrask=*/ 3 * numSpacesInRestOfLevel / 200,
        /*numBaug=*/ 1 * numSpacesInRestOfLevel / 200,
        /*numSpirient=*/ 1 * numSpacesInRestOfLevel / 200,
        /*numIrklingKing=*/ 0 * numSpacesInRestOfLevel / 200,
        /*numEmberfolk=*/ 1 * numSpacesInRestOfLevel / 200,
        /*numChronolisk=*/ 0 * numSpacesInRestOfLevel / 200,
        /*numMantisBombardier=*/ 0 * numSpacesInRestOfLevel / 200,
        /*numLightningTrask=*/ 0 * numSpacesInRestOfLevel / 200);

      var summonLocations = levelSuperstate.FindMarkersLocations("summon", 1);
      EmberDeepUnitsAndItems.FillWithUnits(
        game.rand,
        level,
        levelSuperstate,
        (loc) => summonLocations.Contains(loc),
        /*numIrkling=*/ summonLocations.Count / 2,
        /*numDraxling=*/ summonLocations.Count / 4,
        /*numRavagianTrask=*/ summonLocations.Count / 8 - 1,
        /*numBaug=*/ summonLocations.Count / 8,
        /*numSpirient=*/ 1,
        /*numIrklingKing=*/ 0,
        /*numEmberfolk=*/ 0,
        /*numChronolisk=*/ 0,
        /*numMantisBombardier=*/ 0,
        /*numLightningTrask=*/ 0);

      levelSuperstate.Reconstruct(level);

      game.levels.Add(level);

      level.controller = game.root.EffectNestLevelControllerCreate(level).AsILevelController();
    }

    public static string GetName(this NestLevelController obj) {
      return "Nest";
    }

    public static bool ConsiderCornersAdjacent(this NestLevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this NestLevelController obj,
        Game game,
        Superstate superstate,
        string triggerName) {
      game.root.logger.Info("Got trigger: " + triggerName);

      if (triggerName == "levelStart") {
        var hopTo = superstate.levelSuperstate.FindMarkerLocation("playerHopTo");
        Actions.Step(game, superstate, game.player, hopTo, true, false);
        game.AddEvent(new WaitEvent(1000, "playerEntryHopDone").AsIGameEvent());
      }
      if (triggerName == "playerEntryHopDone") {
        game.AddEvent(
          new ShowOverlayEvent(
            30, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            300,// fadeInEnd
            2400, // fadeOutStart
            2700, // fadeOutEnd,
            "uhOhDone",

            "Uh oh...",
            new Color(255, 64, 0, 255), // textColor
            300, // textFadeInStartS
            600, // textFadeInEndS
            2100, // textFadeOutStartS
            2400, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() { }))
          .AsIGameEvent());
      }
      if (triggerName == "uhOhDone") {
        game.AddEvent(
          new FlyCameraEvent(
            superstate.levelSuperstate.FindMarkerLocation("cameraPanTo"),
            new Vec3(0, 8, 8),
            1000,
            "cameraReachedPanTo")
          .AsIGameEvent());
      }
      if (triggerName == "cameraReachedPanTo") {
        game.AddEvent(
          new WaitEvent(1000, "cameraWaitDone").AsIGameEvent());
      }
      if (triggerName == "cameraWaitDone") {
        game.AddEvent(
          new FlyCameraEvent(
            superstate.levelSuperstate.FindMarkerLocation("playerHopTo"),
            new Vec3(0, 8, 8),
            1000,
            "cameraDone")
          .AsIGameEvent());
      }
      if (triggerName == "cameraDone") {
        game.player.nextActionTime = game.level.time;

        game.AddEvent(
          new ShowOverlayEvent(
            40, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            300,// fadeInEnd
            3400, // fadeOutStart
            3700, // fadeOutEnd,
            "",

            "It's a nest of some sort!\n\nTime for some chronomancy!",
            new Color(255, 64, 0, 255), // textColor
            300, // textFadeInStartS
            600, // textFadeInEndS
            3100, // textFadeOutStartS
            3400, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() { }))
          .AsIGameEvent());
      }

      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this NestLevelController obj,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      //if (triggerName == "ambushTrigger") {
      //  game.AddEvent(
      //    new ShowOverlayEvent(
      //      80, // sizePercent
      //      new Color(0, 0, 0, 224), // backgroundColor
      //      0,// fadeInEnd
      //      4000, // fadeOutStart
      //      5000, // fadeOutEnd,
      //      "startSummons",

      //      "Seems quiet... my brother's journals described it as a lively place.",
      //      new Color(255, 64, 0, 255), // textColor
      //      1000, // textFadeInStartS
      //      2000, // textFadeInEndS
      //      4000, // textFadeOutStartS
      //      5000, // textFadeOutEndS
      //      true, // topAligned
      //      true, // leftAligned

      //      new ButtonImmList(new List<Button>() { }))
      //    .AsIGameEvent());
      //}
      return new Atharia.Model.Void();
    }

  private static string LEVEL = @"
-6 -6 2 1 Marker(summon)
-6 -6 4 1 Marker(summon) HealthPotion
-6 -6 7 1 Marker(summon)
-6 -5 4 1
-6 -5 6 1
-6 -5 7 1 HealthPotion
-5 -6 3 1 Marker(summon) HealthPotion
-5 -6 4 1 Marker(summon)
-5 -6 5 1 Marker(summon) ManaPotion
-5 -6 6 1 Marker(summon)
-5 -6 7 1 Marker(summon) ManaPotion
-5 -5 0 1
-5 -5 1 1
-5 -5 2 1
-5 -5 3 1
-5 -5 4 1
-5 -5 5 1
-5 -5 6 1
-5 -5 7 1
-5 -4 0 1
-5 -4 1 1
-5 -4 2 1
-5 -4 3 1
-5 -4 4 1
-5 -4 5 1
-5 -4 6 1
-5 -4 7 1 Marker(summon) HealthPotion
-4 -5 3 1
-4 -5 4 1
-4 -5 5 1
-4 -5 6 1 HealthPotion
-4 -5 7 1
-4 -4 0 1 Marker(playerHopTo)
-4 -4 1 1
-4 -4 2 1
-4 -4 3 1
-4 -4 4 1
-4 -4 5 1
-4 -4 6 1 Marker(summon)
-4 -4 7 1 HealthPotion
-4 -3 0 1 Marker(summon)
-4 -3 1 1 Marker(summon) ManaPotion
-4 -3 2 1 Marker(summon)
-3 -5 4 1 ManaPotion
-3 -5 5 1
-3 -5 6 1
-3 -5 7 1
-3 -4 0 1
-3 -4 1 1 Marker(start)
-3 -4 2 1
-3 -4 3 1 ManaPotion
-3 -4 4 1
-3 -4 5 1
-3 -4 6 1
-3 -4 7 1 ManaPotion
-3 -3 0 1
-3 -3 1 1 Marker(summon)
-3 -3 2 1 HealthPotion
-3 -3 3 1
-2 -5 5 1
-2 -5 6 1
-2 -4 0 1 ManaPotion
-2 -4 1 1
-2 -4 2 1
-2 -4 3 1
-2 -4 4 1
-2 -4 5 1
-2 -4 6 1
-2 -4 7 1
-2 -3 0 1
-2 -3 1 1
-2 -3 2 1
-2 -3 3 1
-2 3 2 1
-2 3 3 1
-2 3 4 1
-2 3 5 1
-2 3 6 1
-2 3 7 1 Marker(exit)
-1 -4 1 1
-1 -4 2 1
-1 -4 3 1 HealthPotion
-1 -4 4 1
-1 -4 5 1
-1 -4 6 1
-1 -4 7 1
-1 -3 0 1
-1 -3 1 1
-1 -3 2 1
-1 -3 3 1
-1 -3 4 1 HealthPotion
-1 -3 5 1
-1 -3 6 1 HealthPotion
-1 -3 7 1
-1 3 3 1
-1 3 4 1
-1 3 5 1
-1 3 6 1
-1 3 7 1
-1 4 0 1
-1 4 1 1
0 -4 2 1
0 -4 3 1 HealthPotion
0 -4 4 1
0 -4 5 1
0 -4 6 1
0 -4 7 1 HealthPotion
0 -3 1 1
0 -2 0 1 Marker(summon) ManaPotion
0 -2 1 1
0 -2 2 1 Marker(summon) ManaPotion
0 -2 3 1 Marker(summon)
0 -2 4 1 Marker(summon)
0 4 1 1
1 -5 7 1
1 -4 2 1
1 -4 3 1
1 -4 4 1 Marker(cameraPanTo)
1 -4 5 1
1 -4 6 1
1 -4 7 1 Marker(sword)
1 -3 0 1
1 -3 2 1
1 -3 4 1
1 -3 7 1
1 -2 3 1 Marker(summon)
1 -2 5 1 Marker(summon)
2 -5 2 1 Marker(summon)
2 -5 3 1 Marker(summon)
2 -5 4 1 Marker(summon)
2 -5 5 1 Marker(summon)
2 -5 6 1 Marker(summon)
2 -5 7 1 Marker(summon)
2 -4 0 1
2 -4 1 1
2 -4 2 1
2 -4 3 1
2 -4 4 1
2 -4 5 1
2 -4 6 1
2 -4 7 1
2 -3 0 1
2 -3 1 1
2 -3 2 1
2 -3 3 1
2 -3 5 1
2 -3 6 1
2 -2 0 1
3 -5 5 1 Marker(summon)
3 -3 0 1
3 -3 1 1
3 -3 2 1
3 -3 3 1
3 -2 1 1
3 -2 2 1 Marker(summon)
3 -2 3 1 Marker(summon)
3 -2 4 1 Marker(summon)
3 -2 5 1 Marker(summon)
3 -2 6 1 Marker(summon)
3 -2 7 1 Marker(summon)
4 -3 1 1
4 -3 2 1 Marker(summon)
4 -3 3 1
4 -3 4 1 Marker(summon)
4 -3 7 1 Marker(summon)
4 -2 3 1 Marker(summon)
4 -2 5 1 Marker(summon)
5 -3 3 1 Marker(summon)
5 -3 4 1 Marker(summon)
5 -3 5 1 Marker(summon)
5 -3 6 1 Marker(summon)
5 -3 7 1 Marker(summon)
";
}
}
