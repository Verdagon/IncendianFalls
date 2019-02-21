using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class ShieldingUnitComponentExtensions {
    public static int AffectIncomingDamageImpl(this Atharia.Model.ShieldingUnitComponent detail, int damage) {
      int newDamage = damage / 3;
      Console.WriteLine("Defend:" + damage + "->" + newDamage);
      return newDamage;
    }
    public static Atharia.Model.Void PreActImpl(this Atharia.Model.ShieldingUnitComponent detail, Unit unit) {
      unit.components.Remove(detail.AsIUnitComponent());
      detail.Delete();
      return new Atharia.Model.Void();
    }
    public static Atharia.Model.Void PostActImpl(this Atharia.Model.ShieldingUnitComponent detail, Unit unit) {
      return new Atharia.Model.Void();
    }
  }
}
