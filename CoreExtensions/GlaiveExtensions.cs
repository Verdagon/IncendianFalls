using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class GlaiveExtensions {
    public static int AffectIncomingDamageImpl(this Glaive glaive, int damage) {
      return damage;
    }
    public static int AffectOutgoingDamageImpl(this Glaive glaive, int damage) {
      int newDamage = damage + 10;
      glaive.root.logger.Info("Glaive:" + damage + "->" + newDamage);
      return newDamage;
    }
  }
}
