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

    public static Void Enact(
        this ContinueBidingImpulse obj,
        Game game,
        Superstate superstate,
        Unit unit) {

      var capability = unit.components.GetOnlyBideAICapabilityUCOrNull();
      Asserts.Assert(capability.Exists());
      capability.charge = capability.charge + 1;
      unit.nextActionTime = unit.nextActionTime + unit.CalculateCombatTimeCost(600);
      return new Atharia.Model.Void();
    }
  }
}
