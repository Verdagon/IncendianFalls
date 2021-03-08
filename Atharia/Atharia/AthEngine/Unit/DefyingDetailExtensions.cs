using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class DefyingUCExtensions {
    public static Atharia.Model.Void Destruct(
        this DefyingUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static int GetIncomingDamageAddConstant(this Atharia.Model.DefyingUC detail) {
      return 0;
    }
    public static int GetIncomingDamageMultiplierPercent(this Atharia.Model.DefyingUC detail) {
      return 33;
    }
    public static Void PreAct(
        this Atharia.Model.DefyingUC detail,
        Game game,
        Superstate superstate,
        Unit unit) {
      //detail.root.logger.Error("PreAct removing " + detail.id + " there are " + game.root.rootIncarnation.incarnationsDefyingUC.Count + "+" + game.root.rootIncarnation.countDefyingMemberships());
      unit.components.Remove(detail.AsIUnitComponent());
      detail.Destruct();
      //detail.root.logger.Error("PreAct removed, there are " + game.root.rootIncarnation.incarnationsDefyingUC.Count + "+" + game.root.rootIncarnation.countDefyingMemberships());
      return new Atharia.Model.Void();
    }
  }
}
