﻿using System;
using System.Collections.Generic;

namespace Atharia.Model {
  /*
	struct Terrain node(NobiliaModel) {
	  pattern: Pattern;
	  tileset: Tileset;
	  elevationStepHeight: F32;

	  tiles: !Map:(Location, TerrainTile, locationBefore);
	}
	*/

  public static class LevelExtensions {
    public static Atharia.Model.Void Destruct(
        this Level obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static string GetName(this Level obj) {
      Asserts.Assert(obj.controller.Exists());
      return obj.controller.GetName();
    }

    public static bool ConsiderCornersAdjacent(this Level obj) {
      Asserts.Assert(obj.controller.Exists());
      return obj.controller.ConsiderCornersAdjacent();
    }

    public static void EnterUnit(
        this Level obj,
        LevelSuperstate levelSuperstate,

        Location location,
        // This param is just in here to make us think about it.
        // If it's proving irksome, just provide unit.nextActionTime.
        int nextActionTime,
        Unit unit) {
      unit.location = location;
      unit.nextActionTime = nextActionTime;
      obj.units.Add(unit);
      levelSuperstate.AddUnit(unit);
    }

    public static void ExitUnit(
        this Level obj,
        Game game,
        LevelSuperstate levelSuperstate,
        Unit unit) {
      levelSuperstate.RemoveUnit(unit);
      game.level.units.Remove(unit);
    }
  }
}
