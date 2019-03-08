using System;
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

      Unit nearestEnemy =
          superstate.levelSuperstate.FindNearestLiveUnit(
              game,
              unit.location,
              // Filter so its not this unit
              unit,
              // Opposite allegiance to unit
              !unit.good);
      if (!nearestEnemy.Exists()) {
        // There are no enemies. Don't update directive.
        return false;
      }

      // Enemy is not next to subject.
      // Check if we can see them.
      if (!Sight.CanSee(game, unit, nearestEnemy.location, out List<Location> sightPath)) {
        // Can't see the enemy. Don't update directive.
        return false;
      }

      var terrain = game.level.terrain;
      // Check if we can reach them.
      List<Location> pathToNearestEnemy =
          AStarExplorer.Go(
              terrain.pattern,
              unit.location,
              nearestEnemy.location,
              game.level.ConsiderCornersAdjacent(),
              (Location from, Location to) => {
                return terrain.tiles.ContainsKey(to) &&
                    terrain.tiles[to].walkable &&
                    terrain.GetElevationDifference(from, to) <= 2;
              });

      if (pathToNearestEnemy.Count == 0) {
        // Can't see the enemy. Don't update directive.
        return false;
      }

      // Can see the enemy. Add directive to pursue.
      directive =
          game.root.EffectKillDirectiveUCCreate(
              nearestEnemy,
              game.root.EffectLocationMutListCreate(pathToNearestEnemy));
      Asserts.Assert(directive.Exists());
      unit.ReplaceDirective(directive.AsIDirectiveUC());
      Asserts.Assert(unit.GetDirectiveOrNull().Exists());
      return false;
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
        if (game.level.terrain.pattern.LocationsAreAdjacent(unit.location, directive.targetUnit.location, game.level.ConsiderCornersAdjacent()) &&
            game.level.terrain.GetElevationDifference(unit.location, directive.targetUnit.location) <= 2) {
          // Target is right next to subject. Attack!
          return obj.root.EffectAttackImpulseCreate(800, directive.targetUnit).AsIImpulse();
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
