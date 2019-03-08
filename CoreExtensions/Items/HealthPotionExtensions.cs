using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class HealthPotionExtensions {
    public static Atharia.Model.Void Destruct(this HealthPotion obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static Atharia.Model.Void Use(
        this HealthPotion potion,
        Game game,
        Superstate superstate,
        Unit unit) {
      Asserts.Assert(unit.components.membersHealthPotionMutSet.Contains(potion));

      unit.hp = unit.maxHp;

      unit.components.Remove(potion.AsIUnitComponent());
      potion.Destruct();
      return new Atharia.Model.Void();
    }
    public static IItem ClonifyAndReturnNewReal(this HealthPotion potion, Root newRoot) {
      return NullIItem.Null;
    }
  }
}
