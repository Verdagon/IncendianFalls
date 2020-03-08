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

      bool playDirtRoad = false;
      bool playBackstory = false;
      bool playTutorial1 = false;
      bool playTutorial2 = false;
      int startingDepth = 0;

      Level startLevel = Level.Null;
      Location startLevelEntryLocation = null;

      Level previousLevel = Level.Null;
      Location previousLevelExitLocation = null;

      if (playDirtRoad) {
        DirtRoadLevelControllerExtensions.LoadLevel(
          out var level,
          out var levelSuperstate,
          out var entryLocation,
          out var exitLocation,
          game);
        if (!startLevel.Exists()) {
          startLevel = level;
          startLevelEntryLocation = entryLocation;
        }
        if (previousLevel.Exists()) {
          previousLevel.terrain.tiles[previousLevelExitLocation].components.Add(
            game.root.EffectLevelLinkTTCCreate(true, level, entryLocation).AsITerrainTileComponent());
        }
        previousLevel = level;
        previousLevelExitLocation = exitLocation;
      }

      if (playBackstory) {
        SotaventoLevelControllerExtensions.LoadLevel(
          out var level,
          out var levelSuperstate,
          out var entryLocation,
          out var exitLocation,
          game);
        if (!startLevel.Exists()) {
          startLevel = level;
          startLevelEntryLocation = entryLocation;
        }
        if (previousLevel.Exists()) {
          previousLevel.terrain.tiles[previousLevelExitLocation].components.Add(
            game.root.EffectLevelLinkTTCCreate(true, level, entryLocation).AsITerrainTileComponent());
        }
        previousLevel = level;
        previousLevelExitLocation = exitLocation;
      }

      if (playTutorial1) {
        Tutorial1LevelControllerExtensions.LoadLevel(
          out var level,
          out var levelSuperstate,
          out var entryLocation,
          out var exitLocation,
          game);
        if (!startLevel.Exists()) {
          startLevel = level;
          startLevelEntryLocation = entryLocation;
        }
        if (previousLevel.Exists()) {
          previousLevel.terrain.tiles[previousLevelExitLocation].components.Add(
            game.root.EffectLevelLinkTTCCreate(true, level, entryLocation).AsITerrainTileComponent());
        }
        previousLevel = level;
        previousLevelExitLocation = exitLocation;
      }

      if (playTutorial2) {
        Tutorial2LevelControllerExtensions.LoadLevel(
          out var level,
          out var levelSuperstate,
          out var entryLocation,
          out var exitLocation,
          game);
        if (!startLevel.Exists()) {
          startLevel = level;
          startLevelEntryLocation = entryLocation;
        }
        if (previousLevel.Exists()) {
          previousLevel.terrain.tiles[previousLevelExitLocation].components.Add(
            game.root.EffectLevelLinkTTCCreate(true, level, entryLocation).AsITerrainTileComponent());
        }
        previousLevel = level;
        previousLevelExitLocation = exitLocation;
      }

      game.player = Chronomancer.Make(game.root);

      if (previousLevel.Exists()) {
        previousLevel.terrain.tiles[previousLevelExitLocation].components.Add(
          game.root.EffectEmberDeepLevelLinkerTTCCreate(startingDepth).AsITerrainTileComponent());
      } else {
        EmberDeepLevelLinkerTTCExtensions.MakeNextLevel(
          out var level,
          out var levelSuperstate,
          out var entryLocation,
          game,
          superstate,
          startingDepth);
        if (!startLevel.Exists()) {
          startLevel = level;
          startLevelEntryLocation = entryLocation;
        }
        previousLevel = level;
        previousLevelExitLocation = null;
      }

      Asserts.Assert(startLevel.Exists());

      game.root.logger.Info("starting level: " + startLevel.controller.GetName());
      LevelLinkTTCExtensions.Travel(game, superstate, game.player, startLevel, startLevelEntryLocation, false);

      game.level.controller.SimpleTrigger(game, superstate, "firstLevelStart");

      return game;
    }
  }
}
