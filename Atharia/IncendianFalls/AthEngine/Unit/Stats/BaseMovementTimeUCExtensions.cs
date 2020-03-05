using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class BaseMovementTimeUCExtensions {
    public static Atharia.Model.Void Destruct(
        this BaseMovementTimeUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetMovementTimeAddConstant(this BaseMovementTimeUC obj) {
      return obj.movementTimeAddConstant;
    }
    public static int GetMovementTimeMultiplierPercent(this BaseMovementTimeUC obj) {
      return obj.movementTimeMultiplierPercent;
    }
    public static ICloneableUC ClonifyAndReturnNewReal(this BaseMovementTimeUC obj, Root newRoot) {
      return newRoot.EffectBaseMovementTimeUCCreate(
        obj.movementTimeAddConstant,
        obj.movementTimeMultiplierPercent)
        .AsICloneableUC();
    }
  }
}
