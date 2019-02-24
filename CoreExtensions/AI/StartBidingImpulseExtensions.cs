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

    public static Atharia.Model.Void Enact(this StartBidingImpulse obj, Unit unit, Game game) {
      unit.ReplaceOperation(obj.root.EffectBidingOperationUCCreate().AsIOperationUC());
      unit.nextActionTime = unit.nextActionTime + unit.inertia;
      return new Atharia.Model.Void();
    }
  }
}
