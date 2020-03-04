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
              context.root.EffectIGameEventMutListCreate(new List<IGameEvent>()),
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

      IncendianFallsLevelLinkerTTCExtensions.MakeNextLevel(
          out var firstLevel,
          out var firstLevelSuperstate,
          out var entryLocation,
          game,
          superstate,
          0);
      game.level = firstLevel;
      superstate.levelSuperstate = firstLevelSuperstate;

      var player =
          context.root.EffectUnitCreate(
              context.root.EffectIUnitEventMutListCreate(),
              true,
              0,
              new Location(0, 0, 0),
              "Chronomancer",
              90, 90,
              100, 100,
              600,
              0,
              IUnitComponentMutBunch.New(context.root),
              true,
              5);
      firstLevel.EnterUnit(
          superstate.levelSuperstate,
          player,
          entryLocation);
      game.player = player;

      game.events.Add(new NarrateEvent("The Incendian Falls! I've finally made it.\nIf I can find Volcaetus, I can save my brother!").AsIGameEvent());

      return game;
    }
  }
}
