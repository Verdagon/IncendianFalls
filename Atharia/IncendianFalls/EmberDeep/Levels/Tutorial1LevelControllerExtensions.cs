using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class Tutorial1LevelControllerExtensions {
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
            PentagonPattern9.makePentagon9Pattern(),
            0.3f,
            game.root.EffectTerrainTileByLocationMutMapCreate()),
          game.root.EffectUnitMutSetCreate(),
          NullILevelController.Null,
          game.time);

      var geomancy =
        Vivifier.Vivify(level, Vivifier.ParseGeomancy(LEVEL));

      if (geomancy.Count > 0) {
        Asserts.Assert(false, Vivifier.PrintMembers(geomancy));
      }

      levelSuperstate = new LevelSuperstate(level);

      level.controller = game.root.EffectTutorial1LevelControllerCreate(level).AsILevelController();

      game.levels.Add(level);

      entryLocation = levelSuperstate.FindMarkerLocation("start");
      exitLocation = levelSuperstate.FindMarkerLocation("exit");
    }

    public static string GetName(this Tutorial1LevelController obj) {
      return "Tutorial";
    }

    public static bool ConsiderCornersAdjacent(this Tutorial1LevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this Tutorial1LevelController obj,
        Game game,
        Superstate superstate,
        string triggerName) {
      game.root.logger.Error("Got simple trigger: " + triggerName);

      if (triggerName == "levelStart") {
        var sorcerous = game.player.components.GetOnlySorcerousUCOrNull();
        if (sorcerous.Exists()) {
          sorcerous.mp = sorcerous.maxMp;
        }
        game.player.hp = game.player.maxHp;
        game.events.Add(
          new ShowOverlayEvent(
            100, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            0000, // fadeInEnd
            0000, // fadeOutStart
            1000, // fadeOutEnd,
            "showTitle", // end trigger

            "",
            new Color(255, 64, 0, 255), // textColor
            1000, // textFadeInStartS
            1000, // textFadeInEndS
            1000, // textFadeOutStartS
            1000, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() { }))
          .AsIGameEvent());
      }
      if (triggerName == "showTitle") {
        game.events.Add(
          new ShowOverlayEvent(
            30, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            500, // fadeInEnd
            2000, // fadeOutStart
            2500, // fadeOutEnd,
            "",

            "Basic Introduction",
            new Color(255, 255, 255, 255), // textColor
            500, // textFadeInStartS
            1000, // textFadeInEndS
            2000, // textFadeOutStartS
            2500, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>()))
          .AsIGameEvent());
      }

      if (triggerName == "ambush1b") {
        game.events.Add(
          new ShowOverlayEvent(
            50, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            300, // fadeInEnd
            0, // fadeOutStart
            0, // fadeOutEnd,
            "",

            "You see an Irkling!\n\nTo attack, click on it while next to it.",
            new Color(255, 255, 255, 255), // textColor
            0, // textFadeInStartS
            300, // textFadeInEndS
            0, // textFadeOutStartS
            0, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() {
              new Button("Forward to glory!", new Color(64, 64, 64, 255), "")
            }))
          .AsIGameEvent());
      }

      if (triggerName == "ambush2b") {
        game.events.Add(
          new ShowOverlayEvent(
            50, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            300, // fadeInEnd
            0, // fadeOutStart
            0, // fadeOutEnd,
            "",

            "You see a Baug!\n\nBaugs have a lot of life points. You'll need to hit it several times.",
            new Color(255, 255, 255, 255), // textColor
            0, // textFadeInStartS
            300, // textFadeInEndS
            0, // textFadeOutStartS
            0, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() {
              new Button("Forward to battle!", new Color(64, 64, 64, 255), "")
            }))
          .AsIGameEvent());
      }

      if (triggerName == "ambush3b") {
        game.events.Add(new NarrateEvent(1 + " Defy remaining!").AsIGameEvent());
        game.events.Add(
          new ShowOverlayEvent(
            70, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            300, // fadeInEnd
            0, // fadeOutStart
            0, // fadeOutEnd,
            "",

            "You see a Spiriant!\n\nSpiriants are incredibly dangerous.\n\nHowever, they die with one hit.\n\nUse Defy ('D') to wait for it to come to you.",
            new Color(255, 255, 255, 255), // textColor
            0, // textFadeInStartS
            300, // textFadeInEndS
            0, // textFadeOutStartS
            0, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() {
              new Button("For valor!", new Color(64, 64, 64, 255), "")
            }))
          .AsIGameEvent());
      }

      if (triggerName == "healthPotionB") {
        game.events.Add(
          new ShowOverlayEvent(
            70, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            300, // fadeInEnd
            0, // fadeOutStart
            0, // fadeOutEnd,
            "",

            "You've found a health potion!\n\nYou can't take it with you, but you can use it now.\n\nUse Interact ('I') to use it.",
            new Color(255, 255, 255, 255), // textColor
            0, // textFadeInStartS
            300, // textFadeInEndS
            0, // textFadeOutStartS
            0, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() {
              new Button("For prosperity!", new Color(64, 64, 64, 255), "")
            }))
          .AsIGameEvent());
      }
      if (triggerName == "ambush4b") {
        game.events.Add(
          new ShowOverlayEvent(
            60, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            300, // fadeInEnd
            0, // fadeOutStart
            0, // fadeOutEnd,
            "",

            "You see a Ravagian Trask!\n\nThese attack very fast.\n\nYou'll need help!",
            new Color(255, 255, 255, 255), // textColor
            0, // textFadeInStartS
            300, // textFadeInEndS
            0, // textFadeOutStartS
            0, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() {
              new Button("Help? How?", new Color(64, 64, 64, 255), "ambush4c")
            }))
          .AsIGameEvent());
      }
      if (triggerName == "ambush4c") {
        game.events.Add(
          new ShowOverlayEvent(
            60, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            300, // fadeInEnd
            0, // fadeOutStart
            0, // fadeOutEnd,
            "",

            "You must help your future self, so that in the future, you'll receive help from your past self.",
            new Color(255, 255, 255, 255), // textColor
            0, // textFadeInStartS
            300, // textFadeInEndS
            0, // textFadeOutStartS
            0, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() {
              new Button("Nonsense!", new Color(64, 64, 64, 255), "ambush4d")
            }))
          .AsIGameEvent());
      }

      if (triggerName == "ambush4d") {
        game.events.Add(
          new ShowOverlayEvent(
            60, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            300, // fadeInEnd
            0, // fadeOutStart
            0, // fadeOutEnd,
            "",

            "Let's see it in action.\n\nFirst, select Time Anchor ('A') and then step in any direction.\n\n(In this case, go right.)",
            new Color(255, 255, 255, 255), // textColor
            0, // textFadeInStartS
            300, // textFadeInEndS
            0, // textFadeOutStartS
            0, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() {
              new Button("Will do!", new Color(64, 64, 64, 255), "")
            }))
          .AsIGameEvent());
      }

      if (triggerName == "ambush4e") {
        if (superstate.anchorTurnIndices.Count == 0) {
          game.events.Add(
            new ShowOverlayEvent(
              60, // sizePercent
              new Color(0, 0, 0, 224), // backgroundColor
              300, // fadeInEnd
              0, // fadeOutStart
              0, // fadeOutEnd,
              "",

              "Uh oh! You didn't create a time anchor.\n\nYou're probably going to die now.\n\nNext time, use Time Anchor ('A')!",
              new Color(255, 255, 255, 255), // textColor
              0, // textFadeInStartS
              300, // textFadeInEndS
              0, // textFadeOutStartS
              0, // textFadeOutEndS
              true, // topAligned
              true, // leftAligned

              new ButtonImmList(new List<Button>() {
                new Button("Alas...", new Color(64, 64, 64, 255), "")
              }))
            .AsIGameEvent());
        } else {
          game.player.components.Add(game.root.EffectTutorialDefyCounterUCCreate(10, "firstSelfDefied").AsIUnitComponent());
          game.events.Add(new NarrateEvent(10 + " Defy remaining!").AsIGameEvent());
          game.events.Add(
            new ShowOverlayEvent(
              60, // sizePercent
              new Color(0, 0, 0, 224), // backgroundColor
              300, // fadeInEnd
              0, // fadeOutStart
              0, // fadeOutEnd,
              "",

              "Now, use Defy ('D') about 10 times.\n\nYour future self will thank you, because you are distracting the Ravagian Trask.",
              new Color(255, 255, 255, 255), // textColor
              0, // textFadeInStartS
              300, // textFadeInEndS
              0, // textFadeOutStartS
              0, // textFadeOutEndS
              true, // topAligned
              true, // leftAligned

              new ButtonImmList(new List<Button>() {
                new Button("For valor!", new Color(64, 64, 64, 255), "")
              }))
            .AsIGameEvent());
        }
      }
      if (triggerName == "firstSelfDefied") {
        var defyCounter = game.player.components.GetOnlyTutorialDefyCounterUCOrNull();
        Asserts.Assert(defyCounter.Exists());

        if (defyCounter.numDefiesRemaining == 0) {
          game.player.components.Remove(defyCounter.AsIUnitComponent());
          defyCounter.Destruct();
          game.events.Add(
            new ShowOverlayEvent(
              50, // sizePercent
              new Color(0, 0, 0, 224), // backgroundColor
              300, // fadeInEnd
              0, // fadeOutStart
              0, // fadeOutEnd,
              "",

              "Now, use Time Revert ('R') to go back in time.",
              new Color(255, 255, 255, 255), // textColor
              0, // textFadeInStartS
              300, // textFadeInEndS
              0, // textFadeOutStartS
              0, // textFadeOutEndS
              true, // topAligned
              true, // leftAligned

              new ButtonImmList(new List<Button>() {
                new Button("Backward to glory!", new Color(64, 64, 64, 255), "")
              }))
            .AsIGameEvent());
        } else {
          game.events.Add(new NarrateEvent(defyCounter.numDefiesRemaining + " Defy remaining!").AsIGameEvent());
        }
      }

      if (triggerName == "ambush4f") {
        game.events.Add(new NarrateEvent(1 + " Defy remaining!").AsIGameEvent());
        game.events.Add(
          new ShowOverlayEvent(
            70, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            300, // fadeInEnd
            0, // fadeOutStart
            0, // fadeOutEnd,
            "",

            "Now, your past self is here to help you!\n\nIt will do the same things you did.\n\nDefy ('D') once, then attack the Ravagian Trask while it attacks your past self!",
            new Color(255, 255, 255, 255), // textColor
            0, // textFadeInStartS
            300, // textFadeInEndS
            0, // textFadeOutStartS
            0, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() {
              new Button("For vengeance!", new Color(64, 64, 64, 255), "")
            }))
          .AsIGameEvent());
      }

      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this Tutorial1LevelController obj,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      game.root.logger.Info("Got simple unit trigger: " + triggerName);
      if (triggeringUnit.NullableIs(game.player) && triggerName == "ambush1Trigger") {
        superstate.levelSuperstate.RemoveSimplePresenceTriggers("ambush1Trigger", 1);
        game.level.EnterUnit(
          superstate.levelSuperstate,
          superstate.levelSuperstate.FindMarkerLocation("ambush1Summon"),
          game.player.nextActionTime + 300,
          Irkling.Make(game.root));
        game.events.Add(new WaitEvent(400, "ambush1b").AsIGameEvent());
        superstate.navigatingState = null;
      }
      if (triggeringUnit.NullableIs(game.player) && triggerName == "ambush2Trigger") {
        superstate.levelSuperstate.RemoveSimplePresenceTriggers("ambush2Trigger", 1);
        game.level.EnterUnit(
          superstate.levelSuperstate,
          superstate.levelSuperstate.FindMarkerLocation("ambush2Summon"),
          game.player.nextActionTime + 300,
          Baug.Make(game.root));
        game.events.Add(new WaitEvent(400, "ambush2b").AsIGameEvent());
        superstate.navigatingState = null;
      }
      if (triggeringUnit.NullableIs(game.player) && triggerName == "ambush3Trigger") {
        superstate.levelSuperstate.RemoveSimplePresenceTriggers("ambush3Trigger", 1);
        game.level.EnterUnit(
          superstate.levelSuperstate,
          superstate.levelSuperstate.FindMarkerLocation("ambush3Summon"),
          game.player.nextActionTime + 300,
          Spirient.Make(game.root));
        game.events.Add(new WaitEvent(400, "ambush3b").AsIGameEvent());
        superstate.navigatingState = null;
      }
      if (triggeringUnit.NullableIs(game.player) && triggerName == "defyHint") {
        superstate.levelSuperstate.RemoveSimplePresenceTriggers("defyHint", 1);
        game.events.Add(
          new ShowOverlayEvent(
            50, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            300, // fadeInEnd
            0, // fadeOutStart
            0, // fadeOutEnd,
            "",

            "Pro tip: Defy also taunts adjacent enemies to attack you.",
            new Color(255, 255, 255, 255), // textColor
            0, // textFadeInStartS
            300, // textFadeInEndS
            0, // textFadeOutStartS
            0, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>() {
              new Button("Got it!", new Color(64, 64, 64, 255), "")
            }))
          .AsIGameEvent());
        superstate.navigatingState = null;
      }
      if (triggeringUnit.NullableIs(game.player) && triggerName == "healthPotion") {
        superstate.levelSuperstate.RemoveSimplePresenceTriggers("healthPotion", 1);
        game.events.Add(new WaitEvent(400, "healthPotionB").AsIGameEvent());
        superstate.navigatingState = null;
      }
      if (triggeringUnit.NullableIs(game.player) && triggerName == "ambush4Trigger") {
        superstate.levelSuperstate.RemoveSimplePresenceTriggers("ambush4Trigger", 1);
        game.level.EnterUnit(
          superstate.levelSuperstate,
          superstate.levelSuperstate.FindMarkerLocation("ambush4Summon"),
          game.player.nextActionTime + 300,
          RavagianTrask.Make(game.root));

        game.events.Add(new WaitEvent(400, "ambush4b").AsIGameEvent());
        superstate.navigatingState = null;
      }
      if (triggerName == "ambush4DefySpot" &&
          triggeringUnit.NullableIs(game.player)) {
        superstate.levelSuperstate.RemoveSimplePresenceTriggers("ambush4DefySpot", 1);
        game.events.Add(new WaitEvent(400, "ambush4e").AsIGameEvent());
        superstate.navigatingState = null;
      }
      if (triggerName == "ambush4DefySpot" &&
          triggeringUnit.components.GetAllTimeCloneAICapabilityUC().Count > 0) {
        superstate.levelSuperstate.RemoveSimplePresenceTriggers("ambush4DefySpot", 1);
        game.events.Add(new WaitEvent(600, "ambush4f").AsIGameEvent());
        superstate.navigatingState = null;
      }
      return new Atharia.Model.Void();
    }
    
    private static string LEVEL = @"
