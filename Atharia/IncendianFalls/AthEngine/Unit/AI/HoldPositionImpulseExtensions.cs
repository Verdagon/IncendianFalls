using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class HoldPositionImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this HoldPositionImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetWeight(this HoldPositionImpulse obj) {
      return obj.weight;
    }

    public static Atharia.Model.Void Enact(
        this HoldPositionImpulse obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      unit.nextActionTime = unit.nextActionTime + obj.duration;
      return new Atharia.Model.Void();
    }
  }
}
