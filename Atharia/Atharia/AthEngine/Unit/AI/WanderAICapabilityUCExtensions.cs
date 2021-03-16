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

      var hoppableLocs = superstate.levelSuperstate.GetHoppableLocs(unit.location, true);
      if (hoppableLocs.Count == 0) {
        // Nowhere to walk, so can't wander.
        return obj.root.EffectNoImpulseCreate().AsIImpulse();
      }

      var randomSteppableLoc = SetUtils.GetRandom(game.rand.Next(), hoppableLocs);

      return obj.root.EffectMoveImpulseCreate(200, randomSteppableLoc).AsIImpulse();
    }
  }
}
