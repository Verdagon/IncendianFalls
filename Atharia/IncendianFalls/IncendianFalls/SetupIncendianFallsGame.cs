using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  class SetupIncendianFallsGame {
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
              context.root.EffectExecutionStateCreate(
                  Unit.Null,
                  false,
                  IPreActingUCWeakMutBunch.Null,
                  IPostActingUCWeakMutBunch.Null),
              
              "",
              false,

              context.root.EffectIGameEventMutListCreate(new List<IGameEvent>()),
              context.root.EffectUnitWeakMutSetCreate(),
              context.root.EffectTerrainTileWeakMutSetCreate());

      superstate =
          new Superstate(
            game,
            null,
            new List<RootIncarnation>(),
            new List<IRequest>(),
            new List<int>(),
            null,
            null);

      IncendianFallsLevelLinkerTTCExtensions.MakeNextLevel(
          out var firstLevel,
          out var firstLevelSuperstate,
          out var entryLocation,
          game,
          superstate,
          0);
      game.level = firstLevel;
      superstate.levelSuperstate = firstLevelSuperstate;

      var player = Chronomancer.Make(context.root);
      firstLevel.EnterUnit(
          superstate.levelSuperstate,
          entryLocation,
          0,
          player);
      game.player = player;

      return game;
    }
  }
}
