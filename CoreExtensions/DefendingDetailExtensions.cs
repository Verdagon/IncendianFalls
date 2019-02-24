using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class ShieldingUCExtensions {
    public static Atharia.Model.Void Destruct(
        this ShieldingUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static int AffectIncomingDamage(this Atharia.Model.ShieldingUC detail, int damage) {
      int newDamage = damage / 3;
      detail.root.logger.Info("Defend:" + damage + "->" + newDamage);
      return newDamage;
    }
    public static Atharia.Model.Void PreAct(this Atharia.Model.ShieldingUC detail, Game game, Unit unit) {
      unit.components.Remove(detail.AsIUnitComponent());
      detail.Destruct();
      return new Atharia.Model.Void();
    }
    public static Atharia.Model.Void PostAct(this Atharia.Model.ShieldingUC detail, Unit unit) {
      return new Atharia.Model.Void();
    }
  }
}
