using System;
using System.Collections.Generic;
using System.Linq;
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
        out SortedSet<Location> openLowerLocsRet,
        out SortedSet<Location> openUpperLocsRet,
        SSContext context,
        Game game,
        Superstate superstate,
        int depth,
        bool squareLevelsOnly) {
      bool considerCornersAdjacent = false;

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      Pattern pattern = squareLevelsOnly ? SquarePattern.MakeSquarePattern() : PentagonPattern9.makePentagon9Pattern();
      if (depth == 2) {
        pattern = HexPattern.MakeHexPattern();
      }
      var (terrain, openLowerLocs, openUpperLocs) =
        IntertwiningCaveTerrainGenerator.Generate(
          context,
          game.root,
          pattern,
          false,
          game.rand,
          20.0f);
      openLowerLocsRet = openLowerLocs;
      openUpperLocsRet = openUpperLocs;
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


      var entryAndExitCandidateLocations = openLowerLocs;
      var wideOpenLocations = level.terrain.pattern.GetInnerLocations(openLowerLocs, considerCornersAdjacent);
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
      openLowerLocs.Remove(entryLocation);

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      // var exitLocation =
      //   levelSuperstate.GetNRandomWalkableLocations(
      //     level.terrain,
      //     game.rand,
      //     1,
      //     (loc) => !loc.Equals(entryLocation),
      //     false,
      //     false)[0];
      // level.terrain.tiles[exitLocation].components.Add(
      //   game.root.EffectCaveTTCCreate().AsITerrainTileComponent());
      // level.terrain.tiles[exitLocation].components.Add(
      //   game.root.EffectEmberDeepLevelLinkerTTCCreate(depth + 1).AsITerrainTileComponent());
      // openUpperLocs.Remove(exitLocation);

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      // EmberDeepUnitsAndItems.PlaceRocks(game.rand, level, levelSuperstate);
      //
      // context.Flare(context.root.GetDeterministicHashCode().ToString());

      level.controller = game.root.EffectJumpingCaveLevelControllerCreate(level, depth).AsILevelController();

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      game.levels.Add(level);

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      levelRet = level;
      entryLocationRet = entryLocation;
    }

    public static string GetName(this JumpingCaveLevelController obj) {
      return "Forest";
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
      if (self.depth == 0 && triggerName == "viviantDied") {
        var viviants = new List<Unit>();
        foreach (var unit in game.level.units) {
          var deathTrigger = unit.components.GetOnlyDeathTriggerUCOrNull();
          if (deathTrigger.Exists()) {
            if (deathTrigger.triggerName == "viviantDied") {
              viviants.Add(unit);
            }
          }
        }
        if (viviants.Count() == 9) {
          game.player.components.Add(game.root.EffectChallengingUCCreate().AsIUnitComponent());
        }
        // // In the second half, every time a viviant dies, send all idle units on the level at the player.
        // if (viviants.Count() < 10) {
        //   foreach (var viviant in game.level.units) {
        //     var attackCapability = viviant.components.GetOnlyAttackAICapabilityUCOrNull();
        //     if (attackCapability.Exists()) {
        //       if (!attackCapability.killDirective.Exists()) {
        //         var pathToPlayer = superstate.levelSuperstate.FindPath(viviant.location, game.player.location);
        //         attackCapability.killDirective =
        //             self.root.EffectKillDirectiveCreate(
        //                 game.player,
        //                 self.root.EffectLocationMutListCreate(pathToPlayer));
        //       }
        //     }
        //   }
        // }
        if (viviants.Count() == 1) {
          var viviarch = Viviarch.Make(self.root);
          var viviarchLoc =
              superstate.levelSuperstate.GetNRandomWalkableLocations(
                  game.level.terrain,
                  game.rand,
                  1,
                  (a) => true,
                  true,
                  true)[0];
          game.level.EnterUnit(superstate.levelSuperstate, viviarchLoc, game.time, viviarch);
          
          game.EnterCinematic();
          game.Wait(1500);
          game.FlyCameraTo(1000, viviarchLoc);
          game.Wait(800);
          game.ShowAside("kylin", "Finally, found the cause of all this chaos!");
          game.Wait(800);
          game.FlyCameraTo(1500, game.player.location);
          game.ExitCinematic();
        }
      }
      if (self.depth == 0 && triggerName == "viviarchDied") {
        game.hideInput = true;
        game.comms.Add(
            game.root.EffectCommCreate(
                new DramaticCommTemplate(false).AsICommTemplate(),
                new CommActionImmList(new CommAction("Huzzah!", "_exitGame")),
                new CommTextImmList(new CommText("narrator", "Congratulations, you have won the game!"))));
      }
      if (self.depth == 0 && triggerName == "levelStart") {
        var hoppableLocs = superstate.levelSuperstate.GetHoppableLocs(game.player.location, true);
        if (hoppableLocs.Count > 0) {
          Actions.Hop(game, superstate, game.player, SetUtils.GetFirst(hoppableLocs), false);
          game.player.WaitFor();
        }
        game.ShowAside("kylin", "I've made it to the forest! I need to kill all these corrupted spirits!");
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
