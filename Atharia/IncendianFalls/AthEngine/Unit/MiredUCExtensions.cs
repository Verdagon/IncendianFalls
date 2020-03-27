using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class MiredUCExtensions {
    public static Atharia.Model.Void Destruct(
        this MiredUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static Void PreAct(
        this Atharia.Model.MiredUC detail,
        Game game,
        Superstate superstate,
        Unit unit) {
      unit.components.Remove(detail.AsIUnitComponent());
      detail.Destruct();
      return new Atharia.Model.Void();
    }
  }
}
