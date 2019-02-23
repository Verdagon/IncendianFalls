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
        game.root.logger.Info("Weighing impulse " + hayImpulse + " " + hayImpulse.GetWeight() + " against incumbent " + strongestImpulse + " " + strongestImpulse.GetWeight());
        if (hayImpulse.GetWeight() > strongestImpulse.GetWeight()) {
          strongestImpulse = hayImpulse;
        }
      }
      game.root.logger.Info("Enacting impulse: " + strongestImpulse + " " + unit.nextActionTime);
      strongestImpulse.Enact(unit, game);
      game.root.logger.Info("Enacted impulse: " + unit.nextActionTime);
    }
  }
}
