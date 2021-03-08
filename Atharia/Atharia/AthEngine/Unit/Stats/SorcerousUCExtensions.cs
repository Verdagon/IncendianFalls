using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class SorcerousUCExtensions {
    public static Atharia.Model.Void Destruct(
        this SorcerousUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static ICloneableUC ClonifyAndReturnNewReal(this SorcerousUC sorcerous, Root newRoot) {
      return newRoot.EffectSorcerousUCCreate(sorcerous.mp, sorcerous.maxMp).AsICloneableUC();
    }
  }
}
