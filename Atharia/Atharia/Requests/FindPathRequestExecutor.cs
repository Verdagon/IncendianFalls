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

      return Actions.GetPathTo(game, superstate, unit, destination);
    }
  }
}
