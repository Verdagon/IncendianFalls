using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class NoImpulseExtensions {
    public static int GetWeightImpl(this NoImpulse obj) {
      return 0;
    }

    public static Atharia.Model.Void EnactImpl(this NoImpulse obj, Unit unit, Game game) {
      unit.nextActionTime = unit.nextActionTime + unit.inertia / 2;
      return new Atharia.Model.Void();
    }
  }
}
