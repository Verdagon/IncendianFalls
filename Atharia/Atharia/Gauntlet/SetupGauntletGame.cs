using Atharia.Model;
using IncendianFalls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gauntlet {
  class SetupGauntletGame {
    public static Game SetupGame(
        SSContext context,
        out Superstate superstate,
        int randomSeed,
        bool squareLevelsOnly) {

      var rand = context.root.EffectRandCreate(randomSeed);

      var levels = context.root.EffectLevelMutSetCreate();

      var game =
          context.root.EffectGameCreate(
              rand,
              squareLevelsOnly,
              levels,
              Unit.Null,
              Level.Null,
              0,

              Unit.Null,
              false,

              1,

              "",
              false,

              NullIGameEvent.Null,
              context.root.EffectCommMutListCreate());

      superstate =
          new Superstate(
            game,
            null,
            new List<RootIncarnation>(),
            new List<IRequest>(),
            new List<int>());

      PreGauntletLevelControllerExtensions.MakeLevel(
          out var preGauntletLevel,
          out var preGauntletLevelSuperstate,
          out var downStaircaseLocation,
          context,
          game.root,
          squareLevelsOnly,
          game.rand,
          game.time);
      game.levels.Add(preGauntletLevel);

      GauntletLevelControllerExtensions.MakeLevel(
          out var gauntletLevel,
          out var gauntletLevelSuperstate,
          out var upStaircaseLocation,
          context,
          game,
          squareLevelsOnly);
      game.levels.Add(gauntletLevel);


      var downStaircaseTile = preGauntletLevel.terrain.tiles[downStaircaseLocation];
      downStaircaseTile.components.Add(
        game.root.EffectLevelLinkTTCCreate(false, gauntletLevel, upStaircaseLocation).AsITerrainTileComponent());

      var upStaircaseTile = gauntletLevel.terrain.tiles[upStaircaseLocation];
      upStaircaseTile.components.Add(
        game.root.EffectLevelLinkTTCCreate(false, preGauntletLevel, downStaircaseLocation).AsITerrainTileComponent());

      game.level = preGauntletLevel;
      superstate.levelSuperstate = preGauntletLevelSuperstate;

      var player = Chronomancer.Make(context.root);
      preGauntletLevel.EnterUnit(
          superstate.levelSuperstate,
          downStaircaseLocation,
          0,
          player);
      game.player = player;

      return game;
    }
  }
}
