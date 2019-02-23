using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class ShieldingUCExtensions {
    public static int AffectIncomingDamageImpl(this Atharia.Model.ShieldingUC detail, int damage) {
      int newDamage = damage / 3;
      detail.root.logger.Info("Defend:" + damage + "->" + newDamage);
      return newDamage;
    }
    public static Atharia.Model.Void PreActImpl(this Atharia.Model.ShieldingUC detail, Game game, Unit unit) {
      unit.components.Remove(detail.AsIUnitComponent());
      detail.Delete();
      return new Atharia.Model.Void();
    }
    public static Atharia.Model.Void PostActImpl(this Atharia.Model.ShieldingUC detail, Unit unit) {
      return new Atharia.Model.Void();
    }
  }
}
