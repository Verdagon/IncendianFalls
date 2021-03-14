using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class OnFireUCExtensions {
    public static Atharia.Model.Void Destruct(
        this OnFireUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static Void PreAct(
        this Atharia.Model.OnFireUC detail,
        Game game,
        Superstate superstate,
        Unit unit) {
      Asserts.Assert(false);
      unit.components.Remove(detail.AsIUnitComponent());
      detail.Destruct();
      return new Atharia.Model.Void();
    }
  }
}
