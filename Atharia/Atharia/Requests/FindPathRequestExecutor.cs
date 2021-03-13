using Atharia.Model;
using Gauntlet;
using EmberDeep;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  public static class FindPathRequestExecutor {
    public static List<Location> Execute(
        SSContext context,
        Superstate superstate,
        FindPathRequest request) {
      int gameId = request.gameId;
      int unitId = request.unitId;
      Location destination = request.destination;

      var game = context.root.GetGame(gameId);
      var unit = context.root.GetUnit(unitId);
      
      var explorer =
          new AStarExplorer(
              new SortedSet<Location> {unit.location},
              (a) => superstate.levelSuperstate.GetReachableLocations(a),
              (a, b, totalCost) => true,
              (a) => a == destination,
              AStarExplorer.MakeDistanceCostGuesser(game.level.terrain.pattern, destination),
              (a, b) => {
                if (game.level.terrain.pattern.LocationsAreAdjacent(a, b, game.level.terrain.considerCornersAdjacent)) {
                  return 1;
                } else {
                  return Actions.LEAP_DISTANCE + 1;
                }
              });
      if (explorer.getClosedLocations().Contains(destination)) {
        return explorer.GetPathTo(destination);
      } else {
        return new List<Location>();
      }
    }
  }
}
