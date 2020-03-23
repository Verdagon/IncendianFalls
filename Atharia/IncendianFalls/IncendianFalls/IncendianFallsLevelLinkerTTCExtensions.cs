using Atharia.Model;
using IncendianFalls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Atharia.Model {
  static class IncendianFallsLevelLinkerTTCExtensions {
    public static Atharia.Model.Void Destruct(this IncendianFallsLevelLinkerTTC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static string Interact(
      this IncendianFallsLevelLinkerTTC linker,
        IncendianFalls.SSContext context,
      Game game,
      Superstate superstate,
      Unit interactingUnit,
      Location containingTileLocation) {

      Asserts.Assert(game.level.units.Contains(game.player));

      //// Unless this is going to -1, which means it's the first level staircase,
      //// so do nothing.
      //if (staircase.destinationLevelPortalIndex == -1) {
      //  return "I can't go back, I must go forward!";
      //}

      // Replace this linker with a regular LevelLink.
      var thisLevelDepth = linker.thisLevelDepth;
      game.level.terrain.tiles[containingTileLocation].components.Remove(linker.AsITerrainTileComponent());
      linker.Destruct();

      MakeNextLevel(
          out var nextLevel,
          out var nextLevelSuperstate,
          out var nextLevelEntryLocation,
          context,
          game,
          superstate,
          thisLevelDepth + 1);

      // Link to the next level.
      var levelLink = game.root.EffectLevelLinkTTCCreate(false, nextLevel, nextLevelEntryLocation);
      game.level.terrain.tiles[containingTileLocation].components.Add(levelLink.AsITerrainTileComponent());

      // Make the next level link back to here.
      var nextLevelEntryTile = nextLevel.terrain.tiles[nextLevelEntryLocation];
      nextLevelEntryTile.components.Add(
        game.root.EffectLevelLinkTTCCreate(false, game.level, containingTileLocation).AsITerrainTileComponent());

      // Travel the level link, to switch levels.
      return levelLink.Interact(context, game, superstate, interactingUnit, containingTileLocation);
    }


    public static void MakeNextLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocation,
        SSContext context,
        Game game,
        Superstate superstate,
        int depth) {
      int numCliffLevels = IncendianFallsUnitsAndItems.TOTAL_NUM_LEVELS_BEFORE_BOSS * 2 / 3;
      int cliffLevelsEnd = numCliffLevels;
      int caveLevelsStart = cliffLevelsEnd;
      int numCaveLevels = IncendianFallsUnitsAndItems.TOTAL_NUM_LEVELS_BEFORE_BOSS - numCliffLevels;
      int caveLevelsEnd = caveLevelsStart + numCaveLevels;
      int volcaetusLevel = caveLevelsEnd;

      if (game.squareLevelsOnly) {
        SquareCaveLevelControllerExtensions.MakeLevel(
            out level,
            out levelSuperstate,
            out entryLocation,
            out var exitLocation,
            game,
            superstate,
            //levelAbove,
            //(depth == 0 ? -1 : 1),
            //Level.Null,
            //0,
            //game.levels.Count,
            depth);
        return;
      }

      int levelIndex = game.levels.Count;

      if (levelIndex < cliffLevelsEnd) {
        GenerateCliffLevel.MakeLevel(
            out level,
            out levelSuperstate,
            out entryLocation,
            context,
            game,
            superstate,
            //levelAbove,
            //(depth == 0 ? -1 : 1),
            //levelIndex,
            depth);
      } else if (levelIndex < caveLevelsEnd) {
        if (levelIndex % 2 == 1) {
          PentagonalCaveLevelControllerExtensions.MakeLevel(
              out level,
              out levelSuperstate,
              out entryLocation,
              out var exitLocation,
              context,
              game,
              superstate,
              //1,
              //Level.Null,
              //0,
              //levelIndex,
              depth);
        } else {
          SquareCaveLevelControllerExtensions.MakeLevel(
              out level,
              out levelSuperstate,
              out entryLocation,
              out var exitLocation,
              game,
              superstate,
              //levelAbove,
              //1,
              //Level.Null,
              //0,
              //levelIndex,
              depth);
        }
      } else {
        RavashrikeLevelControllerExtensions.MakeLevel(
            out level,
            out levelSuperstate,
            out entryLocation,
            context,
            game,
            superstate,
            depth,
            levelIndex);
      }
    }


  }
}