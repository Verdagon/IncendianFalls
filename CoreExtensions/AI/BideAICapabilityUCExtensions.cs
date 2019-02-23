using System;
using Atharia.Model;

namespace IncendianFalls {
  public static class BideAICapabilityUCExtensions {
    public static IImpulse ProduceImpulseImpl(
        this BideAICapabilityUC obj,
        Unit unit,
        Game game) {
      var bidingOperation = unit.components.GetOnlyBidingOperationUCOrNull();
      if (bidingOperation.Exists()) {
        obj.root.logger.Info("Already biding, unleash!");
        return obj.root.EffectUnleashBideImpulseCreate(800).AsIImpulse();
      } else {
        var isNextToPlayer =
          game.level.terrain.pattern.LocationsAreAdjacent(
            game.player.location,
            unit.location,
            game.level.considerCornersAdjacent);
        if (isNextToPlayer) {
          obj.root.logger.Info("Next to player, bide!");
          return obj.root.EffectStartBidingImpulseCreate(800).AsIImpulse();
        } else {
          return obj.root.EffectNoImpulseCreate().AsIImpulse();
        }
      }
    }
  }
}
