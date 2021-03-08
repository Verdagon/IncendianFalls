using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class Sight {
    public static bool CanSee(Game game, Unit unit, Location destinationLoc, out List<Location> outPath) {
      var unitPos = game.level.terrain.pattern.GetTileCenter(unit.location);
      var destinationPos = game.level.terrain.pattern.GetTileCenter(destinationLoc);
      if (unitPos.distance(destinationPos) <= unit.CalculateSightRange()) {
        if (CanSee(game, unit.location, destinationLoc, out outPath)) {
          return true;
        }
      }
      outPath = new List<Location>();
      return false;
    }

    private static bool CanSee(Game game, Location from, Location to, out List<Location> outPath) {
      var path =
          GetOptimisticDriveTo(
              game, from, to, false);
      foreach (var step in path) {
        if (!game.level.terrain.tiles.ContainsKey(step)) {
          outPath = new List<Location>();
          return false;
        }
        if (game.level.terrain.tiles[step].BlocksSight()) {
          outPath = new List<Location>();
          return false;
        }
      }
      outPath = path;
      return true;
    }

    // Optimistic in that it assumes nothing's in the way
    private static List<Location> GetOptimisticDriveTo(
        Game game,
        Location from,
        Location to,
        bool includeStart) {
      return PatternDriver.Drive(
          game.level.terrain.pattern,
          game.level.ConsiderCornersAdjacent(),
          from,
          to,
          includeStart);
    }
  }
}
