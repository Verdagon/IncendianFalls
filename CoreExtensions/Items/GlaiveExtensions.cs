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
    public static int AffectIncomingDamage(this Glaive glaive, int damage) {
      return damage;
    }
    public static int AffectOutgoingDamage(this Glaive glaive, int damage) {
      int newDamage = damage * 2;
      return newDamage;
    }
    public static IItem ClonifyAndReturnNewReal(this Glaive glaive, Root newRoot) {
      return newRoot.EffectGlaiveCreate().AsIItem();
    }
  }
}
