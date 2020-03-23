using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atharia.Model;

namespace IncendianFalls {
  public class EnemyAI {
    // Returns true if we should return
    public static bool AI(
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        Unit unit) {

      var unitPosition = game.level.terrain.pattern.GetTileCenter(unit.location);

      IImpulse strongestImpulse = game.root.EffectNoImpulseCreate().AsIImpulse();
      IAICapabilityUC strongestImpulseOriginatingCapability = NullIAICapabilityUC.Null;
      foreach (var capability in unit.components.GetAllIAICapabilityUC()) {
        var hayImpulse = capability.ProduceImpulse(game, superstate, unit);
        if (hayImpulse.GetWeight() > strongestImpulse.GetWeight()) {
          strongestImpulse.Destruct();
          strongestImpulse = hayImpulse;
          strongestImpulseOriginatingCapability = capability;
        } else {
          hayImpulse.Destruct();
        }
      }
      // game.root.logger.Info("Enacting impulse: " + strongestImpulse.ToString());

      foreach (var preReactor in unit.components.GetAllIImpulsePreReactor()) {
        preReactor.BeforeImpulse(
            game,
            superstate,
            unit,
            strongestImpulseOriginatingCapability,
            strongestImpulse);
      }

      bool ret = strongestImpulse.Enact(game, superstate, unit);

      // The unit theoretically could have self-destructed somehow.
      // Do we want to change this to a live check?
      if (unit.alive) {
        foreach (var postReactor in unit.components.GetAllIImpulsePostReactor()) {
          postReactor.AfterImpulse(
            context,
              game,
              superstate,
              unit,
              strongestImpulseOriginatingCapability,
              strongestImpulse);
        }
      }

      strongestImpulse.Destruct();

      return ret;
    }
  }
}
