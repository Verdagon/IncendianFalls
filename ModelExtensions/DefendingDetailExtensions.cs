using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class DefendingDetailExtensions {
    public static int AffectIncomingDamageImpl(this Atharia.Model.DefendingDetail detail, int damage) {
      int newDamage = damage / 3;
      Console.WriteLine("Defend:" + damage + "->" + newDamage);
      return newDamage;
    }
    public static Atharia.Model.Void AfterTurnImpl(this Atharia.Model.DefendingDetail detail, Unit unit) {
      for (int i = 0; i < unit.details.Count; i++) {
        if (unit.details[i].Is(detail.AsIDetail())) {
          unit.details.RemoveAt(i);
          detail.Delete();
          return new Atharia.Model.Void();
        }
      }
      throw new Exception("This detail wasn't in that unit!");
    }
    public static Atharia.Model.Void PreActImpl(this Atharia.Model.DefendingDetail detail, Unit unit) {
      for (int i = 0; i < unit.details.Count; i++) {
        if (unit.details[i].Is(new DefendingDetailAsIDetail(detail))) {
          unit.details.RemoveAt(i);
          detail.Delete();
          return new Atharia.Model.Void();
        }
      }
      // not found in unit!
      Asserts.Assert(false);
      return new Atharia.Model.Void();
    }
    public static Atharia.Model.Void PostActImpl(this Atharia.Model.DefendingDetail detail, Unit unit) {
      return new Atharia.Model.Void();
    }
  }
}
