using Atharia.Model;
using IncendianFalls;
using System;
using System.Collections.Generic;
using System.Text;

namespace RavaArcana {
  class SetupRavaArcanaGame {
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

      Level startLevel = Level.Null;
      Location startLevelEntryLocation = null;

      Level previousLevel = Level.Null;
      Location previousLevelExitLocation = null;

      game.player = Chronomancer.Make(game.root);

      if (previousLevel.Exists()) {
        previousLevel.terrain.tiles[previousLevelExitLocation].components.Add(
          game.root.EffectRavaArcanaLevelLinkerTTCCreate(0).AsITerrainTileComponent());
      } else {
        RavaArcanaLevelLinkerTTCExtensions.MakeNextLevel(
          out var level,
          out var levelSuperstate,
          out var entryLocation,
          context,
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
      LevelLinkTTCExtensions.Travel(context, game, superstate, game.player, startLevel, startLevelEntryLocation, false);

      return game;
    }
  }
}
