using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class SotaventoLevelControllerExtensions {
    public static void LoadLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocation,
        out Location exitLocation,
        Game game) {
      level =
        game.root.EffectLevelCreate(
          new Vec3(0, -8, 16),
          game.root.EffectTerrainCreate(
            SquarePattern.MakeSquarePattern(),
            0.3f,
            game.root.EffectTerrainTileByLocationMutMapCreate()),
          game.root.EffectUnitMutSetCreate(),
          NullILevelController.Null,
          game.time);

      var geomancy =
        Vivifier.Vivify(level, Vivifier.ParseGeomancy(LEVEL));

      levelSuperstate = new LevelSuperstate(level);

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

      level.controller = game.root.EffectSotaventoLevelControllerCreate(level).AsILevelController();

      game.levels.Add(level);

      entryLocation = levelSuperstate.FindMarkerLocation("start");
      exitLocation = levelSuperstate.FindMarkerLocation("retreatTo");
    }

    public static string GetName(this SotaventoLevelController obj) {
      return "Sotavento";
    }

    public static bool ConsiderCornersAdjacent(this SotaventoLevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this SotaventoLevelController obj,
        Game game,
        Superstate superstate,
        string triggerName) {
      game.root.logger.Info("Got trigger: " + triggerName);

      if (triggerName == "levelStart") {
        game.AddEvent(
          new ShowOverlayEvent(

            "My brother was an explorer.\n\nOne of the only people to explore Ember Deep and survive, thanks to his mastery of chronomancy.",

            "dramatic",
            "kylin",
          // We tell the view that this isnt the first in the series because we dont want it to fade in.
          // It's a bit of a hack. We could instead throw in another boolean to say "but its the first in the
          // entire game" or something...
          // Or, we can think of it like we've already started the sequence in the intro screen, we would
          // just need to add an auto-closing fade-out from there. Maybe with some text.
          false,
          false,
          false,

            new ButtonImmList(new List<Button>() { new Button("...", "introLine1Done") }))
          .AsIGameEvent());
      }
      if (triggerName == "introLine1Done") {
        game.AddEvent(
          new ShowOverlayEvent(
            "\"Ember Deep is a dangerous place, ravaged by time magic since millenia ago.\n\nPeople fear it, and call it evil. I don't think it is.\n\nPast the danger, there are wonders to discover down there, answers to the deeper mysteries of our realm.\"",
            "dramatic",
            "kylinBrother",
          false,
          false,
          false,
            new ButtonImmList(new List<Button>() { new Button("...", "introLine2Done") }))
          .AsIGameEvent());
      }
      if (triggerName == "introLine2Done") {
        game.AddEvent(
          new ShowOverlayEvent(
            "Seven years ago, when the terrible Ravashrike attacked our town, he stood against it.",
            "dramatic",
          "kylin",
          false,
          true,
          false,
            new ButtonImmList(new List<Button>() { new Button("...", "introLine3Done") }))
          .AsIGameEvent());
      }
      if (triggerName == "introLine3Done") {
        var ravashrikeHopTo1 = superstate.levelSuperstate.FindMarkerLocation("ravashrikeHopTo1");
        game.AddEvent(new FlyCameraEvent(ravashrikeHopTo1, new Vec3(5, 5, 5), 300, "flyCameraToRavashrikeDone").AsIGameEvent());
      }
      if (triggerName == "flyCameraToRavashrikeDone") {
        game.AddEvent(new WaitEvent(true, 500, "wait1Done").AsIGameEvent());
      }
      if (triggerName == "wait1Done") {
        var chronomancer = superstate.levelSuperstate.FindLiveUnit("Chronomancer");
        game.AddEvent(new FlyCameraEvent(chronomancer.location, new Vec3(5, 5, 5), 300, "flyToBrother1Done").AsIGameEvent());
      }
      if (triggerName == "flyToBrother1Done") {
        game.AddEvent(new SetGameSpeedEvent(40).AsIGameEvent());
        var ravashrike = superstate.levelSuperstate.FindLiveUnit("Ravashrike");
        var ravashrikeHopTo1 = superstate.levelSuperstate.FindMarkerLocation("ravashrikeHopTo1");
        Actions.Step(game, superstate, ravashrike, ravashrikeHopTo1, true, false);
        game.AddEvent(new WaitEvent(true, 1000, "fire1").AsIGameEvent());
      }
      if (triggerName == "fire1") {
        var ravashrike = superstate.levelSuperstate.FindLiveUnit("Ravashrike");
        var chronomancer = superstate.levelSuperstate.FindLiveUnit("Chronomancer");
        Actions.Fire(game, superstate, chronomancer, ravashrike);
        game.AddEvent(new WaitEvent(true, 500, "step2").AsIGameEvent());
      }
      if (triggerName == "step2") {
        var ravashrike = superstate.levelSuperstate.FindLiveUnit("Ravashrike");
        var ravashrikeHopTo1 = superstate.levelSuperstate.FindMarkerLocation("ravashrikeHopTo2");
        Actions.Step(game, superstate, ravashrike, ravashrikeHopTo1, true, false);
        game.AddEvent(new WaitEvent(true, 1000, "fire2").AsIGameEvent());
      }
      if (triggerName == "fire2") {
        var ravashrike = superstate.levelSuperstate.FindLiveUnit("Ravashrike");
        var chronomancer = superstate.levelSuperstate.FindLiveUnit("Chronomancer");
        Actions.Fire(game, superstate, chronomancer, ravashrike);
        game.AddEvent(new WaitEvent(true, 500, "step3").AsIGameEvent());
      }
      if (triggerName == "step3") {
        var ravashrike = superstate.levelSuperstate.FindLiveUnit("Ravashrike");
        var ravashrikeHopTo1 = superstate.levelSuperstate.FindMarkerLocation("ravashrikeHopTo3");
        Actions.Step(game, superstate, ravashrike, ravashrikeHopTo1, true, false);
        game.AddEvent(new WaitEvent(true, 1000, "fire3").AsIGameEvent());
      }
      if (triggerName == "fire3") {
        var ravashrike = superstate.levelSuperstate.FindLiveUnit("Ravashrike");
        var chronomancer = superstate.levelSuperstate.FindLiveUnit("Chronomancer");
        Actions.Fire(game, superstate, chronomancer, ravashrike);
        game.AddEvent(new WaitEvent(true, 500, "ravashrikeAttack").AsIGameEvent());
      }
      if (triggerName == "ravashrikeAttack") {
        var ravashrike = superstate.levelSuperstate.FindLiveUnit("Ravashrike");
        var chronomancer = superstate.levelSuperstate.FindLiveUnit("Chronomancer");
        var retreatTo = superstate.levelSuperstate.FindMarkerLocation("retreatTo");
        Actions.Bump(game, superstate, ravashrike, chronomancer, 1.0f, true);
        Actions.Step(game, superstate, chronomancer, retreatTo, true, false);
        game.AddEvent(new WaitEvent(true, 1000, "ravashrikeAttackDone").AsIGameEvent());
      }
      if (triggerName == "ravashrikeAttackDone") {
        game.AddEvent(
          new ShowOverlayEvent(
            "\"I can't fight it! Time to do something desperate...\"",
            "normal",
            "kylinBrother",
          true,
          true,
          false,
            new ButtonImmList(new List<Button>() { new Button("...", "realizationDone") }))
          .AsIGameEvent());
      }
      if (triggerName == "realizationDone") {
        //Actions.Stasis(game, superstate, chronomancer, ravashrike);
        game.AddEvent(
          new ShowOverlayEvent(
            "He used a black incendium staff to cast an Eternal Stasis on the Ravashrike...\n\nBut he caught himself in it too.",
            "dramatic",
            "kylin",
          true,
          false,
          false,
            new ButtonImmList(new List<Button>() { new Button("...", "cinematicFadeOut") }))
          .AsIGameEvent());
      }
      if (triggerName == "cinematicFadeOut") {
        game.AddEvent(
          new ShowOverlayEvent(
            "Now, my journey begins.",
            "dramatic",
            "kylin",
          false,
          false,
          true,
            new ButtonImmList(new List<Button>() { new Button("...", "cinematicDone") }))
          .AsIGameEvent());
      }
      if (triggerName == "cinematicDone") {
        game.AddEvent(new SetGameSpeedEvent(100).AsIGameEvent());
        var linkLocation = game.player.location;
        game.level.terrain.tiles[linkLocation].components.GetOnlyLevelLinkTTCOrNull()
          .Interact(game, superstate, game.player, linkLocation);
      }
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this SotaventoLevelController obj,
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
