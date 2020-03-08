using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class SpeedRingExtensions {
    public static Atharia.Model.Void Destruct(this SpeedRing obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static int GetMovementTimeAddConstant(this SpeedRing armor) {
      return 0;
    }
    public static int GetMovementTimeMultiplierPercent(this SpeedRing armor) {
      return 120;
    }
    public static ICloneableUC ClonifyAndReturnNewReal(this SpeedRing ring, Root newRoot) {
      return newRoot.EffectSpeedRingCreate().AsICloneableUC();
    }
  }
}
