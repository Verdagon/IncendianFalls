using Atharia.Model;
using Gauntlet;
using EmberDeep;
using System;
using System.Collections.Generic;
using System.Text;
using RavaArcana;

namespace IncendianFalls {
  public static class SetupRavaArcanaGameRequestExecutor {
    public static Game Execute(
        SSContext context,
        out Superstate superstate,
        SetupRavaArcanaGameRequest request) {
      return SetupRavaArcanaGame.SetupGame(
        context,
        out superstate,
        request.randomSeed,
        request.startLevel,
        request.squareLevelsOnly);
    }
  }
}
