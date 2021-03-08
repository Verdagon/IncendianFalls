using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class BaseDefenseUCExtensions {
    public static Atharia.Model.Void Destruct(
        this BaseDefenseUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static int GetIncomingDamageAddConstant(this BaseDefenseUC obj) {
      return obj.incomingDamageAddConstant;
    }
    public static int GetIncomingDamageMultiplierPercent(this BaseDefenseUC obj) {
      return obj.incomingDamageMultiplierPercent;
    }
    public static ICloneableUC ClonifyAndReturnNewReal(this BaseDefenseUC obj, Root newRoot) {
      return newRoot.EffectBaseDefenseUCCreate(
        obj.incomingDamageAddConstant,
        obj.incomingDamageMultiplierPercent)
        .AsICloneableUC();
    }
  }
}
