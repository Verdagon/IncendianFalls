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
            superstate.levelSuperstate.FindNearestLiveUnit(
                game, unit.location, unit, !unit.good);
        if (!nearestEnemy.Exists()) {
          return obj.root.EffectNoImpulseCreate().AsIImpulse();
        }

        var isNextToPlayer =
            game.level.terrain.pattern.LocationsAreAdjacent(
                nearestEnemy.location,
                unit.location,
                game.level.ConsiderCornersAdjacent());
        if (!isNextToPlayer) {
          return obj.root.EffectNoImpulseCreate().AsIImpulse();
        }

        int need = game.rand.Next() % 2 == 0 ? 800 : 400;
        return obj.root.EffectStartBidingImpulseCreate(need).AsIImpulse();
      } else {
        if (bidingOperation.charge < 3) {
          return obj.root.EffectContinueBidingImpulseCreate(800).AsIImpulse();
        } else {
          return obj.root.EffectUnleashBideImpulseCreate(800).AsIImpulse();
        }
      }
    }
  }
}
