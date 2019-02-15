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
    public void Remove(Location location) {
      walkableLocations.Remove(location);
    }
  }

  public class SetupCommon {
    public static void FillWithUnits(
        Root root,
        Rand rand,
        Terrain terrain,
        UnitMutBunch units,
        WalkableLocations walkableLocations,
        int numUnits) {
      for (int i = 0; i < numUnits; i++) {
        var goblinLocation = walkableLocations.GetRandom(rand.Next());
        walkableLocations.Remove(goblinLocation);

        var goblin =
            root.EffectUnitCreate(
                root.EffectIUnitEventMutListCreate(),
                true,
                0,
                goblinLocation,
                "goblin",
                3, 3,
                40, 40,
                600,
                0,
                new MoveDirectiveAsIDirective(MoveDirective.Null),
                root.EffectIDetailMutListCreate(),
                root.EffectIItemMutBunchCreate());
        units.Add(goblin);

        if (rand.Next(0, 3) == 0) {
          goblin.items.Add(new ArmorAsIItem(root.EffectArmorCreate()));
        }
        if (rand.Next(0, 3) == 0) {
          goblin.items.Add(new GlaiveAsIItem(root.EffectGlaiveCreate()));
        }
      }
    }

    public static Unit MakePlayer(
        Root root,
        Rand rand,
        UnitMutBunch units,
        WalkableLocations walkableLocations) {

      var playerLocation = walkableLocations.GetRandom(rand.Next());
      walkableLocations.Remove(playerLocation);
      var player =
          root.EffectUnitCreate(
              root.EffectIUnitEventMutListCreate(),
              true,
              0,
              playerLocation,
              "chronomancer",
              90, 90,
              40, 40,
              600,
              0,
              new MoveDirectiveAsIDirective(MoveDirective.Null),
              root.EffectIDetailMutListCreate(),
              root.EffectIItemMutBunchCreate());
      units.Add(player);
      return player;
    }

  }
}
