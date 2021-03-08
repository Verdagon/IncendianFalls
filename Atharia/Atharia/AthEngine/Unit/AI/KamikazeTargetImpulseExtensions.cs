using System;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class KamikazeTargetImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this KamikazeTargetImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetWeight(this KamikazeTargetImpulse obj) {
      return obj.weight;
    }

    public static Atharia.Model.Void Enact(
        this KamikazeTargetImpulse self,
        Game game,
        Superstate superstate,
        Unit unit) {

      self.capability.targetLocationCenter = self.targetLocationCenter;
      foreach (var location in self.targetLocations) {
        var target = game.root.EffectKamikazeTargetTTCCreate(self.capability);
        game.level.terrain.tiles[location].components.Add(target.AsITerrainTileComponent());
        self.capability.targetByLocation.Add(location, target);
      }
      Asserts.Assert(self.capability.targetByLocation.Count == self.targetLocations.Count);

      unit.nextActionTime = unit.nextActionTime + unit.CalculateCombatTimeCost(600);

      return new Atharia.Model.Void();
    }
  }
}
