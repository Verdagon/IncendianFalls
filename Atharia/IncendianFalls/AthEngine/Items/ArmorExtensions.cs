using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class ArmorExtensions {
    public static Atharia.Model.Void Destruct(this Armor obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static int GetIncomingDamageAddConstant(this Armor armor) {
      return 0;
    }
    public static int GetIncomingDamageMultiplierPercent(this Armor armor) {
      return 50;
    }
    public static ICloneableUC ClonifyAndReturnNewReal(this Armor armor, Root newRoot) {
      return newRoot.EffectArmorCreate().AsICloneableUC();
    }
  }
}
