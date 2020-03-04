using Atharia.Model;
using IncendianFalls;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmberDeep {
  class SetupEmberDeepGame {
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

      RidgeLevelControllerExtensions.LoadLevel(
        out var firstLevel,
        out var firstLevelSuperstate,
        out var ridgeEntryLocation,
        out var ridgeExitLocation,
        game,
        superstate);

      TutorialLevelControllerExtensions.LoadLevel(
        out var tutorialLevel, out var tutorialLevelSuperstate, out var tutorialEntryLocation, game);

      //EmberDeepLevelLinkerTTCExtensions.MakeNextLevel(
      //    out var firstLevel,
      //    out var firstLevelSuperstate,
      //    out var entryLocation,
      //    game,
      //    superstate);

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

      game.level.controller.SimpleTrigger(game, superstate, "levelStart");

      return game;
    }
  }
}
