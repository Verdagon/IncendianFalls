using System;
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
        Game game,
        LevelSuperstate levelSuperstate,
        Unit unit,
        Level fromLevel,
        int fromLevelPortalIndex) {
      game.root.logger.Warning("enterunit, " + fromLevel.id + " " + fromLevelPortalIndex);
      var entryLocation =
          obj.controller.GetEntryLocation(
              game, levelSuperstate, fromLevel, fromLevelPortalIndex);
      unit.location = entryLocation;

      obj.units.Add(unit);
      levelSuperstate.Add(unit);
    }

    public static void ExitUnit(
        this Level obj,
        Game game,
        LevelSuperstate levelSuperstate,
        Unit unit) {
      levelSuperstate.Remove(unit);
      game.level.units.Remove(unit);
    }
  }
}
