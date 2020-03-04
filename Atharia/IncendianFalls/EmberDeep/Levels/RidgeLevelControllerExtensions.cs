using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class RidgeLevelControllerExtensions {
    public static void LoadLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocation,
        out Location exitLocation,
        Root root) {
      level =
        root.EffectLevelCreate(
          new Vec3(0, -8, 16),
          root.EffectTerrainCreate(
            SquarePattern.MakeSquarePattern(),
            0.3f,
            root.EffectTerrainTileByLocationMutMapCreate()),
          root.EffectUnitMutSetCreate(),
          NullILevelController.Null,
          0);

      levelSuperstate = new LevelSuperstate(level);

      var geomancy =
        Vivifier.Vivify(level, levelSuperstate, Vivifier.ParseGeomancy(LEVEL));

      var ravashrike = levelSuperstate.FindLiveUnit("Ravashrike");
      var components = new List<IUnitComponent>();
      foreach (var component in ravashrike.components) {
        components.Add(component);
      }
      ravashrike.components.Clear();
      foreach (var component in components) {
        component.Destruct();
      }

      if (geomancy.Count > 0) {
        Asserts.Assert(false, Vivifier.PrintMembers(geomancy));
      }

      level.controller = root.EffectRidgeLevelControllerCreate(level).AsILevelController();

      entryLocation = levelSuperstate.FindMarker("start");
      exitLocation = levelSuperstate.FindMarker("retreatTo");
    }

    public static string GetName(this RidgeLevelController obj) {
      return "Ridge";
    }

    public static bool ConsiderCornersAdjacent(this RidgeLevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this RidgeLevelController obj,
        Game game,
        Superstate superstate,
        string triggerName) {
      if (triggerName == "levelStart") {
        game.events.Add(
          new ShowOverlayEvent(
            100, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            0,// fadeInEnd
            7000, // fadeOutStart
            7000, // fadeOutEnd,
            "introLine1Done",

            "My brother was an explorer.\n\nOne of the only people to explore Ember Deep and survive, thanks to his mastery of chronomancy.",
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
        game.events.Add(
          new ShowOverlayEvent(
            100, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            0, // fadeInEnd
            10000, // fadeOutStart
            10000, // fadeOutEnd,
            "introLine2Done",

            "\"Ember Deep is a dangerous place, ravaged by time magic since millenia ago.\n\nPeople fear it, and call it evil. I don't think it is.\n\nPast the danger, there are wonders to discover down there, answers to the deeper mysteries of our realm.\"",
            new Color(64, 192, 255, 255), // textColor
            0, // textFadeInStartS
            1000, // textFadeInEndS
            9000, // textFadeOutStartS
            10000, // textFadeOutEndS
            true, // topAligned
            false, // leftAligned

            new ButtonImmList(new List<Button>() { }))
          .AsIGameEvent());
      }
      if (triggerName == "introLine2Done") {
        game.events.Add(
          new ShowOverlayEvent(
            100, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            0, // fadeInEnd
            5000, // fadeOutStart
            6000, // fadeOutEnd,
            "introLine3Done",

            "Seven years ago, when the terrible Ravashrike attacked our town, he stood against it.",
            new Color(255, 64, 0, 255), // textColor
            0, // textFadeInStartS
            1000, // textFadeInEndS
            4000, // textFadeOutStartS
            5000, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() { }))
          .AsIGameEvent());
      }
      if (triggerName == "introLine3Done") {
        var ravashrikeHopTo1 = superstate.levelSuperstate.FindMarker("ravashrikeHopTo1");
        game.events.Add(new FlyCameraEvent(ravashrikeHopTo1, new Vec3(5, 5, 5), 300, "flyCameraToRavashrikeDone").AsIGameEvent());
      }
      if (triggerName == "flyCameraToRavashrikeDone") {
        game.events.Add(new WaitEvent(500, "wait1Done").AsIGameEvent());
      }
      if (triggerName == "wait1Done") {
        var chronomancer = superstate.levelSuperstate.FindLiveUnit("Chronomancer");
        game.events.Add(new FlyCameraEvent(chronomancer.location, new Vec3(5, 5, 5), 300, "flyToBrother1Done").AsIGameEvent());
      }
      if (triggerName == "flyToBrother1Done") {
        game.events.Add(new SetGameSpeedEvent(40).AsIGameEvent());
        var ravashrike = superstate.levelSuperstate.FindLiveUnit("Ravashrike");
        var ravashrikeHopTo1 = superstate.levelSuperstate.FindMarker("ravashrikeHopTo1");
        Actions.Step(game, superstate, ravashrike, ravashrikeHopTo1, true);
        game.events.Add(new WaitEvent(1000, "fire1").AsIGameEvent());
      }
      if (triggerName == "fire1") {
        var ravashrike = superstate.levelSuperstate.FindLiveUnit("Ravashrike");
        var chronomancer = superstate.levelSuperstate.FindLiveUnit("Chronomancer");
        Actions.Fire(game, superstate, chronomancer, ravashrike);
        game.events.Add(new WaitEvent(500, "step2").AsIGameEvent());
      }
      if (triggerName == "step2") {
        var ravashrike = superstate.levelSuperstate.FindLiveUnit("Ravashrike");
        var ravashrikeHopTo1 = superstate.levelSuperstate.FindMarker("ravashrikeHopTo2");
        Actions.Step(game, superstate, ravashrike, ravashrikeHopTo1, true);
        game.events.Add(new WaitEvent(1000, "fire2").AsIGameEvent());
      }
      if (triggerName == "fire2") {
        var ravashrike = superstate.levelSuperstate.FindLiveUnit("Ravashrike");
        var chronomancer = superstate.levelSuperstate.FindLiveUnit("Chronomancer");
        Actions.Fire(game, superstate, chronomancer, ravashrike);
        game.events.Add(new WaitEvent(500, "step3").AsIGameEvent());
      }
      if (triggerName == "step3") {
        var ravashrike = superstate.levelSuperstate.FindLiveUnit("Ravashrike");
        var ravashrikeHopTo1 = superstate.levelSuperstate.FindMarker("ravashrikeHopTo3");
        Actions.Step(game, superstate, ravashrike, ravashrikeHopTo1, true);
        game.events.Add(new WaitEvent(1000, "fire3").AsIGameEvent());
      }
      if (triggerName == "fire3") {
        var ravashrike = superstate.levelSuperstate.FindLiveUnit("Ravashrike");
        var chronomancer = superstate.levelSuperstate.FindLiveUnit("Chronomancer");
        Actions.Fire(game, superstate, chronomancer, ravashrike);
        game.events.Add(new WaitEvent(500, "ravashrikeAttack").AsIGameEvent());
      }
      if (triggerName == "ravashrikeAttack") {
        var ravashrike = superstate.levelSuperstate.FindLiveUnit("Ravashrike");
        var chronomancer = superstate.levelSuperstate.FindLiveUnit("Chronomancer");
        var retreatTo = superstate.levelSuperstate.FindMarker("retreatTo");
        Actions.Bump(game, superstate, ravashrike, chronomancer, 1.0f, true);
        Actions.Step(game, superstate, chronomancer, retreatTo, true);
        game.events.Add(new WaitEvent(1000, "ravashrikeAttackDone").AsIGameEvent());
      }
      if (triggerName == "ravashrikeAttackDone") {
        game.events.Add(
          new ShowOverlayEvent(
            50, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            500, // fadeInEnd
            3000, // fadeOutStart
            4000, // fadeOutEnd,
            "realizationDone",

            "\"I can't fight it! Time to do something desperate...\"",
            new Color(64, 192, 255, 255), // textColor
            0, // textFadeInStartS
            1000, // textFadeInEndS
            3000, // textFadeOutStartS
            4000, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() { }))
          .AsIGameEvent());
      }
      if (triggerName == "realizationDone") {
        //Actions.Stasis(game, superstate, chronomancer, ravashrike);
        game.events.Add(
          new ShowOverlayEvent(
            100, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            1000, // fadeInEnd
            6000, // fadeOutStart
            7000, // fadeOutEnd,
            "cinematicDone", // end trigger

            "He cast an Eternal Stasis on the Ravashrike... but he caught himself in it too.",
            new Color(255, 64, 0, 255), // textColor
            1000, // textFadeInStartS
            2000, // textFadeInEndS
            5000, // textFadeOutStartS
            6000, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() { }))
          .AsIGameEvent());
      }
      if (triggerName == "cinematicDone") {
        game.events.Add(new SetGameSpeedEvent(100).AsIGameEvent());
        var linkLocation = game.player.location;
        game.level.terrain.tiles[linkLocation].components.GetOnlyLevelLinkTTCOrNull()
          .Interact(game, superstate, game.player, linkLocation);
      }
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this RidgeLevelController obj,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      return new Atharia.Model.Void();
    }

    private static string LEVEL = @"
