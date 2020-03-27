using System;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class SummonImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this SummonImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetWeight(this SummonImpulse obj) {
      return obj.weight;
    }

    public static Atharia.Model.Void Enact(
        this SummonImpulse obj,
        Game game,
        Superstate superstate,
        Unit actingUnit) {

      var capability = actingUnit.components.GetOnlySummonAICapabilityUCOrNull();
      Asserts.Assert(capability.Exists());
      Asserts.Assert(capability.charges > 0);
      capability.charges = capability.charges - 1;

      Asserts.Assert(obj.blueprintName == "Irkling"); // implement others
      game.level.EnterUnit(superstate.levelSuperstate, obj.location, game.level.time, Irkling.Make(game.root));

      actingUnit.nextActionTime = actingUnit.nextActionTime + actingUnit.CalculateMovementTimeCost(300);

      return new Atharia.Model.Void();
    }
  }
}
