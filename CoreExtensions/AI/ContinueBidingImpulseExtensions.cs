using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class ContinueBidingImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this ContinueBidingImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static int GetWeight(this ContinueBidingImpulse obj) {
      return obj.weight;
    }

    public static bool Enact(
        this ContinueBidingImpulse obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      if (unit.GetOperationOrNull() is BidingOperationUCAsIOperationUC bidingI) {
        bidingI.obj.charge = bidingI.obj.charge + 1;
      } else {
        Asserts.Assert(false);
      }
      unit.nextActionTime = unit.nextActionTime + unit.CalculateInertia();
      return false;
    }
  }
}