-8 -2 4 2 CaveWall
-8 -2 5 2 CaveWall
-8 -2 6 2 CaveWall
-8 -2 7 1 Dirt
-7 -2 0 2 CaveWall
-7 -2 2 2 CaveWall
-7 -2 4 2 CaveWall
-7 -2 5 2 CaveWall
-7 -2 6 1 Dirt Marker(ambush4Summon)
-7 -2 7 1 Dirt
-7 -1 0 1 Dirt
-7 -1 1 2 CaveWall
-7 -1 2 1 Dirt Cave Marker(exit)
-7 -1 3 2 CaveWall
-7 -1 4 2 CaveWall
-6 -2 0 2 CaveWall
-6 -2 1 2 CaveWall
-6 -2 2 1 Dirt
-6 -2 3 1 Dirt
-6 -2 4 2 CaveWall
-6 -2 5 1 Dirt Trigger(ambush4Trigger)
-6 -2 6 1 Dirt Trigger(ambush4DefySpot)
-6 -2 7 2 CaveWall
-6 -1 0 2 CaveWall
-6 -1 1 1 Dirt
-6 -1 2 1 Dirt
-6 -1 3 1 Dirt
-6 -1 4 2 CaveWall
-6 -1 5 2 CaveWall
-5 -2 0 2 CaveWall
-5 -2 1 2 CaveWall
-5 -2 2 1 Dirt
-5 -2 3 1 Dirt Trigger(ambush4Warning)
-5 -2 4 2 CaveWall
-5 -2 5 2 CaveWall
-5 -1 0 2 CaveWall
-5 -1 1 2 CaveWall
-5 -1 3 2 CaveWall
-5 -1 5 2 CaveWall
-4 -2 0 2 CaveWall
-4 -2 1 2 CaveWall
-4 -2 2 1 Dirt
-4 -2 3 1 Dirt HealthPotion Trigger(healthPotion)
-4 -2 4 1 Dirt Trigger(defyHint)
-4 -2 5 2 CaveWall
-4 -2 6 2 CaveWall
-4 -2 7 1 Dirt
-3 -2 1 2 CaveWall
-3 -2 3 2 CaveWall
-3 -2 4 2 CaveWall
-3 -2 5 1 Dirt Trigger(defyHint)
-3 -2 6 1 Dirt Marker(ambush3Summon)
-3 -2 7 2 CaveWall
-3 -1 0 1 Dirt
-3 -1 1 2 CaveWall
-3 -1 2 1 Dirt Trigger(ambush3Trigger)
-3 -1 3 2 CaveWall
-3 -1 4 1 Dirt
-3 -1 5 2 CaveWall
-3 -1 6 2 CaveWall
-3 -1 7 1 Dirt
-3 0 0 2 CaveWall
-3 0 2 2 CaveWall
-2 -1 0 2 CaveWall
-2 -1 1 1 Dirt
-2 -1 2 2 CaveWall
-2 -1 3 1 Dirt Trigger(ambush3Trigger)
-2 -1 4 2 CaveWall
-2 -1 5 1 Dirt
-2 -1 6 1 Dirt
-2 -1 7 2 CaveWall
-2 0 0 1 Dirt Marker(ambush2Summon)
-2 0 1 1 Dirt
-2 0 2 1 Dirt Trigger(ambush2Trigger)
-2 0 3 2 CaveWall
-2 0 4 1 Dirt
-2 0 5 2 CaveWall
-2 0 6 2 CaveWall
-2 0 7 2 CaveWall
-1 -1 0 3 CaveWall
-1 -1 2 3 CaveWall
-1 -1 4 3 CaveWall
-1 -1 7 2 CaveWall
-1 0 0 2 CaveWall
-1 0 1 2 CaveWall
-1 0 2 2 CaveWall
-1 0 3 1 Dirt
-1 0 4 1 Dirt
-1 0 5 1 Dirt
-1 0 6 1 Dirt
-1 0 7 1 Dirt
-1 1 0 2 CaveWall
0 -1 0 2 CaveWall
0 -1 1 3 CaveWall
0 -1 2 1 Dirt
0 -1 3 2 Dirt
0 -1 4 1 Dirt Rocks
0 -1 5 1 Mud Dirt
0 -1 6 1 Mud Dirt
0 -1 7 2 Mud Dirt
0 0 0 2 CaveWall
0 0 2 2 CaveWall
0 0 3 2 CaveWall
0 0 4 1 Dirt
0 0 5 1 Dirt
0 0 6 1 Dirt Rocks
0 0 7 1 Dirt
0 1 0 2 CaveWall
0 1 1 2 CaveWall
1 -1 1 2 CaveWall
1 -1 2 2 CaveWall
1 -1 3 1 Dirt
1 -1 4 1 Dirt
1 -1 5 1 Dirt Marker(start)
1 -1 6 1 Dirt
1 -1 7 2 Dirt
1 0 0 1 Mud Dirt
1 0 1 1 Mud Dirt
1 0 2 1 Dirt Rocks
1 0 3 1 Dirt Trigger(ambush1Trigger)
1 0 4 2 Dirt Trigger(ambush1Trigger)
1 0 5 1 Dirt Marker(ambush1Summon)
1 0 6 2 CaveWall
1 0 7 3 CaveWall
1 1 0 2 CaveWall
1 1 1 2 CaveWall
2 -1 3 4 CaveWall
2 -1 5 3 CaveWall
2 -1 6 3 CaveWall
2 0 0 3 CaveWall
2 0 1 2 Mud Dirt
2 0 2 3 CaveWall
2 0 3 3 CaveWall
2 0 5 3 CaveWall

";
  }
}
