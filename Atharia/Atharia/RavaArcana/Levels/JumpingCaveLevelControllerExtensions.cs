﻿using System;
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

      levelSuperstate.Reconstruct(level);

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
