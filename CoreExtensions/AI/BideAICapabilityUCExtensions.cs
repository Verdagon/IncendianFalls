using System;
using Atharia.Model;

namespace IncendianFalls {
  public static class BideAICapabilityUCExtensions {
    public static IImpulse ProduceImpulseImpl(
        this BideAICapabilityUC obj,
        Unit unit,
        Game game) {
      var isNextToPlayer =
        game.level.terrain.pattern.LocationsAreAdjacent(
          game.player.location,
          unit.location,
          game.level.considerCornersAdjacent);
      if (isNextToPlayer) {
        return obj.root.EffectBideImpulseCreate(800).AsIImpulse();
      } else {
        return obj.root.EffectNoImpulseCreate().AsIImpulse();
      }
    }
  }
}
