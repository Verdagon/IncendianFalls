using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class LightningChargingUCExtensions {
    public static Atharia.Model.Void Destruct(
        this LightningChargingUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void BeforeImpulse(
        LightningChargingUC self,
        Game game,
        Superstate superstate,
        Unit unit,
        IAICapabilityUC originatingCapability,
        IImpulse impulse) {

      if (unit.hp > Actions.LIGHTNING_CHARGE_DAMAGE + 1 && unit.hp >= unit.maxHp / 4) {
        var bunch = IImpulseStrongMutBunch.New(game.root);
        bunch.Add(impulse);

        var pursueImpulse = bunch.GetOnlyPursueImpulseOrNull();
        if (pursueImpulse.Exists() && pursueImpulse.isClearPath) {
          IncendianFalls.Actions.LightningCharge(game, superstate, unit);
        }

        var attackImpulse = bunch.GetOnlyAttackImpulseOrNull();
        if (attackImpulse.Exists()) {
          IncendianFalls.Actions.LightningCharge(game, superstate, unit);
        }
      }

      return new Atharia.Model.Void();
    }
  }
}
