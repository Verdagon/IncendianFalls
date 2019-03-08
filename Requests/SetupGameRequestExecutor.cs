using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class SetupGameRequestExecutor {
    public static Game Execute(
        SSContext context,
        out Superstate superstate,
        SetupGameRequest request) {
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
              NullIRequest.Null,
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
            new List<int>(),
            null);

      MakeLevel.MakeNextLevel(
          out var firstLevel,
          out var firstLevelSuperstate,
          context,
          game,
          superstate,
          Level.Null,
          0);
      game.levels.Add(firstLevel);
      game.level = firstLevel;
      superstate.levelSuperstate = firstLevelSuperstate;

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
      firstLevel.EnterUnit(
          game,
          superstate.levelSuperstate,
          player,
          Level.Null,
          1);
      game.player = player;

      return game;
    }
  }
}
