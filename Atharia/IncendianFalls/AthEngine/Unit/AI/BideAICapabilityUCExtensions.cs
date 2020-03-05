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
        this BideAICapabilityUC self,
        Game game,
        Superstate superstate,
        Unit unit) {
      if (self.charge == 0) {
        var nearestEnemy =
            superstate.levelSuperstate.FindNearestLiveUnit(
                game, unit.location, unit, !unit.good);
        if (!nearestEnemy.Exists()) {
          return self.root.EffectNoImpulseCreate().AsIImpulse();
        }

        var isNextToPlayer =
            game.level.terrain.pattern.LocationsAreAdjacent(
                nearestEnemy.location,
                unit.location,
                game.level.ConsiderCornersAdjacent());
        if (!isNextToPlayer) {
          return self.root.EffectNoImpulseCreate().AsIImpulse();
        }

        int need = game.rand.Next() % 2 == 0 ? 800 : 400;
        return self.root.EffectStartBidingImpulseCreate(need).AsIImpulse();
      } else if (self.charge < 3) {
        return self.root.EffectContinueBidingImpulseCreate(800).AsIImpulse();
      } else {
        return self.root.EffectUnleashBideImpulseCreate(800).AsIImpulse();
      }
    }

    public static Atharia.Model.Void BeforeImpulse(
        BideAICapabilityUC self,
        Game game,
        Superstate superstate,
        Unit unit,
        IAICapabilityUC originatingCapability,
        IImpulse impulse) {
      if (!originatingCapability.NullableIs(self.AsIAICapabilityUC()) &&
          self.charge > 0) {
        // They're doing something other than unleashing the bide, cancel this operation.
        self.charge = 0;
      }
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void AfterImpulse(
        BideAICapabilityUC self,
        Game game,
        Superstate superstate,
        Unit unit,
        IAICapabilityUC originatingCapability,
        IImpulse impulse) {
      return new Atharia.Model.Void();
    }

    public static int GetIncomingDamageAddConstant(this BideAICapabilityUC self) {
      return 0;
    }
    public static int GetIncomingDamageMultiplierPercent(this BideAICapabilityUC self) {
      if (self.charge > 0) {
        return 150;
      }
      return 100;
    }
  }
}
