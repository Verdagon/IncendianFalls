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

    public static Atharia.Model.Void Enact(
        this PursueImpulse impulse,
        Game game,
        Superstate superstate,
        Unit unit) {

      var capability = unit.components.GetOnlyAttackAICapabilityUCOrNull();
      Asserts.Assert(capability.Exists());

      var directive = capability.killDirective;
      Asserts.Assert(directive.Exists());

      Actions.Hop(game, superstate, unit, directive.pathToLastSeenLocation[0], true);

      directive.pathToLastSeenLocation.RemoveAt(0);

      if (directive.pathToLastSeenLocation.Count == 0) {
        // We made it and we can't find the target.
        capability.killDirective.Destruct();
      }

      return new Atharia.Model.Void();
    }
  }
}
