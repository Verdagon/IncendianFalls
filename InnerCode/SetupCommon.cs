using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class WalkableLocations {
    SortedSet<Location> walkableLocations;

    public WalkableLocations() {
      walkableLocations = new SortedSet<Location>();
    }
    public WalkableLocations(Terrain terrain, UnitMutBunch units) : this() {
      foreach (var locationAndTile in terrain.tiles) {
        if (locationAndTile.Value.walkable) {
          walkableLocations.Add(locationAndTile.Key);
        }
      }
      foreach (var unit in units) {
        walkableLocations.Remove(unit.location);
      }
    }
    public WalkableLocations(Level level) : this(level.terrain, level.units) { }
    public Location GetRandom(int randomInt) {
      return SetUtils.GetRandom(randomInt, walkableLocations);
    }
    public List<Location> GetRandomN(int randomInt, int n) {
      return SetUtils.GetRandomN(randomInt, walkableLocations, n);
    }
    public void Remove(Location location) {
      walkableLocations.Remove(location);
    }
  }

  public class SetupCommon {
    public static void FillWithUnits(
        SSContext context,
        Rand rand,
        int currentTime,
        Terrain terrain,
        UnitMutBunch units,
        WalkableLocations walkableLocations,
        int numUnits) {
      context.root.GetDeterministicHashCode();
      for (int i = 0; i < numUnits; i++) {
        context.root.GetDeterministicHashCode();
        var goblinLocation = walkableLocations.GetRandom(rand.Next());
        walkableLocations.Remove(goblinLocation);

        context.root.GetDeterministicHashCode();

        var goblin =
            context.root.EffectUnitCreate(
                context.root.EffectIUnitEventMutListCreate(),
                true,
                0,
                goblinLocation,
                "goblin",
                3, 3,
                40, 40,
                600,
                currentTime,
                new MoveDirectiveAsIDirective(MoveDirective.Null),
                context.root.EffectIDetailMutListCreate(),
                context.root.EffectIItemMutBunchCreate());
        context.root.GetDeterministicHashCode();
        units.Add(goblin);
        context.root.GetDeterministicHashCode();

        if (rand.Next(0, 3) == 0) {
          goblin.items.Add(new ArmorAsIItem(context.root.EffectArmorCreate()));
        }
        if (rand.Next(0, 3) == 0) {
          goblin.items.Add(new GlaiveAsIItem(context.root.EffectGlaiveCreate()));
        }
      }
    }

    public static Unit MakePlayer(
        SSContext context,
        Rand rand,
        UnitMutBunch units,
        WalkableLocations walkableLocations) {

      var playerLocation = walkableLocations.GetRandom(rand.Next());
      walkableLocations.Remove(playerLocation);
      var player =
          context.root.EffectUnitCreate(
              context.root.EffectIUnitEventMutListCreate(),
              true,
              0,
              playerLocation,
              "chronomancer",
              90, 90,
              40, 40,
              600,
              0,
              new MoveDirectiveAsIDirective(MoveDirective.Null),
              context.root.EffectIDetailMutListCreate(),
              context.root.EffectIItemMutBunchCreate());
      units.Add(player);
      return player;
    }

  }
}
