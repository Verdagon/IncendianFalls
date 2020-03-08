using System;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class TemporaryCloneImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this TemporaryCloneImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetWeight(this TemporaryCloneImpulse obj) {
      return obj.weight;
    }

    public static bool Enact(
        this TemporaryCloneImpulse obj,
        Game game,
        Superstate superstate,
        Unit actingUnit) {

      var capability = actingUnit.components.GetOnlyTemporaryCloneAICapabilityUCOrNull();
      Asserts.Assert(capability.Exists());
      Asserts.Assert(capability.charges > 0);
      capability.charges = capability.charges - 1;

      Asserts.Assert(obj.blueprintName == "Chronolisk"); // implement others
      var chronolisk = Chronolisk.Make(game.root);
      chronolisk.hp = obj.hp;
      foreach (var cloneable in chronolisk.components.GetAllTemporaryCloneAICapabilityUC()) {
        chronolisk.components.Remove(cloneable.AsIUnitComponent());
        cloneable.Destruct();
      }
      chronolisk.components.Add(game.root.EffectDoomedUCCreate(game.time += 600 * 10).AsIUnitComponent());
      game.level.EnterUnit(superstate.levelSuperstate, obj.location, game.level.time, chronolisk);

      actingUnit.nextActionTime = actingUnit.nextActionTime + actingUnit.CalculateMovementTimeCost(300);

      return false;
    }
  }
}
