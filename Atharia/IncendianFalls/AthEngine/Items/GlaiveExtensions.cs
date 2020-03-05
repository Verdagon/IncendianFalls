using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class GlaiveExtensions {
    public static Atharia.Model.Void Destruct(
        this Glaive obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static int GetOutgoingDamageAddConstant(this Glaive armor) {
      return 0;
    }
    public static int GetOutgoingDamageMultiplierPercent(this Glaive armor) {
      return 200;
    }
    public static ICloneableUC ClonifyAndReturnNewReal(this Glaive glaive, Root newRoot) {
      return newRoot.EffectGlaiveCreate().AsICloneableUC();
    }
  }
}
