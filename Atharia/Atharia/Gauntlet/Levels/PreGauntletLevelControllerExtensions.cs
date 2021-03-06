﻿using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class PreGauntletLevelControllerExtensions {
    public static Atharia.Model.Void Destruct(this PreGauntletLevelController self) {
      self.Delete();
      return new Atharia.Model.Void();
    }

    public static void MakeLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location exitLocation,
        SSContext context,
        Root root,
        bool squareLevelsOnly,
        Rand rand,
        int time) {

      context.logger.Info("in makelevel " + squareLevelsOnly);

      var terrain =
          CircleTerrainGenerator.Generate(
            context,
            root,
            squareLevelsOnly ? SquarePattern.MakeSquarePattern() : HexPattern.MakeHexPattern(),
            false,
            rand,
            4.0f);

      var units = root.EffectUnitMutSetCreate();

      level =
          root.EffectLevelCreate(
          new Vec3(-5, 8, 10),
              terrain, units, NullILevelController.Null, time);
      levelSuperstate = new LevelSuperstate(level);

      var controller = root.EffectPreGauntletLevelControllerCreate(level);
      level.controller = controller.AsILevelController();

      IncendianFallsUnitsAndItems.PlaceItems(
          rand, level, levelSuperstate, 0, new Location(0, 0, 0), .1f, .1f);

      var downStaircaseLocation = new Location(0, 0, 0);
      var downStaircaseTile = level.terrain.tiles[downStaircaseLocation];
      downStaircaseTile.components.Add(
        root.EffectDownStairsTTCCreate().AsITerrainTileComponent());

      exitLocation = downStaircaseLocation;
    }

    public static string GetName(this PreGauntletLevelController obj) {
      return "PreGauntlet Lair";
    }

    //public static Location GetEntryLocation(
    //    this PreGauntletLevelController obj,
    //    Game game,
    //    LevelSuperstate levelSuperstate,
    //    Level fromLevel, int fromLevelPortalIndex) {
    //  var level = obj.level;
    //  if (fromLevel.NullableIs(level)) {
    //    return levelSuperstate.GetNRandomWalkableLocations(
    //        obj.level.terrain, rand, 1, true, true)[0];
    //  } else {
    //    return new Location(0, 0, 0);
    //  }
    //}

    public static Atharia.Model.Void SimpleTrigger(
        this PreGauntletLevelController obj,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        string triggerName) {
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this PreGauntletLevelController obj,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      return new Atharia.Model.Void();
    }
  }
}
