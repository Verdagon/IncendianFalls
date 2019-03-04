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

    public static bool Enact(
        this UnleashBideImpulse obj,
        Game game,
        Superstate superstate,
        Unit actingUnit) {
      List<Unit> victims = new List<Unit>();
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
          victims.Add(otherUnit);
        }
      }
      Actions.UnleashBide(game, superstate, actingUnit, victims);

      actingUnit.ClearOperation();

      return true;
    }
  }
}
