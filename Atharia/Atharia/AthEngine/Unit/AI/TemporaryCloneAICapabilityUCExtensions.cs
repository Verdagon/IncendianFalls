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
      if (!superstate.levelSuperstate.CanSee(unit.location, nearestEnemy.location)) {
        // Can't see the enemy. Don't update directive.
        return game.root.EffectNoImpulseCreate().AsIImpulse();
      }


      var adjacentSteppableLocs =
          superstate.levelSuperstate.GetHoppableLocs(unit.location, true);
      if (adjacentSteppableLocs.Count == 0) {
        // Nowhere to summon, so don't.
        return obj.root.EffectNoImpulseCreate().AsIImpulse();
      }
      var randomAdjacentWalkableLocation = SetUtils.GetRandom(game.rand.Next(), adjacentSteppableLocs);

      return obj.root.EffectTemporaryCloneImpulseCreate(900, obj.blueprintName, randomAdjacentWalkableLocation, unit.hp).AsIImpulse();
    }
  }
}
