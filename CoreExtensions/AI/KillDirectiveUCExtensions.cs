using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class KillDirectiveUCExtensions {
    public static Atharia.Model.Void Destruct(
        this KillDirectiveUC obj) {
      var thing = obj.pathToLastSeenLocation;
      obj.Delete();
      thing.Destruct();
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void AfterImpulse(
        this KillDirectiveUC obj,
        Game game,
        Superstate superstate,
        Unit unit,
        IImpulse impulse) {
      if (!obj.targetUnit.Exists()) {
        unit.ClearDirective();
      }
      return new Atharia.Model.Void();
    }
  }
}
