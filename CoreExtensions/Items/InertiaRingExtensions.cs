using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class InertiaRingExtensions {
    public static Atharia.Model.Void Destruct(this InertiaRing obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static int AffectInertia(this InertiaRing armor, int inertia) {
      int newInertia = inertia * 2 / 3;
      return newInertia;
    }
    public static IItem ClonifyAndReturnNewReal(this InertiaRing ring, Root newRoot) {
      return newRoot.EffectInertiaRingCreate().AsIItem();
    }
  }
}
