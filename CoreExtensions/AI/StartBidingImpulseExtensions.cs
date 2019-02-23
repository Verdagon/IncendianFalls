using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class StartBidingImpulseExtensions {
    public static int GetWeightImpl(this StartBidingImpulse obj) {
      return obj.weight;
    }

    public static Atharia.Model.Void EnactImpl(this StartBidingImpulse obj, Unit unit, Game game) {
      unit.ReplaceOperation(obj.root.EffectBidingOperationUCCreate().AsIOperationUC());
      unit.nextActionTime = unit.nextActionTime + unit.inertia;
      return new Atharia.Model.Void();
    }
  }
}
