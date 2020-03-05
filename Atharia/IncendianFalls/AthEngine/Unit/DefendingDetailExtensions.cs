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
    public static int GetIncomingDamageAddConstant(this Atharia.Model.ShieldingUC detail) {
      return 0;
    }
    public static int GetIncomingDamageMultiplierPercent(this Atharia.Model.ShieldingUC detail) {
      return 33;
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
