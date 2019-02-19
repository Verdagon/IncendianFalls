using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class SetupGameRequestExecutor {
    public static Game Execute(SSContext context, int randomSeed, bool squareLevelsOnly) {
      context.root.GetDeterministicHashCode();

      var rand = context.root.EffectRandCreate(randomSeed);

      context.root.GetDeterministicHashCode();

      var firstLevel =
          MakeLevel.MakeNextLevel(
              context,
              rand,
              0,
              squareLevelsOnly,
              "Ridge");

      context.root.GetDeterministicHashCode();

      var walkableLocations = new WalkableLocations(firstLevel.terrain, firstLevel.units);

      context.root.GetDeterministicHashCode();

      var player = SetupCommon.MakePlayer(context, rand, firstLevel.units, walkableLocations);

      context.root.GetDeterministicHashCode();

      var levels = context.root.EffectLevelMutBunchCreate();
      context.root.GetDeterministicHashCode();

      levels.Add(firstLevel);

      context.root.GetDeterministicHashCode();

      var game =
          context.root.EffectGameCreate(
              rand,
              squareLevelsOnly,
              levels,
              player,
              firstLevel,
              0,
              context.root.EffectExecutionStateCreate(Unit.Null, false, IDetailMutList.Null, IDetailMutList.Null));

      context.root.GetDeterministicHashCode();

      return game;
    }
  }
}
