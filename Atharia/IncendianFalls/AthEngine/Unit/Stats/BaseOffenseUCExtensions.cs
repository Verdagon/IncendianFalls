using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class BaseOffenseUCExtensions {
    public static Atharia.Model.Void Destruct(
        this BaseOffenseUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static int GetOutgoingDamageAddConstant(this BaseOffenseUC obj) {
      return obj.outgoingDamageAddConstant;
    }
    public static int GetOutgoingDamageMultiplierPercent(this BaseOffenseUC obj) {
      return obj.outgoingDamageMultiplierPercent;
    }
    public static ICloneableUC ClonifyAndReturnNewReal(this BaseOffenseUC obj, Root newRoot) {
      return newRoot.EffectBaseOffenseUCCreate(
        obj.outgoingDamageAddConstant,
        obj.outgoingDamageMultiplierPercent)
        .AsICloneableUC();
    }
  }
}
