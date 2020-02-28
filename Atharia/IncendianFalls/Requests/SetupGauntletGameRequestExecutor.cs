using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  public static class SetupGauntletGameRequestExecutor {
    public static Game Execute(
        SSContext context,
        out Superstate superstate,
        SetupGauntletGameRequest request) {
      int randomSeed = request.randomSeed;
      bool squareLevelsOnly = request.squareLevelsOnly;

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
              context.root.EffectExecutionStateCreate(
                  Unit.Null,
                  false,
                  IPreActingUCWeakMutBunch.Null,
                  IPostActingUCWeakMutBunch.Null));

      superstate =
          new Superstate(
            game,
            null,
            new List<RootIncarnation>(),
            new List<IRequest>(),
            new List<int>(),
            null,
            null);


        PreGauntletLevelControllerExtensions.MakeLevel(
            out var preGauntletLevel,
            out var preGauntletLevelSuperstate,
            out var downStaircaseLocation,
            game.root,
            game.rand,
            game.time);
      game.levels.Add(preGauntletLevel);

      GauntletLevelControllerExtensions.MakeLevel(
          out var gauntletLevel,
          out var gauntletLevelSuperstate,
          out var upStaircaseLocation,
          game);
      game.levels.Add(gauntletLevel);


      var downStaircaseTile = preGauntletLevel.terrain.tiles[downStaircaseLocation];
      downStaircaseTile.components.Add(
        game.root.EffectLevelLinkTTCCreate(gauntletLevel, upStaircaseLocation).AsITerrainTileComponent());

      var upStaircaseTile = preGauntletLevel.terrain.tiles[upStaircaseLocation];
      upStaircaseTile.components.Add(
        game.root.EffectLevelLinkTTCCreate(preGauntletLevel, downStaircaseLocation).AsITerrainTileComponent());

      game.level = preGauntletLevel;
      superstate.levelSuperstate = preGauntletLevelSuperstate;

      var player =
          context.root.EffectUnitCreate(
              context.root.EffectIUnitEventMutListCreate(),
              true,
              0,
              new Location(0, 0, 0),
              "chronomancer",
              90, 90,
              100, 100,
              600,
              0,
              IUnitComponentMutBunch.New(context.root),
              true,
              5);
      preGauntletLevel.EnterUnit(
          game,
          superstate.levelSuperstate,
          player,
          downStaircaseLocation);
      game.player = player;

      return game;
    }

  }
}
