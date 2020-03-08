using System;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class KamikazeJumpImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this KamikazeJumpImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetWeight(this KamikazeJumpImpulse obj) {
      return obj.weight;
    }

    public static bool Enact(
        this KamikazeJumpImpulse self,
        Game game,
        Superstate superstate,
        Unit unit) {
      foreach (var location in self.capability.targetByLocation.Keys) {
        var target = self.capability.targetByLocation[location];
        self.capability.targetByLocation.Remove(location);
        game.level.terrain.tiles[location].components.Remove(
          target.AsITerrainTileComponent());
        target.Destruct();

        Actions.ExplodeFireBomb(game, superstate, location);
      }

      Actions.Step(game, superstate, unit, self.jumpTarget, true, false);
      // We told step to take no time. Let's take double here.
      unit.nextActionTime = unit.nextActionTime + unit.CalculateMovementTimeCost(1200);

      Asserts.Assert(self.capability.targetByLocation.Count == 0);

      return false;
    }
  }
}
