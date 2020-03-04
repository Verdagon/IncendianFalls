using Atharia.Model;
using Gauntlet;
using EmberDeep;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  public static class SetupEmberDeepGameRequestExecutor {
    public static Game Execute(
        SSContext context,
        out Superstate superstate,
        SetupEmberDeepGameRequest request) {
      return SetupEmberDeepGame.SetupGame(
        context,
        out superstate,
        request.randomSeed,
        request.squareLevelsOnly);
    }

  }
}
