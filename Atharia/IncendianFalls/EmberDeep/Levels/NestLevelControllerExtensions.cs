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
        out Location entryLocation,
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

      levelSuperstate = new LevelSuperstate(level);

      var geomancy =
        Vivifier.Vivify(level, levelSuperstate, Vivifier.ParseGeomancy(LEVEL));
      var (floors, walls) = GetFloorsAndNearbyWalls(level.terrain);

      foreach (var wall in walls) {
        level.terrain.tiles.Add(
          wall,
          level.root.EffectTerrainTileCreate(0, ITerrainTileComponentMutBunch.New(level.root)));
      }

      CellularAutomataTerrainGenerator.AddCircle(level.terrain, new Location(0, 0, 0), 16.0f);

      TerrainUtils.randify(game.rand, level.terrain, 2);

      for (int i = 0; i < 2; i++) {
        foreach (var floor in floors) {
          level.terrain.tiles[floor].elevation = 1;
        }
        foreach (var wall in walls) {
          level.terrain.tiles[wall].elevation = 0;
        }

        CellularAutomataTerrainGenerator.CellularAutomataModeIteration(level.terrain, considerCornersAdjacent);

        foreach (var floor in floors) {
          level.terrain.tiles[floor].elevation = 1;
        }
        foreach (var wall in walls) {
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
          var tile = game.root.EffectTerrainTileCreate(3, ITerrainTileComponentMutBunch.New(game.root));
          tile.components.Add(game.root.EffectCaveWallTTCCreate().AsITerrainTileComponent());
          level.terrain.tiles.Add(borderLocation, tile);
        }
      }

      game.levels.Add(level);

      level.controller = game.root.EffectNestLevelControllerCreate(level).AsILevelController();

      entryLocation = levelSuperstate.FindMarkerLocation("start");
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
        game.events.Add(
          new ShowOverlayEvent(
            100, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            0,// fadeInEnd
            7000, // fadeOutStart
            7000, // fadeOutEnd,
            "introLine1Done",

            "To undo it, I need to dive into Ember Deep, find the Emberdrift, and follow it until I find a similar black incendium artifact.",
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
        var hopTo = superstate.levelSuperstate.FindMarkerLocation("playerHop");
        Actions.Step(game, superstate, game.player, hopTo, true);
        game.events.Add(new WaitEvent(1000, "playerEntryHopDone").AsIGameEvent());
      }
      if (triggerName == "playerEntryHopDone") {
        game.events.Add(
          new ShowOverlayEvent(
            80, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            500,// fadeInEnd
            7000, // fadeOutStart
            7000, // fadeOutEnd,
            "objectiveMusingDone",

            "Finally, Ember Deep! I need to go deep...\n\nWhen embers fill the air, I've found the Emberdrift, and I'll follow it to find the black incendium I need.",
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
      if (triggerName == "objectiveMusingDone") {
        //var destination = superstate.levelSuperstate.FindSimplePresenceTriggerLocation("ambushTrigger");
        //var terrain = game.level.terrain;
        //var steps =
        //    AStarExplorer.Go(
        //        terrain.pattern,
        //        game.player.location,
        //        destination,
        //        game.level.ConsiderCornersAdjacent(),
        //        (Location from, Location to) => {
        //          return terrain.tiles.ContainsKey(to) &&
        //              terrain.tiles[to].IsWalkable() &&
        //              terrain.GetElevationDifference(from, to) <= 2;
        //        });
        //Asserts.Assert(steps.Count > 0);
        //superstate.navigatingState = new Superstate.NavigatingState(steps);

        var hopTo = superstate.levelSuperstate.FindSimplePresenceTriggerLocation("ambushTrigger");
        Actions.Step(game, superstate, game.player, hopTo, true);
        game.events.Add(new WaitEvent(1000, "reachedAmbush").AsIGameEvent());
      }
      if (triggerName == "reachedAmbush") {
        game.events.Add(
          new ShowOverlayEvent(
            80, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            500,// fadeInEnd
            4000, // fadeOutStart
            5000, // fadeOutEnd,
            "startSummons",

            "Seems quiet... my brother's journals described it as a lively place.",
            new Color(255, 64, 0, 255), // textColor
            1000, // textFadeInStartS
            2000, // textFadeInEndS
            4000, // textFadeOutStartS
            5000, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() { }))
          .AsIGameEvent());
      }
      if (triggerName == "startSummons") {
        game.events.Add(
          new FlyCameraEvent(
            superstate.levelSuperstate.FindMarkerLocation("cameraPanTo"),
            new Vec3(0, 8, 8),
            1000,
            "cameraDoneFlying")
          .AsIGameEvent());
      }
      if (triggerName == "cameraDoneFlying") {
        game.player.nextActionTime = game.level.time;
        var summonLocations = superstate.levelSuperstate.FindMarkersLocations("summon", 1);
        foreach (var summonLocation in summonLocations) {
          game.level.EnterUnit(
            superstate.levelSuperstate,
            summonLocation,
            game.level.time + 300,
            Irkling.Make(game.root));
        }
        game.events.Add(
          new ShowOverlayEvent(
            60, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            500,// fadeInEnd
            7000, // fadeOutStart
            7000, // fadeOutEnd,
            "",

            "Spoke too soon. An Irkling nest!\n\nWith chronomancy, I might be able to survive this!",
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
      //  game.events.Add(
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
-6 -6 4 1 Marker(summon)
-6 -6 7 1 Marker(summon)
-6 -5 4 1
-6 -5 6 1
-6 -5 7 1
-5 -6 3 1 Marker(summon)
-5 -6 4 1 Marker(summon)
-5 -6 5 1 Marker(summon)
-5 -6 6 1 Marker(summon)
-5 -6 7 1 Marker(summon)
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
-5 -4 7 1 Marker(summon)
-4 -5 3 1
-4 -5 4 1
-4 -5 5 1
-4 -5 6 1
-4 -5 7 1
-4 -4 0 1
-4 -4 1 1 Marker(cameraPanTo)
-4 -4 2 1
-4 -4 3 1
-4 -4 4 1
-4 -4 5 1
-4 -4 6 1 Marker(summon)
-4 -4 7 1
-4 -3 0 1 Marker(summon)
-4 -3 1 1 Marker(summon)
-4 -3 2 1 Marker(summon)
-3 -5 4 1
-3 -5 5 1
-3 -5 6 1
-3 -5 7 1
-3 -4 0 1
-3 -4 1 1
-3 -4 2 1
-3 -4 3 1 Trigger(ambushTrigger)
-3 -4 4 1
-3 -4 5 1
-3 -4 6 1
-3 -4 7 1
-3 -3 0 1
-3 -3 1 1 Marker(summon)
-3 -3 2 1
-3 -3 3 1
-2 -5 5 1
-2 -5 6 1
-2 -4 0 1
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
-1 -4 3 1
-1 -4 4 1
-1 -4 5 1
-1 -4 6 1
-1 -4 7 1
-1 -3 0 1
-1 -3 1 1
-1 -3 2 1
-1 -3 3 1
-1 -3 4 1
-1 -3 5 1
-1 -3 6 1
-1 -3 7 1
-1 3 3 1
-1 3 4 1
-1 3 5 1
-1 3 6 1
-1 3 7 1
-1 4 0 1
-1 4 1 1
0 -4 2 1
0 -4 3 1
0 -4 4 1
0 -4 5 1
0 -4 6 1
0 -4 7 1
0 -3 1 1
0 -2 0 1 Marker(summon)
0 -2 1 1
0 -2 2 1 Marker(summon)
0 -2 3 1 Marker(summon)
0 -2 4 1 Marker(summon)
0 4 1 1
1 -5 7 1
1 -4 2 1
1 -4 3 1
1 -4 4 1
1 -4 5 1
1 -4 6 1 Marker(playerHop)
1 -4 7 1 Marker(start)
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
