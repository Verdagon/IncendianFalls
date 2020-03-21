using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class CounteringUCExtensions {
    public static Atharia.Model.Void Destruct(
        this CounteringUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static bool React(
        this Atharia.Model.CounteringUC detail,
        Game game,
        Superstate superstate,
        Unit unit,
        Unit attacker) {
      if (!attacker.Exists()) {
        return false;
      }
      unit.components.Remove(detail.AsIUnitComponent());
      detail.Destruct();
      Actions.Bump(game, superstate, unit, attacker, 200, false);

      var sorcerous = attacker.components.GetOnlySorcerousUCOrNull();
      Asserts.Assert(sorcerous != null, "Must be sorcerous to counter");
      sorcerous.mp = sorcerous.mp - 2;
      return true;
    }
    public static bool PreAct(
        this Atharia.Model.CounteringUC detail,
        Game game,
        Superstate superstate,
        Unit unit) {
      unit.components.Remove(detail.AsIUnitComponent());
      detail.Destruct();
      return true;
    }
  }
}
