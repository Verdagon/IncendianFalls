using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class EvolvifyAICapabilityUCExtensions {
    public static Atharia.Model.Void Destruct(
        this EvolvifyAICapabilityUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static IImpulse ProduceImpulse(
        this EvolvifyAICapabilityUC obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      // Let's see if theres an enemy near.
      Unit nearestEnemy = DetermineTarget.Determine(game, superstate, unit);
      if (!nearestEnemy.Exists()) {
        // There are no enemies.
        return game.root.EffectNoImpulseCreate().AsIImpulse();
      }

      // Check if we can see them.
      if (!superstate.levelSuperstate.CanSee(unit.location, nearestEnemy.location)) {
        // Can't see the enemy. Don't update directive.
        return game.root.EffectNoImpulseCreate().AsIImpulse();
      }
      
      // Find a location that we ourselves can move to.
      var steppableLocs = game.level.terrain.GetAdjacentExistingLocations(unit.location, false);
      SetUtils.RetainAll(steppableLocs, superstate.levelSuperstate.GetHoppableLocs(unit.location, true));
      if (steppableLocs.Count == 0) {
        return game.root.EffectNoImpulseCreate().AsIImpulse();
      }
      
      // Check if we're on top of a plant right now.
      if (game.level.terrain.tiles[unit.location].components.GetOnlyIPlantTTCOrNull().Exists()) {
        var destinationLoc = SetUtils.GetRandom(game.rand.Next(), steppableLocs);
        // 820 to override a possible attack command
        return obj.root.EffectEvolvifyImpulseCreate(820, destinationLoc).AsIImpulse();
      } else {
        // If not, see if there are plants nearby, and walk to them.
        var steppableLocsWithPlants = new SortedSet<Location>();
        foreach (var loc in steppableLocs) {
          if (game.level.terrain.tiles[loc].components.GetOnlyIPlantTTCOrNull().Exists()) {
            steppableLocsWithPlants.Add(loc);
          }
        }
        if (steppableLocsWithPlants.Count == 0) {
          return game.root.EffectNoImpulseCreate().AsIImpulse();
        }
        
        var destinationLoc = SetUtils.GetRandom(game.rand.Next(), steppableLocsWithPlants);
        // 820 to override a possible attack command
        return obj.root.EffectMoveImpulseCreate(820, destinationLoc).AsIImpulse();
      }
    }
  }
}
