using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class GuardAICapabilityUCExtensions {
    public static Atharia.Model.Void Destruct(
        this GuardAICapabilityUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static IImpulse ProduceImpulse(
        this GuardAICapabilityUC obj,
        Game game,
        Superstate superstate,
        Unit unit) {

      if (unit.location.Equals(obj.guardCenterLocation)) {
        // Already there.
        return obj.root.EffectNoImpulseCreate().AsIImpulse();
      }

      List<Location> pathToGuardArea =
        AStarExplorer.Go(
          game.level.terrain.pattern,
          unit.location,
          obj.guardCenterLocation,
          game.level.terrain.considerCornersAdjacent,
          (Location from, Location to, float totalCost) => {
            return game.level.terrain.tiles.ContainsKey(to) &&
                game.level.terrain.tiles[to].IsWalkable() &&
                game.level.terrain.GetElevationDifference(from, to) <= 2;
          });
      if (pathToGuardArea.Count == 0) {
        // No path.
        return obj.root.EffectNoImpulseCreate().AsIImpulse();
      } else if (pathToGuardArea.Count < obj.guardRadius) {
        // Within area.
        return obj.root.EffectNoImpulseCreate().AsIImpulse();
      } else {
        // We want to go towards the area.
        var nextStep = pathToGuardArea[0];
        if (!superstate.levelSuperstate.CanHop(unit.location, nextStep, true)) {
          // Something else is in the way.
          return obj.root.EffectNoImpulseCreate().AsIImpulse();
        } else {
          // Go towards the area.
          return obj.root.EffectMoveImpulseCreate(300, pathToGuardArea[0]).AsIImpulse();
        }
      }
    }
  }
}