-1 -2 0 7 Mud
-1 -1 0 7 Mud
-1 0 0 11 Floor
-1 1 0 11 Floor
-1 2 0 11 Floor
-1 3 0 11 Floor
-1 4 0 10 Mud
-1 5 0 10 Mud Rocks
-1 6 0 11 Mud Grass
-1 7 0 11 Mud Tree
-1 8 0 12 Mud Floor
-1 9 0 13 Mud Floor
-1 10 0 13 Mud Floor
-1 11 0 12 Mud Grass
0 -2 0 6 Mud Rocks
0 -1 0 7 Mud
0 0 0 11 Floor
0 1 0 11 Floor
0 2 0 11 Floor
0 3 0 11 Floor
0 4 0 9 Mud Rocks
0 5 0 9 Mud Rocks
0 6 0 11 Mud
0 7 0 11 Mud Grass
0 8 0 13 Mud Floor
0 9 0 13 Mud Floor
0 10 0 13 Mud Floor
0 11 0 11 Mud Grass
1 -2 0 5 Mud Rocks
1 -1 0 7 Mud Rocks
1 0 0 7 Mud
1 1 0 7 Mud Rocks
1 2 0 7 Mud Rocks
1 3 0 8 Mud
1 4 0 8 Mud
1 5 0 8 Mud
1 6 0 8 Mud Ravashrike
1 7 0 9 Mud Rocks
1 8 0 9 Mud
1 9 0 9 Mud Grass
1 10 0 9 Mud Rocks
1 11 0 10 Mud
2 -2 0 3 Mud Grass
2 -1 0 6 Floor
2 0 0 6 Floor
2 1 0 6 Dirt
2 2 0 6 Mud Rocks
2 3 0 6 Mud
2 4 0 7 Mud Grass
2 5 0 7 Mud
2 6 0 7 Mud
2 7 0 5 Mud Dirt
2 8 0 5 Water
2 9 0 7 Mud
2 10 0 7 Mud
2 11 0 8 Mud
3 -2 0 3 Mud
3 -1 0 6 Floor
3 0 0 6 Floor
3 1 0 6 Floor
3 2 0 3 Mud
3 3 0 3 Mud Water
3 4 0 4 Mud Water
3 5 0 6 Dirt
3 6 0 6 Dirt Marker(ravashrikeHopTo1)
3 7 0 5 Water
3 8 0 5 Water
3 9 0 8 Mud Grass
3 10 0 9 Mud Rocks
3 11 0 9 Mud
4 -2 0 3 Mud Grass
4 -1 0 2 Mud Rocks
4 0 0 2 Mud Rocks
4 1 0 4 Mud
4 2 0 3 Mud Water
4 3 0 3 Mud Water
4 4 0 4 Mud Water
4 5 0 6 Dirt
4 6 0 6 Dirt Marker(ravashrikeHopTo2)
4 7 0 5 Water
4 8 0 5 Mud Dirt
4 9 0 8 Mud
4 10 0 9 Mud Grass
4 11 0 9 Mud Grass
5 -2 0 1 Mud Grass
5 -1 0 1 Mud Rocks
5 0 0 2 Mud
5 1 0 3 Mud
5 2 0 1 Mud Water
5 3 0 1 Mud Water
5 4 0 5 Mud Grass
5 5 0 6 Dirt
5 6 0 6 Grass
5 7 0 6 Grass
5 8 0 6 Mud Rocks
5 9 0 7 Mud
5 10 0 8 Mud Grass
5 11 0 9 Mud
6 -2 0 1 Mud
6 -1 0 1 Mud
6 0 0 1 Mud Water
6 1 0 1 Mud Water
6 2 0 1 Mud Water
6 3 0 1 Mud Water
6 4 0 4 Dirt
6 5 0 5 Mud Rocks Marker(ravashrikeHopTo3)
6 6 0 6 Dirt
6 7 0 6 Grass
6 8 0 6 Grass
6 9 0 7 Grass
6 10 0 8 Mud
6 11 0 9 Mud
7 -2 0 1 Mud Water
7 -1 0 1 Mud Water
7 0 0 1 Mud
7 1 0 1 Mud Water
7 2 0 1 Mud
7 3 0 2 Mud Grass
7 4 0 3 Mud Rocks
7 5 0 5 Mud Grass Marker(start)
7 6 0 5 Mud Grass
7 7 0 5 Mud
7 8 0 6 Grass
7 9 0 6 Mud Rocks
7 10 0 10 Floor
7 11 0 10 Floor
8 -2 0 1 Mud Water
8 -1 0 1 Mud Water
8 0 0 1 Mud Water
8 1 0 1 Mud Water
8 2 0 1 Mud
8 3 0 2 Mud Dirt
8 4 0 2 Mud Grass Marker(retreatTo)
8 5 0 5 Mud Grass
8 6 0 5 Mud Grass
8 7 0 5 Mud
8 8 0 6 Mud
8 9 0 6 Mud Rocks
8 10 0 10 Floor
8 11 0 10 Floor
9 -2 0 1 Mud Water
9 -1 0 1 Mud Water
9 0 0 1 Mud Rocks
9 1 0 1 Mud
9 2 0 1 Mud Grass
9 3 0 1 Mud Rocks
9 4 0 2 Mud Dirt
9 5 0 6 Floor
9 6 0 6 Floor
9 7 0 5 Mud
9 8 0 5 Mud
9 9 0 6 Mud Rocks
9 10 0 10 Floor
9 11 0 10 Floor
10 -2 0 1 Mud Water
10 -1 0 1 Mud Rocks
10 0 0 1 Mud Grass
10 1 0 1 Mud Grass
10 2 0 1 Mud
10 3 0 1 Mud
10 4 0 1 Mud Grass
10 5 0 6 Floor
10 6 0 6 Floor
10 7 0 6 Floor
10 8 0 5 Mud Grass
10 9 0 5 Mud
10 10 0 6 Mud
10 11 0 6 Mud Grass
11 -2 0 1 Mud
11 -1 0 1 Mud Grass
11 0 0 1 Mud
11 1 0 1 Mud Rocks
11 2 0 1 Dirt
11 3 0 1 Mud Grass
11 4 0 1 Mud Grass
11 5 0 1 Mud
11 6 0 1 Mud Rocks
11 7 0 2 Mud Rocks
11 8 0 3 Mud Rocks
11 9 0 4 Mud
11 10 0 5 Mud Rocks
11 11 0 6 Mud Dirt

";
  }
}
