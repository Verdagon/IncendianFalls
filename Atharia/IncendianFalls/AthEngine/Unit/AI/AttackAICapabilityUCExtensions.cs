using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class AttackAICapabilityUCExtensions {
    public static Atharia.Model.Void Destruct(
        this AttackAICapabilityUC obj) {
      var killDirective = obj.killDirective;
      obj.Delete();
      if (killDirective.Exists()) {
        killDirective.Destruct();
      }
      return new Atharia.Model.Void();
    }

    private static Unit DetermineTarget(Game game, Superstate superstate, Unit unit) {
      var adjLocs =
          game.level.terrain.GetAdjacentExistingLocations(
              unit.location, game.level.ConsiderCornersAdjacent());
      var adjacentEnemies = new List<Unit>();
      foreach (var adjLoc in adjLocs) {
        if (game.level.terrain.GetElevationDifference(unit.location, adjLoc) <= 2) {
          var otherUnit = superstate.levelSuperstate.GetLiveUnitAt(adjLoc);
          if (otherUnit.Exists() && unit.good != otherUnit.good) {
            // Prioritize shielding and countering units
            if (otherUnit.components.GetOnlyDefyingUCOrNull().Exists()) {
              adjacentEnemies.Insert(0, otherUnit);
            } else if (otherUnit.components.GetOnlyCounteringUCOrNull().Exists()) {
              adjacentEnemies.Insert(0, otherUnit);
            } else {
              adjacentEnemies.Add(otherUnit);
            }
          }
        }
      }
      if (adjacentEnemies.Count > 0) {
        return adjacentEnemies[0];
      }

      return superstate.levelSuperstate.FindNearestLiveUnit(
          game,
          unit.location,
          // Filter so its not this unit
          unit,
          // Opposite allegiance to unit
          !unit.good);
    }

    public static bool PreAct(
        this AttackAICapabilityUC obj,
        Game game,
        Superstate superstate,
        Unit unit) {

      var directive = obj.killDirective;
      if (directive.Exists() && (!directive.targetUnit.Exists() || !directive.targetUnit.alive)) {
        // Target died, and/or was deleted. Stop targeting.
        obj.killDirective.Destruct();
      }

      // Remember, if we get here, we might still have an existing valid directive.
      // The below code is to just to update it if we have better information now.

      Unit nearestEnemy = DetermineTarget(game, superstate, unit);
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
                    terrain.tiles[to].IsWalkable() &&
                    terrain.GetElevationDifference(from, to) <= 2;
              });

      if (pathToNearestEnemy.Count == 0) {
        // Can't see the enemy. Don't update directive.
        return false;
      }

      if (obj.killDirective.Exists()) {
        obj.killDirective.Destruct();
      }

      // Can see the enemy. Add directive to pursue.
      obj.killDirective =
          game.root.EffectKillDirectiveCreate(
              nearestEnemy,
              game.root.EffectLocationMutListCreate(pathToNearestEnemy));
      return false;
    }

    public static IImpulse ProduceImpulse(
        this AttackAICapabilityUC obj,
        Game game,
        Superstate superstate,
        Unit unit) {

      // We only attack if we have a directive.
      var directive = obj.killDirective;
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

    public static Atharia.Model.Void BeforeImpulse(
        AttackAICapabilityUC self,
        Game game,
        Superstate superstate,
        Unit unit,
        IAICapabilityUC originatingCapability,
        IImpulse impulse) {
      if (!originatingCapability.NullableIs(self.AsIAICapabilityUC()) &&
          self.killDirective.Exists()) {
        self.killDirective.Destruct();
      }
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void AfterImpulse(
        AttackAICapabilityUC self,
        Game game,
        Superstate superstate,
        Unit unit,
        IAICapabilityUC originatingCapability,
        IImpulse impulse) {
      if (self.killDirective.Exists()) {
        if (!self.killDirective.targetUnit.Exists()) {
          self.killDirective.Destruct();
          self.killDirective = KillDirective.Null;
        }
      }
      return new Atharia.Model.Void();
    }
  }
}


