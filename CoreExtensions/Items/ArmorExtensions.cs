using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class ArmorExtensions {
    public static Atharia.Model.Void Destruct(this Armor obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static int AffectIncomingDamage(this Armor armor, int damage) {
      int newDamage = damage / 2;
      armor.root.logger.Info("Armor:" + damage + "->" + newDamage);
      return newDamage;
    }
    public static IItem ClonifyAndReturnNewReal(this Armor armor, Root newRoot) {
      return newRoot.EffectArmorCreate().AsIItem();
    }
  }
}
