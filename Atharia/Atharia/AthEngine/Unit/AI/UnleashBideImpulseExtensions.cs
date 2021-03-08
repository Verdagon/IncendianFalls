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

    public static Atharia.Model.Void Enact(
        this UnleashBideImpulse obj,
        Game game,
        Superstate superstate,
        Unit actingUnit) {
      var capability = actingUnit.components.GetOnlyBideAICapabilityUCOrNull();
      Asserts.Assert(capability.Exists());
      capability.charge = 0;

      // Get this, all surrounding, and all surrounding those.
      var affectedTileLocations = new SortedSet<Location>() { actingUnit.location };
      affectedTileLocations = game.level.terrain.pattern.GetAdjacentLocations(affectedTileLocations, true, game.level.ConsiderCornersAdjacent());
      affectedTileLocations = game.level.terrain.pattern.GetAdjacentLocations(affectedTileLocations, true, game.level.ConsiderCornersAdjacent());
      
      Actions.UnleashBide(game, superstate, actingUnit, affectedTileLocations);

      return new Atharia.Model.Void();
    }
  }
}
