using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class AttackImpulseExtensions {
    public static int GetWeightImpl(this AttackImpulse obj) {
      return obj.weight;
    }

    public static Atharia.Model.Void EnactImpl(this AttackImpulse obj, Unit unit, Game game) {
      var directive = unit.components.GetOnlyKillDirectiveUCOrNull();
      Asserts.Assert(directive.Exists());

      var liveUnitByLocationMap = new LiveUnitByLocationMap(game);
      Actions.Attack(game, liveUnitByLocationMap, unit, directive.targetUnit, true);

      return new Atharia.Model.Void();
    }

    //private static void AttackTo(
    //    Game game,
    //    LiveUnitByLocationMap liveUnitByLocationMap,
    //    Unit unit,
    //    KillDirectiveUC attack) {
    //  // If the target unit died or was deleted, the directive's PreAct() should have
    //  // deleted itself.
    //  Asserts.Assert(attack.targetUnit.Exists());
    //  Asserts.Assert(attack.targetUnit.alive);

    //  Asserts.Assert(attack.pathToLastSeenLocation.Count > 0, "Empty path to last seen?");

    //  var lastSeenLocation =
    //      attack.pathToLastSeenLocation[attack.pathToLastSeenLocation.Count - 1];

    //  if (game.level.terrain.pattern.LocationsAreAdjacent(unit.location, attack.targetUnit.location, game.level.considerCornersAdjacent)) {
    //    Actions.Attack(game, liveUnitByLocationMap, unit, attack.targetUnit);
    //    return;
    //  }

    //  if (!Actions.CanStep(game, liveUnitByLocationMap, unit, attack.pathToLastSeenLocation[0])) {
    //    // Am confused. Can't step that way. This might be because another unit
    //    // walked in front of us or something. Keep the same directive, but stall
    //    // by half a turn.
    //    unit.nextActionTime = unit.nextActionTime + unit.inertia / 2;
    //    return;
    //  }

    //  Actions.Step(game, liveUnitByLocationMap, unit, attack.pathToLastSeenLocation[0]);
    //  attack.pathToLastSeenLocation.RemoveAt(0);

    //  if (attack.pathToLastSeenLocation.Count == 0) {
    //    // We made it and we can't find the player.
    //    attack.pathToLastSeenLocation.Delete();
    //    attack.Delete();
    //  }
    //}

  }
}
