using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class BidingOperationUCExtensions {

    public static int AffectIncomingDamageImpl(this BidingOperationUC obj, int incomingDamage) {
      return incomingDamage * 3 / 2;
    }

    public static Atharia.Model.Void OnImpulseImpl(this BidingOperationUC obj, Unit unit, Game game, IImpulse impulse) {
      if (impulse is UnleashBideImpulse unleash) {
        // Gooooood... proceed.
      } else {
        // They're doing something other than unleashing the bide, cancel this operation.
        unit.ClearOperation();
      }
      return new Atharia.Model.Void();
    }
  }
}
