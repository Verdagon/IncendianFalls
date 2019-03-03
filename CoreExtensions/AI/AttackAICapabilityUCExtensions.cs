﻿using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class AttackAICapabilityUCExtensions {
    public static Atharia.Model.Void Destruct(
        this AttackAICapabilityUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static bool PreAct(
        this AttackAICapabilityUC obj,
        Game game,

        Superstate superstate,
        Unit unit) {
      var directive = unit.components.GetOnlyKillDirectiveUCOrNull();
      if (directive.Exists() && (!directive.targetUnit.Exists() || !directive.targetUnit.alive)) {
        // Target died, and/or was deleted. Stop targeting.
        unit.ClearDirective();
      }

      // Remember, if we get here, we might still have an existing valid directive.
      // The below code is to just to update it if we have better information now.

      // Player is not next to subject.
      // Check if we can see them.
      List<Location> pathToPlayer;
      if (Sight.CanSee(game, unit, game.player.location, out pathToPlayer)) {
        // Can see the enemy. Add directive to pursue.
        directive =
            game.root.EffectKillDirectiveUCCreate(
                game.player,
                game.root.EffectLocationMutListCreate(pathToPlayer));
        Asserts.Assert(directive.Exists());
        unit.ReplaceDirective(directive.AsIDirectiveUC());
        Asserts.Assert(unit.GetDirectiveOrNull().Exists());
        return false;
      } else {
        // Can't see the player. Don't update directive.
        return false;
      }
    }

    public static IImpulse ProduceImpulse(
        this AttackAICapabilityUC obj,
        Game game,

        Superstate superstate,
        Unit unit) {

      // We only attack if we have a directive.
      var directive = unit.components.GetOnlyKillDirectiveUCOrNull();
      if (!directive.Exists()) {
        // No directive, do nothing.
        return obj.root.EffectNoImpulseCreate().AsIImpulse();
      } else {
        if (game.level.terrain.pattern.LocationsAreAdjacent(unit.location, directive.targetUnit.location, game.level.considerCornersAdjacent)) {
          // Target is right next to subject. Attack!
          return obj.root.EffectAttackImpulseCreate(800).AsIImpulse();
        } else {
          // Not right next to us.

          if (!Actions.CanStep(game, superstate, unit, directive.pathToLastSeenLocation[0])) {
            // Am confused. Can't step that way. This might be because another unit
            // walked in front of us or something. Keep the same directive, but stall
            // by half a turn.
            return obj.root.EffectNoImpulseCreate().AsIImpulse();
          } else {
            // Can make the next step! Go for it!
            return obj.root.EffectPursueImpulseCreate(600).AsIImpulse();
          }
        }
      }
    }
  }
}
