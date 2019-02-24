using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atharia.Model;

namespace IncendianFalls {
  public class EnemyAI {
    public static void AI(
        Game game,
        LiveUnitByLocationMap liveUnitByLocationMap,
        Unit unit) {

      var unitPosition = game.level.terrain.pattern.GetTileCenter(unit.location);

      IImpulse strongestImpulse = game.root.EffectNoImpulseCreate().AsIImpulse();
      foreach (var capability in unit.components.GetAllIAICapabilityUC()) {
        var hayImpulse = capability.ProduceImpulse(unit, game);
        if (hayImpulse.GetWeight() > strongestImpulse.GetWeight()) {
          strongestImpulse.Destruct();
          strongestImpulse = hayImpulse;
        } else {
          hayImpulse.Destruct();
        }
      }
      strongestImpulse.Enact(unit, game);
      strongestImpulse.Destruct();
    }
  }
}
