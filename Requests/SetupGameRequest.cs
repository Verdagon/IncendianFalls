using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class SetupGameRequestExecutor {
    public static Game Execute(Root root, int randomSeed, bool squareLevelsOnly) {
      var rand = root.EffectRandCreate(randomSeed);

      var firstLevel = MakeLevel.MakeNextLevel(root, rand, squareLevelsOnly, "Ridge");

      var walkableLocations = new WalkableLocations(firstLevel.terrain, firstLevel.units);

      var player = SetupCommon.MakePlayer(root, rand, firstLevel.units, walkableLocations);

      var levels = root.EffectLevelMutBunchCreate();
      levels.Add(firstLevel);

      var game =
          root.EffectGameCreate(
              rand,
              squareLevelsOnly,
              levels,
              player,
              firstLevel,
              0,
              root.EffectExecutionStateCreate(Unit.Null, false, IDetailMutList.Null, IDetailMutList.Null));

      game.levels.Add(firstLevel);

      return game;
    }
  }
}
