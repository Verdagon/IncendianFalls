using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class StartBidingImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this StartBidingImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static int GetWeight(this StartBidingImpulse obj) {
      return obj.weight;
    }

    public static bool Enact(
        this StartBidingImpulse obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      // Start with 1 charge
      unit.ReplaceOperation(
          obj.root.EffectBidingOperationUCCreate(1)
              .AsIOperationUC());
      unit.nextActionTime = unit.nextActionTime + unit.CalculateInertia();
      return false;
    }
  }
}
