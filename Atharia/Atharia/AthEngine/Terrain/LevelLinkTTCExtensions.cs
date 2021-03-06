﻿using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class LevelLinkTTCExtensions {
    public static Atharia.Model.Void Destruct(this LevelLinkTTC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static string Interact(
        this LevelLinkTTC levelLink,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        Unit interactingUnit,
        Location containingTileLocation) {
      Travel(context, game, superstate, interactingUnit, levelLink.destinationLevel, levelLink.destinationLevelLocation, levelLink.destroyThisLevel);

      return "";
    }

    public static void Travel(
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        Unit unit,
        Level destinationLevel,
        Location destinationLevelLocation,
        bool destroyPreviousLevel) {
      var previousLevel = game.level;

      if (previousLevel.Exists()) {
        game.level.ExitUnit(game, superstate.levelSuperstate, unit);
      }

      game.level = destinationLevel;
      superstate.levelSuperstate = new LevelSuperstate(game.level);

      if (previousLevel.Exists()) {
        if (destroyPreviousLevel) {
          game.levels.Remove(previousLevel);
          previousLevel.Destruct();
        }
      }


      // These will likely be in the distant past, since it's been a while since we've
      // visited here. We'll want to bump them all up to the near future.
      Asserts.Assert(game.time >= game.level.time);

      int levelLastTime = game.level.time;
      int timeNow = game.time;
      int timeSinceLevelLastTime = timeNow - levelLastTime;
      game.level.time = game.time;
      // Add that amount to every unit, so it's as if we just left this level.
      foreach (var nativeUnit in game.level.units) {
        nativeUnit.nextActionTime =
            nativeUnit.nextActionTime + timeSinceLevelLastTime;
      }

      // Make player start immediately.
      game.level.EnterUnit(superstate.levelSuperstate, destinationLevelLocation, game.level.time, unit);

      game.level.controller.SimpleTrigger(context, game, superstate, "levelStart");
    }
  }
}
