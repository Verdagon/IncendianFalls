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

      unit.mp = unit.maxMp;

      unit.components.Remove(potion.AsIUnitComponent());
      potion.Destruct();
      return new Atharia.Model.Void();
    }
    public static IItem ClonifyAndReturnNewReal(this ManaPotion potion, Root newRoot) {
      return NullIItem.Null;
    }
  }
}
