﻿using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class AttackImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this AttackImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetWeight(this AttackImpulse obj) {
      return obj.weight;
    }

    public static bool Enact(
        this AttackImpulse obj,
        Game game,
        LiveUnitByLocationMap liveUnitByLocationMap, 
        Unit unit) {
      var directive = unit.components.GetOnlyKillDirectiveUCOrNull();
      Asserts.Assert(directive.Exists());

      Actions.Bump(game, liveUnitByLocationMap, unit, directive.targetUnit);

      if (!directive.targetUnit.alive) {
        // Glorious victory!
        game.root.logger.Info("Destroyed enemy!");
      //  unit.components.Remove(directive.AsIUnitComponent());
      //  directive.Destruct();
      }

      return true;
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
