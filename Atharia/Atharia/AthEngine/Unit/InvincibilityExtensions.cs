using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class InvincibilityUCExtensions {
    public static Atharia.Model.Void Destruct(
        this InvincibilityUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static int GetOutgoingDamageAddConstant(this Atharia.Model.InvincibilityUC detail) {
      return 10;
    }
    public static int GetOutgoingDamageMultiplierPercent(this Atharia.Model.InvincibilityUC detail) {
      return 500;
    }
    public static int GetIncomingDamageAddConstant(this Atharia.Model.InvincibilityUC detail) {
      return 0;
    }
    public static int GetIncomingDamageMultiplierPercent(this Atharia.Model.InvincibilityUC detail) {
      return 20;
    }
    public static Void PreAct(
        this Atharia.Model.InvincibilityUC detail,
        Game game,
        Superstate superstate,
        Unit unit) {
      unit.hp = unit.maxHp;
      return new Atharia.Model.Void();
    }
  }
}
