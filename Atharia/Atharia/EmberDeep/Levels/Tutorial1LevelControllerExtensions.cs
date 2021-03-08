using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class Tutorial1LevelControllerExtensions {
    public static Atharia.Model.Void Destruct(this Tutorial1LevelController self) {
      self.Delete();
      return new Atharia.Model.Void();
    }

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
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        string triggerName) {
      game.root.logger.Error("Got simple trigger: " + triggerName);

      if (triggerName == "levelStart") {
        game.player.hp = game.player.maxHp;
        var sorcerous = game.player.components.GetOnlySorcerousUCOrNull();
        if (sorcerous.Exists()) {
          sorcerous.mp = sorcerous.maxMp;
        }
        foreach (var item in game.player.components.GetAllIItem()) {
          game.player.components.Remove(item.AsIUnitComponent());
          item.Destruct();
        }

        game.ShowAside("Introduction");
      }


      if (triggerName == "ambush3bDefied") {
        var defyCounter = game.player.components.GetOnlyTutorialDefyCounterUCOrNull();
        Asserts.Assert(defyCounter.Exists());
        Asserts.Assert(defyCounter.numDefiesRemaining == 0);
        game.player.components.Remove(defyCounter.AsIUnitComponent());
        defyCounter.Destruct();
        game.instructions = "";
      }

      if (triggerName == "firstSelfDefied") {
        var defyCounter = game.player.components.GetOnlyTutorialDefyCounterUCOrNull();
        Asserts.Assert(defyCounter.Exists());

        if (defyCounter.numDefiesRemaining == 0) {
          game.player.components.Remove(defyCounter.AsIUnitComponent());
          defyCounter.Destruct();
          game.instructions = "";
          game.ShowComm("Now, use Time Revert ('R') to go back in time.", "Backward to glory!");
        } else {
          game.instructions = defyCounter.numDefiesRemaining + " Defies remaining!";
        }
      }

      if (triggerName == "ambush4fDefied") {
        var defyCounter = game.player.components.GetOnlyTutorialDefyCounterUCOrNull();
        Asserts.Assert(defyCounter.Exists());
        Asserts.Assert(defyCounter.numDefiesRemaining == 0);
        game.player.components.Remove(defyCounter.AsIUnitComponent());
        defyCounter.Destruct();
        game.instructions = "";
      }

      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this Tutorial1LevelController obj,
        IncendianFalls.SSContext context,
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
        game.player.WaitFor();
        game.ShowComm("You see an Irkling!\n\nTo attack, click on it while next to it.", "Forward to glory!");
      }
      if (triggeringUnit.NullableIs(game.player) && triggerName == "ambush2Trigger") {
        superstate.levelSuperstate.RemoveSimplePresenceTriggers("ambush2Trigger", 1);
        game.level.EnterUnit(
          superstate.levelSuperstate,
          superstate.levelSuperstate.FindMarkerLocation("ambush2Summon"),
          game.player.nextActionTime + 300,
          Baug.Make(game.root));
        game.player.WaitFor();

        game.ShowComm("You see a Baug!\n\nBaugs have a lot of life points. You'll need to hit it several times.", "Forward to battle!");
      }
      if (triggeringUnit.NullableIs(game.player) && triggerName == "ambush3Trigger") {
        superstate.levelSuperstate.RemoveSimplePresenceTriggers("ambush3Trigger", 1);
        game.level.EnterUnit(
          superstate.levelSuperstate,
          superstate.levelSuperstate.FindMarkerLocation("ambush3Summon"),
          game.player.nextActionTime + 300,
          Spirient.Make(game.root));
        game.player.WaitFor();

        game.instructions = 1 + " Defy remaining!";
        game.player.components.Add(game.root.EffectTutorialDefyCounterUCCreate(1, "ambush3bDefied").AsIUnitComponent());
        game.ShowComm("You see a Spiriant!\n\nSpiriants are incredibly dangerous.\n\nHowever, they die with one hit.\n\nUse Defy ('D') to wait for it to come to you.", "For valor!");
      }
      if (triggeringUnit.NullableIs(game.player) && triggerName == "defyHint") {
        superstate.levelSuperstate.RemoveSimplePresenceTriggers("defyHint", 1);
        game.ShowComm("Pro tip: Defy also taunts adjacent enemies to attack you.", "Got it!");
      }
      if (triggeringUnit.NullableIs(game.player) && triggerName == "healthPotion") {
        superstate.levelSuperstate.RemoveSimplePresenceTriggers("healthPotion", 1);
        game.player.WaitFor();

        // Player might not have defied.
        var defyCounter = game.player.components.GetOnlyTutorialDefyCounterUCOrNull();
        if (defyCounter.Exists()) {
          game.player.components.Remove(defyCounter.AsIUnitComponent());
          defyCounter.Destruct();
          game.instructions = "";
        }

        game.ShowComm("You've found a health potion!\n\nYou can't take it with you, but you can use it now.\n\nUse Interact ('e') to use it.", "For prosperity!");
      }
      if (triggeringUnit.NullableIs(game.player) && triggerName == "ambush4Trigger") {
        superstate.levelSuperstate.RemoveSimplePresenceTriggers("ambush4Trigger", 1);
        game.level.EnterUnit(
          superstate.levelSuperstate,
          superstate.levelSuperstate.FindMarkerLocation("ambush4Summon"),
          game.player.nextActionTime + 300,
          RavagianTrask.Make(game.root));
        game.player.WaitFor();

        game.ShowComm("You see a Ravagian Trask!\n\nThese attack very fast.\n\nYou'll need help!", "Help? How?");
        game.ShowComm("You must help your future self, so that in the future, you'll receive help from your past self.", "Nonsense!");
        game.ShowComm("Let's see it in action.\n\nFirst, select Time Anchor ('A') and then step in any direction.\n\n(In this case, go right.)", "Will do!");
      }
      if (triggerName == "ambush4DefySpot" &&
          triggeringUnit.NullableIs(game.player)) {
        superstate.levelSuperstate.RemoveSimplePresenceTriggers("ambush4DefySpot", 1);
        game.player.WaitFor();

        if (superstate.anchorTurnIndices.Count == 0) {
          game.ShowComm("Uh oh! You didn't create a time anchor.\n\nYou're probably going to die now.\n\nNext time, use Time Anchor ('A')!", "Alas...");
        } else {
          game.player.components.Add(game.root.EffectTutorialDefyCounterUCCreate(10, "firstSelfDefied").AsIUnitComponent());
          game.instructions = 10 + " Defy remaining!";
          game.ShowComm("Now, use Defy ('D') about 10 times.\n\nYour future self will thank you, because you are distracting the Ravagian Trask.", "For valor!");
        }
      }
      if (triggerName == "ambush4DefySpot" &&
          triggeringUnit.components.GetAllTimeCloneAICapabilityUC().Count > 0) {
        superstate.levelSuperstate.RemoveSimplePresenceTriggers("ambush4DefySpot", 1);
        game.player.WaitFor();

        // Player might not have finished defying the whole 10 previous ones.
        var defyCounter = game.player.components.GetOnlyTutorialDefyCounterUCOrNull();
        if (defyCounter.Exists()) {
          game.player.components.Remove(defyCounter.AsIUnitComponent());
          defyCounter.Destruct();
          game.instructions = "";
        }

        game.instructions = 1 + " Defy remaining!";
        game.player.components.Add(game.root.EffectTutorialDefyCounterUCCreate(1, "ambush4fDefied").AsIUnitComponent());
        game.ShowComm("Now, your past self is here to help you!\n\nIt will do the same things you did.\n\nDefy ('D') once, then attack the Ravagian Trask while it attacks your past self!", "For vengeance!");
      }
      if (triggeringUnit.NullableIs(game.player) && triggerName == "multipleHint") {
        // Make sure there arent any defy counters leftover, in case the player won the entire
        // thing without defying somehow.
        var defyCounter = game.player.components.GetOnlyTutorialDefyCounterUCOrNull();
        if (defyCounter.Exists()) {
          game.player.components.Remove(defyCounter.AsIUnitComponent());
          defyCounter.Destruct();
          game.instructions = "";
        }

        superstate.levelSuperstate.RemoveSimplePresenceTriggers("multipleHint", 1);
        game.ShowComm("You can have several past selves active at the same time.\n\nSometimes, it's the only way to survive!", "Together I stand!");
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
-7 -1 0 1 Dirt Trigger(multipleHint)
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
-6 -1 1 1 Dirt Trigger(multipleHint)
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
