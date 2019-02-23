using System;
using Atharia.Model;

namespace IncendianFalls {
  public static class PursueImpulseExtensions {
    public static int GetWeightImpl(this PursueImpulse obj) {
      return obj.weight;
    }

    public static Atharia.Model.Void EnactImpl(this PursueImpulse impulse, Unit unit, Game game) {
      var liveUnitByLocationMap = new LiveUnitByLocationMap(game);
      Actions.Step(game, liveUnitByLocationMap, unit, impulse.stepLocation);

      var directive = unit.components.GetOnlyKillDirectiveUCOrNull();
      Asserts.Assert(directive.Exists());

      directive.pathToLastSeenLocation.RemoveAt(0);

      if (directive.pathToLastSeenLocation.Count == 0) {
        // We made it and we can't find the player.
        unit.components.Remove(directive.AsIDirectiveUC());
        directive.pathToLastSeenLocation.Delete();
        directive.Delete();
      }

      return new Atharia.Model.Void();
    }
  }
}
