using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class UnleashBideImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this UnleashBideImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static int GetWeight(this UnleashBideImpulse obj) {
      return obj.weight;
    }

    public static Atharia.Model.Void Enact(this UnleashBideImpulse obj, Unit actingUnit, Game game) {
      var liveUnitByLocationMap = new LiveUnitByLocationMap(game);

      foreach (var otherUnit in game.level.units) {
        if (otherUnit.Is(actingUnit)) {
          continue;
        }
        if (!otherUnit.alive) {
          continue;
        }
        var distance =
          game.level.terrain.pattern.GetTileCenter(otherUnit.location)
          .distance(game.level.terrain.pattern.GetTileCenter(actingUnit.location));
        if (distance <= 2) {
          Actions.Attack(game, liveUnitByLocationMap, actingUnit, otherUnit, false);
        }
      }

      actingUnit.nextActionTime = actingUnit.nextActionTime + actingUnit.inertia * 3 / 2;
      actingUnit.ClearOperation();

      return new Atharia.Model.Void();
    }
  }
}
