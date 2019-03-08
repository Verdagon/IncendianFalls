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
    public static int AffectOutgoingDamage(this Armor armor, int damage) {
      return damage;
    }
  }
}
