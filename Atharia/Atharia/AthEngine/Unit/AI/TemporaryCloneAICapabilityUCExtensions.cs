using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class TemporaryCloneAICapabilityUCExtensions {
    public static Atharia.Model.Void Destruct(
        this TemporaryCloneAICapabilityUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static IImpulse ProduceImpulse(
        this TemporaryCloneAICapabilityUC obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      if (obj.charges <= 0) {
        return obj.root.EffectNoImpulseCreate().AsIImpulse();
      }

      // Let's see if theres an enemy near.
      Unit nearestEnemy = DetermineTarget.Determine(game, superstate, unit);
      if (!nearestEnemy.Exists()) {
        // There are no enemies.
        return game.root.EffectNoImpulseCreate().AsIImpulse();
      }

      // Check if we can see them.
      if (!Sight.CanSee(game, unit, nearestEnemy.location, out List<Location> sightPath)) {
        // Can't see the enemy. Don't update directive.
        return game.root.EffectNoImpulseCreate().AsIImpulse();
      }


      var adjacentLocations =
          game.level.terrain.GetAdjacentExistingLocations(
              unit.location, game.level.terrain.considerCornersAdjacent);
      var adjacentWalkableLocations = new SortedSet<Location>();
      foreach (var adjacentLocation in adjacentLocations) {
        if (Actions.CanStep(game, superstate, unit, adjacentLocation)) {
          adjacentWalkableLocations.Add(adjacentLocation);
        }
      }
      if (adjacentWalkableLocations.Count == 0) {
        // Nowhere to summon, so don't.
        return obj.root.EffectNoImpulseCreate().AsIImpulse();
      }
      var randomAdjacentWalkableLocation = SetUtils.GetRandom(game.rand.Next(), adjacentWalkableLocations);

      return obj.root.EffectTemporaryCloneImpulseCreate(900, obj.blueprintName, randomAdjacentWalkableLocation, unit.hp).AsIImpulse();
    }
  }
}
