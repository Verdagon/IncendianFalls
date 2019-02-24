using System;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class PursueImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this PursueImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static int GetWeight(this PursueImpulse obj) {
      return obj.weight;
    }

    public static Atharia.Model.Void Enact(this PursueImpulse impulse, Unit unit, Game game) {
      var liveUnitByLocationMap = new LiveUnitByLocationMap(game);
      Actions.Step(game, liveUnitByLocationMap, unit, impulse.stepLocation);

      var directive = unit.components.GetOnlyKillDirectiveUCOrNull();
      Asserts.Assert(directive.Exists());

      directive.pathToLastSeenLocation.RemoveAt(0);

      if (directive.pathToLastSeenLocation.Count == 0) {
        // We made it and we can't find the player.
        unit.components.Remove(directive.AsIUnitComponent());
        directive.Destruct();
      }

      return new Atharia.Model.Void();
    }
  }
}
