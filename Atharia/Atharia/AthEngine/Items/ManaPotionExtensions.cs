using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class ManaPotionExtensions {
    public static Atharia.Model.Void Destruct(this ManaPotion obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static Atharia.Model.Void Use(
        this ManaPotion potion,
        Game game,
        Superstate superstate,
        Unit unit) {
      Asserts.Assert(unit.components.membersManaPotionMutSet.Contains(potion));

      var sorcerous = unit.components.GetOnlySorcerousUCOrNull();
      Asserts.Assert(sorcerous != null, "Cant use potion, dont have sorcerous!");

      sorcerous.mp = sorcerous.maxMp;
      unit.components.Remove(potion.AsIUnitComponent());
      potion.Destruct();

      return new Atharia.Model.Void();
    }
  }
}
