using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atharia.Model;

namespace IncendianFalls {
  public class EnemyAI {
    // Returns true if we should return
    public static bool AI(
        Game game,
        Superstate superstate,
        Unit unit) {

      var unitPosition = game.level.terrain.pattern.GetTileCenter(unit.location);

      IImpulse strongestImpulse = game.root.EffectNoImpulseCreate().AsIImpulse();
      foreach (var capability in unit.components.GetAllIAICapabilityUC()) {
        var hayImpulse = capability.ProduceImpulse(game, superstate, unit);
        if (hayImpulse.GetWeight() > strongestImpulse.GetWeight()) {
          strongestImpulse.Destruct();
          strongestImpulse = hayImpulse;
        } else {
          hayImpulse.Destruct();
        }
      }
      // game.root.logger.Info("Enacting impulse: " + strongestImpulse.ToString());

      if (unit.GetOperationOrNull().Exists()) {
        unit.GetOperationOrNull().BeforeImpulse(game, superstate, unit, strongestImpulse);
      }

      bool ret = strongestImpulse.Enact(game, superstate, unit);

      if (unit.Exists()) {
        if (unit.GetDirectiveOrNull().Exists()) {
          unit.GetDirectiveOrNull().AfterImpulse(game, superstate, unit, strongestImpulse);
        }
      }

      strongestImpulse.Destruct();

      return ret;
    }
  }
}
