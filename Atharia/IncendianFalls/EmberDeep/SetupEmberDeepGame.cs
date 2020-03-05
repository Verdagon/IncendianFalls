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

      bool playBackstory = false;
      bool playTutorial = false;
      bool playGame = true;
      int startingDepth = 2;

      Level startLevel = Level.Null;
      LevelSuperstate startLevelSuperstate = null;
      Location startLevelEntryLocation = null;

      Level previousLevel = Level.Null;
      Location previousLevelExitLocation = null;

      if (playBackstory) {
        RidgeLevelControllerExtensions.LoadLevel(
          out var level,
          out var levelSuperstate,
          out var entryLocation,
          out var exitLocation,
          game.root);
        if (!startLevel.Exists()) {
          startLevel = level;
          startLevelSuperstate = levelSuperstate;
          startLevelEntryLocation = entryLocation;
        }
        if (previousLevel.Exists()) {
          previousLevel.terrain.tiles[previousLevelExitLocation].components.Add(
            game.root.EffectLevelLinkTTCCreate(level, entryLocation).AsITerrainTileComponent());
        }
        previousLevel = level;
        previousLevelExitLocation = exitLocation;
      }

      if (playTutorial) {
        TutorialLevelControllerExtensions.LoadLevel(
          out var level,
          out var levelSuperstate,
          out var entryLocation,
          out var exitLocation,
          game.root);
        if (!startLevel.Exists()) {
          startLevel = level;
          startLevelSuperstate = levelSuperstate;
          startLevelEntryLocation = entryLocation;
        }
        if (previousLevel.Exists()) {
          previousLevel.terrain.tiles[previousLevelExitLocation].components.Add(
            game.root.EffectLevelLinkTTCCreate(level, entryLocation).AsITerrainTileComponent());
        }
        previousLevel = level;
        previousLevelExitLocation = exitLocation;
      }

      if (playGame) {
        game.root.logger.Info("in setup!");
        EmberDeepLevelLinkerTTCExtensions.MakeNextLevel(
            out var level,
            out var levelSuperstate,
            out var entryLocation,
            game,
            superstate,
            startingDepth);
        Location exitLocation = null;
        if (!startLevel.Exists()) {
          startLevel = level;
          startLevelSuperstate = levelSuperstate;
          startLevelEntryLocation = entryLocation;
        }
        if (previousLevel.Exists()) {
          previousLevel.terrain.tiles[previousLevelExitLocation].components.Add(
            game.root.EffectLevelLinkTTCCreate(level, entryLocation).AsITerrainTileComponent());
        }
        previousLevel = level;
        previousLevelExitLocation = exitLocation;
      }

      Asserts.Assert(startLevel.Exists());

      game.root.logger.Info("setup derp!");
      game.level = startLevel;
      superstate.levelSuperstate = startLevelSuperstate;

      var player = Chronomancer.Make(game.root);
      game.level.EnterUnit(
          superstate.levelSuperstate,
          startLevelEntryLocation,
          0,
          player);
      game.player = player;

      game.root.logger.Info("sending trigger!");
      game.level.controller.SimpleTrigger(game, superstate, "levelStart");

      return game;
    }
  }
}
