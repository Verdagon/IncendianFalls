using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class ArmorExtensions {
    public static int AffectIncomingDamageImpl(this Armor armor, int damage) {
      int newDamage = damage / 2;
      Console.WriteLine("Armor:" + damage + "->" + newDamage);
      return newDamage;
    }
    public static int AffectOutgoingDamageImpl(this Armor armor, int damage) {
      return damage;
    }
  }
}
