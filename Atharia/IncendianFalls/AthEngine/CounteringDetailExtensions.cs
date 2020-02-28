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
      Actions.Bump(game, superstate, unit, attacker, 2.0f, false);
      unit.mp = unit.mp - 2;
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
