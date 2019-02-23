using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class WanderAICapabilityUCExtensions {
    public static IImpulse ProduceImpulseImpl(
        this WanderAICapabilityUC obj,
        Unit unit,
        Game game) {

      if (unit.GetDirectiveOrNull().Exists()) {
        // If there's a directive, we don't want to just be wandering.
        return obj.root.EffectNoImpulseCreate().AsIImpulse();
      } else {
        var liveUnitByLocationMap = new LiveUnitByLocationMap(game);

        var adjacentLocations =
            game.level.terrain.pattern.GetAdjacentLocations(
                unit.location, game.level.considerCornersAdjacent);
        var adjacentWalkableLocations = new SortedSet<Location>();
        foreach (var adjacentLocation in adjacentLocations) {
          if (Actions.CanStep(game, liveUnitByLocationMap, unit, adjacentLocation)) {
            adjacentWalkableLocations.Add(adjacentLocation);
          }
        }

        if (adjacentWalkableLocations.Count == 0) {
          // Nowhere to walk, so can't wander.
          return obj.root.EffectNoImpulseCreate().AsIImpulse();
        }

        var randomAdjacentWalkableLocation =
          SetUtils.GetRandom(game.rand.Next(), adjacentWalkableLocations);

        return obj.root.EffectMoveImpulseCreate(
            200, randomAdjacentWalkableLocation)
            .AsIImpulse();
      }
    }
  }
}
