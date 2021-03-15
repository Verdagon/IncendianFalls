using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class KamikazeAICapabilityUCExtensions {
    public static Atharia.Model.Void Destruct(
        this KamikazeAICapabilityUC self) {
      Asserts.Assert(self.targetByLocation.Count == 0); // should have cleared in the DeathPreactor

      self.Delete();
      return new Atharia.Model.Void();
    }

    public static IImpulse ProduceImpulse(
        this KamikazeAICapabilityUC self,
        Game game,
        Superstate superstate,
        Unit unit) {

      if (self.targetByLocation.Count == 0) {
        // No targets means we aren't currently kamikazing. Let's see if theres a good candidate near.
        Unit nearestEnemy = DetermineTarget.Determine(game, superstate, unit);
        if (!nearestEnemy.Exists()) {
          // There are no enemies.
          return game.root.EffectNoImpulseCreate().AsIImpulse();
        }

        // Enemy is not next to subject.
        // Check if we can see them.
        if (!Sight.CanSee(game, unit, nearestEnemy.location, out List<Location> sightPath)) {
          // Can't see the enemy. Don't update directive.
          return game.root.EffectNoImpulseCreate().AsIImpulse();
        }

        var targetLocationCenter = nearestEnemy.location;
        var targetLocations = new List<Location>(game.level.terrain.GetAdjacentExistingLocations(targetLocationCenter, false));
        targetLocations.Insert(0, targetLocationCenter);
        return game.root.EffectKamikazeTargetImpulseCreate(700, self, targetLocationCenter, new LocationImmList(targetLocations)).AsIImpulse();
      } else {
        // We have targets, lets go there.

        Location locationToJumpTo = null;
        if (Actions.CanTeleportTo(game.level.terrain, superstate, self.targetLocationCenter)) {
          locationToJumpTo = self.targetLocationCenter;
        } else {
          foreach (var location in self.targetByLocation.Keys) {
            if (Actions.CanTeleportTo(game.level.terrain, superstate, location)) {
              locationToJumpTo = location;
            }
          }
        }

        if (locationToJumpTo == null) {
          // We can't fit there. Cancel!
          return game.root.EffectNoImpulseCreate().AsIImpulse();
        }

        return game.root.EffectKamikazeJumpImpulseCreate(850, self, locationToJumpTo).AsIImpulse();
      }
    }

    public static Atharia.Model.Void BeforeImpulse(
        KamikazeAICapabilityUC self,
        Game game,
        Superstate superstate,
        Unit unit,
        IAICapabilityUC originatingCapability,
        IImpulse impulse) {
      if (!originatingCapability.NullableIs(self.AsIAICapabilityUC()) &&
          self.targetByLocation.Count > 0) {
        // They're doing something other than unleashing the kamikaze, cancel this operation.
        foreach (var location in self.targetByLocation.Keys) {
          var target = self.targetByLocation[location];
          self.targetByLocation.Remove(location);
          game.level.terrain.tiles[location].components.Remove(
            target.AsITerrainTileComponent());
          target.Destruct();
        }
        Asserts.Assert(self.targetByLocation.Count == 0);
      }
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void BeforeDeath(
        KamikazeAICapabilityUC self,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        Unit unit) {
      Asserts.Assert(!unit.Alive(), "curiosity");
      var targetLocations = new SortedSet<Location>(self.targetByLocation.Keys);
      foreach (var location in targetLocations) {
        var target = self.targetByLocation[location];
        self.targetByLocation.Remove(location);
        game.level.terrain.tiles[location].components.Remove(
          target.AsITerrainTileComponent());
        target.Destruct();
      }
      return new Atharia.Model.Void();
    }
  }
}
