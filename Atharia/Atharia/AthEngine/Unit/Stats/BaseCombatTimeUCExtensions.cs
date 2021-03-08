using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class BaseCombatTimeUCExtensions {
    public static Atharia.Model.Void Destruct(
        this BaseCombatTimeUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetCombatTimeAddConstant(this BaseCombatTimeUC obj) {
      return obj.combatTimeAddConstant;
    }
    public static int GetCombatTimeMultiplierPercent(this BaseCombatTimeUC obj) {
      return obj.combatTimeMultiplierPercent;
    }
    public static ICloneableUC ClonifyAndReturnNewReal(this BaseCombatTimeUC obj, Root newRoot) {
      return newRoot.EffectBaseCombatTimeUCCreate(
        obj.combatTimeAddConstant,
        obj.combatTimeMultiplierPercent)
        .AsICloneableUC();
    }
  }
}
