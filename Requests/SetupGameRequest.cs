using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class SetupGameRequestExecutor {
    public static Game Execute(
        SSContext context,
        out Superstate superstate,
        SetupGameRequest request) {
      int randomSeed = request.randomSeed;
      bool squareLevelsOnly = request.squareLevelsOnly;

      var rand = context.root.EffectRandCreate(randomSeed);

      var firstLevel =
          MakeLevel.MakeNextLevel(
              context,
              rand,
              0,
              squareLevelsOnly,
              "Ridge");

      var walkableLocations = new WalkableLocations(firstLevel.terrain, firstLevel.units);

      var player = SetupCommon.MakePlayer(context, rand, firstLevel.units, walkableLocations);

      var levels = context.root.EffectLevelMutSetCreate();
      levels.Add(firstLevel);

      var game =
          context.root.EffectGameCreate(
              rand,
              squareLevelsOnly,
              levels,
              player,
              NullIRequest.Null,
              firstLevel,
              0,
              context.root.EffectExecutionStateCreate(Unit.Null, false, IPreActingUCWeakMutBunch.Null, IPostActingUCWeakMutBunch.Null));

      superstate =
          new Superstate(new LiveUnitByLocationMap(game), new List<RootIncarnation>(), 0);

      return game;
    }
  }
}
