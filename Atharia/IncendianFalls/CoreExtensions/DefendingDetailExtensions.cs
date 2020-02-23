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
    public static bool PreAct(
        this Atharia.Model.ShieldingUC detail,
        Game game,
        Superstate superstate,
        Unit unit) {
      unit.components.Remove(detail.AsIUnitComponent());
      detail.Destruct();
      return true;
    }
  }
}
