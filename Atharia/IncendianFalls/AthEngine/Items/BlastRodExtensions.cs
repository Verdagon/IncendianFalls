using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class BlastRodExtensions {
    public static Atharia.Model.Void Destruct(this BlastRod obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static ICloneableUC ClonifyAndReturnNewReal(this BlastRod armor, Root newRoot) {
      return newRoot.EffectBlastRodCreate().AsICloneableUC();
    }
  }
}
