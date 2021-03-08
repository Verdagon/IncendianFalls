using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class SetupIncendianFallsGameRequestExecutor {
    public static Game Execute(
        SSContext context,
        out Superstate superstate,
        SetupIncendianFallsGameRequest request) {
      return SetupIncendianFallsGame.SetupGame(context, out superstate, request.randomSeed, request.squareLevelsOnly);
    }
  }
}
