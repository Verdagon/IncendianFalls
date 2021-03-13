using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  class DetermineTarget {
    public static Unit Determine(Game game, Superstate superstate, Unit unit) {
      var adjLocs =
          game.level.terrain.GetAdjacentExistingLocations(
              unit.location, game.level.terrain.considerCornersAdjacent);
      var adjacentEnemies = new List<Unit>();
      foreach (var adjLoc in adjLocs) {
        if (game.level.terrain.GetElevationDifference(unit.location, adjLoc) <= 2) {
          var otherUnit = superstate.levelSuperstate.GetLiveUnitAt(adjLoc);
          if (otherUnit.Exists() && unit.good != otherUnit.good) {
            // Prioritize shielding and countering units
            if (otherUnit.components.GetOnlyDefyingUCOrNull().Exists()) {
              adjacentEnemies.Insert(0, otherUnit);
            } else if (otherUnit.components.GetOnlyCounteringUCOrNull().Exists()) {
              adjacentEnemies.Insert(0, otherUnit);
            } else {
              adjacentEnemies.Add(otherUnit);
            }
          }
        }
      }
      if (adjacentEnemies.Count > 0) {
        return adjacentEnemies[0];
      }

      return superstate.levelSuperstate.FindNearestLiveUnit(
          game,
          unit.location,
          // Filter so its not this unit
          unit,
          // Opposite allegiance to unit
          !unit.good);
    }
  }
}
