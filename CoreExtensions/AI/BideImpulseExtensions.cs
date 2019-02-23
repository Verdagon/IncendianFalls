using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class BideImpulseExtensions {
    public static int GetWeightImpl(this BideImpulse obj) {
      return 0;
    }

    public static Atharia.Model.Void EnactImpl(this BideImpulse obj, Unit unit, Game game) {
      // TODO: do a bide thing
      unit.nextActionTime = unit.nextActionTime / 2;
      return new Atharia.Model.Void();
    }
  }
}
