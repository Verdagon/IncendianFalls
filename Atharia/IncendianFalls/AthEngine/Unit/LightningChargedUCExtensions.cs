using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class LightningChargedUCExtensions {
    public static Atharia.Model.Void Destruct(
        this LightningChargedUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static int GetMovementTimeAddConstant(this LightningChargedUC armor) {
      return 0;
    }
    public static int GetMovementTimeMultiplierPercent(this LightningChargedUC armor) {
      return 50;
    }
    public static bool PostAct(
        this Atharia.Model.LightningChargedUC detail,
        Game game,
        Superstate superstate,
        Unit unit) {
      unit.components.Remove(detail.AsIUnitComponent());
      detail.Destruct();
      return true;
    }
  }
}
