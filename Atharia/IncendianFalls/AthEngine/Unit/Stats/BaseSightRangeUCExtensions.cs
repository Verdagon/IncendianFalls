using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class BaseSightRangeUCExtensions {
    public static Atharia.Model.Void Destruct(
        this BaseSightRangeUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetSightRangeAddConstant(this BaseSightRangeUC obj) {
      return obj.sightRangeAddConstant;
    }
    public static int GetSightRangeMultiplierPercent(this BaseSightRangeUC obj) {
      return obj.sightRangeMultiplierPercent;
    }
    public static ICloneableUC ClonifyAndReturnNewReal(this BaseSightRangeUC obj, Root newRoot) {
      return newRoot.EffectBaseSightRangeUCCreate(
        obj.sightRangeAddConstant,
        obj.sightRangeMultiplierPercent)
        .AsICloneableUC();
    }
  }
}
