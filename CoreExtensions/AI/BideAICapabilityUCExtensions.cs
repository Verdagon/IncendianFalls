using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class BideAICapabilityUCExtensions {
    public static Atharia.Model.Void Destruct(
        this BideAICapabilityUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static IImpulse ProduceImpulse(
        this BideAICapabilityUC obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      var bidingOperation = unit.components.GetOnlyBidingOperationUCOrNull();
      if (!bidingOperation.Exists()) {
        var nearestEnemy =
            superstate.liveUnitByLocationMap.FindNearestLiveUnit(
                game, unit.location, unit, !unit.good);
        if (!nearestEnemy.Exists()) {
          return obj.root.EffectNoImpulseCreate().AsIImpulse();
        }

        var isNextToPlayer =
            game.level.terrain.pattern.LocationsAreAdjacent(
                nearestEnemy.location,
                unit.location,
                game.level.considerCornersAdjacent);
        if (!isNextToPlayer) {
          return obj.root.EffectNoImpulseCreate().AsIImpulse();
        }

        return obj.root.EffectStartBidingImpulseCreate(800).AsIImpulse();
      } else {
        if (bidingOperation.charge < 2) {
          return obj.root.EffectContinueBidingImpulseCreate(800).AsIImpulse();
        } else {
          return obj.root.EffectUnleashBideImpulseCreate(800).AsIImpulse();
        }
      }
    }
  }
}
