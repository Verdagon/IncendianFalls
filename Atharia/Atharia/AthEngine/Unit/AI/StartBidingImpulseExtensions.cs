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

    public static Atharia.Model.Void Enact(
        this StartBidingImpulse obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      var capability = unit.components.GetOnlyBideAICapabilityUCOrNull();
      Asserts.Assert(capability.Exists());
      capability.charge = 1;
      unit.nextActionTime = unit.nextActionTime + unit.CalculateCombatTimeCost(600);
      return new Atharia.Model.Void();
    }
  }
}
