using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class WanderAICapabilityUCExtensions {
    public static Atharia.Model.Void Destruct(
        this WanderAICapabilityUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static IImpulse ProduceImpulse(
        this WanderAICapabilityUC obj,
        Game game,
        Superstate superstate,
        Unit unit) {

      var reachableLocations = Actions.GetReachableLocations(game.level, unit.location);
      var adjacentWalkableLocations = new SortedSet<Location>();
      foreach (var adjacentLocation in reachableLocations) {
        if (Actions.CanStep(game, superstate, unit, adjacentLocation)) {
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
          200,
          randomAdjacentWalkableLocation)
              .AsIImpulse();
    }
  }
}
