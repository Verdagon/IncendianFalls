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

    public static Atharia.Model.Void BeforeImpulse(
        this BidingOperationUC obj,
        Game game,
        Superstate superstate,
        Unit unit,
        IImpulse impulse) {
      if (impulse is StartBidingImpulseAsIImpulse bide) {
        // Gooooood... proceed.
      } else if (impulse is ContinueBidingImpulseAsIImpulse cont) {
        // Gooooood... proceed.
      } else if (impulse is UnleashBideImpulseAsIImpulse unleash) {
        // Gooooood... proceed.
      } else {
        // They're doing something other than unleashing the bide, cancel this operation.
        unit.ClearOperation();
      }
      return new Atharia.Model.Void();
    }
  }
}
