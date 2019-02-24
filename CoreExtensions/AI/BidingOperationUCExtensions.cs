using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class BidingOperationUCExtensions {

    public static Atharia.Model.Void Destruct(
        this BidingOperationUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int AffectIncomingDamage(this BidingOperationUC obj, int incomingDamage) {
      return incomingDamage * 3 / 2;
    }

    public static Atharia.Model.Void OnImpulse(this BidingOperationUC obj, Unit unit, Game game, IImpulse impulse) {
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
