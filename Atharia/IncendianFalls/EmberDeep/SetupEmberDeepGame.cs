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
        int startingDepth,
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
              context.root.EffectUnitWeakMutSetCreate(),
              context.root.EffectTerrainTileWeakMutSetCreate(),
              Level.Null,
              "",
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

      Level startLevel = Level.Null;
      Location startLevelEntryLocation = null;

      Level previousLevel = Level.Null;
      Location previousLevelExitLocation = null;

      if (startingDepth <= -5) {
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

      if (startingDepth <= -4) {
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

      if (startingDepth <= -3) {
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

      if (startingDepth <= -2) {
        RetreatLevelControllerExtensions.LoadLevel(
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

      if (startingDepth <= -1) {
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
          game.root.EffectEmberDeepLevelLinkerTTCCreate(0).AsITerrainTileComponent());
      } else {
        EmberDeepLevelLinkerTTCExtensions.MakeNextLevel(
          out var level,
          out var levelSuperstate,
          out var entryLocation,
          game,
          superstate,
          0);
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

      return game;
    }
  }
}
