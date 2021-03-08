using Atharia.Model;
using Gauntlet;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  public static class SetupGauntletGameRequestExecutor {
    public static Game Execute(
        SSContext context,
        out Superstate superstate,
        SetupGauntletGameRequest request) {
      return SetupGauntletGame.SetupGame(context, out superstate, request.randomSeed, request.squareLevelsOnly);
    }
  }
}
