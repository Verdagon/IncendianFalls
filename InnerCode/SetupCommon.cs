using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class WalkableLocations {
    SortedSet<Location> walkableLocations;

    public WalkableLocations() {
      walkableLocations = new SortedSet<Location>();
    }
    public WalkableLocations(Terrain terrain) : this() {
      foreach (var locationAndTile in terrain.tiles) {
        if (locationAndTile.Value.walkable) {
          walkableLocations.Add(locationAndTile.Key);
        }
      }
    }
    public WalkableLocations(Terrain terrain, UnitMutSet units) : this(terrain) {
      foreach (var unit in units) {
        walkableLocations.Remove(unit.location);
      }
    }
    public WalkableLocations(Level level) : this(level.terrain, level.units) { }
    public Location GetRandom(int randomInt) {
      return SetUtils.GetRandom(randomInt, walkableLocations);
    }
    public List<Location> GetRandomN(Rand rand, int n) {
      return SetUtils.GetRandomN(rand, walkableLocations, n);
    }
    public void Remove(Location location) {
      walkableLocations.Remove(location);
    }
    public int Count {  get { return walkableLocations.Count;  } }
  }

  public class SetupCommon {
    public static void FillWithUnits(
        SSContext context,
        Rand rand,
        int currentTime,
        Terrain terrain,
        UnitMutSet units,
        WalkableLocations walkableLocations,
        int numUnits) {
      context.root.GetDeterministicHashCode();
      for (int i = 0; i < numUnits; i++) {
        context.root.GetDeterministicHashCode();
        var enemyLocation = walkableLocations.GetRandom(rand.Next());
        walkableLocations.Remove(enemyLocation);

        context.root.GetDeterministicHashCode();

        var components = IUnitComponentMutBunch.New(context.root);
        components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
        components.Add(context.root.EffectAttackAICapabilityUCCreate().AsIUnitComponent());

        Unit enemy;
        if (rand.Next(0, 5) == 0) {
          enemy =
              context.root.EffectUnitCreate(
                  context.root.EffectIUnitEventMutListCreate(),
                  true,
                  0,
                  enemyLocation,
                  "goblin",
                  3, 3,
                  0, 0,
                  600,
                  currentTime + 10,
                  components,
                  IItemMutBunch.New(context.root),
                  false);
        } else {
          components.Add(context.root.EffectBideAICapabilityUCCreate().AsIUnitComponent());
          enemy =
              context.root.EffectUnitCreate(
                  context.root.EffectIUnitEventMutListCreate(),
                  true,
                  0,
                  enemyLocation,
                  "irklingking",
                  20, 20,
                  40, 40,
                  600,
                  currentTime + 10,
                  components,
                  IItemMutBunch.New(context.root),
                  false);
        }
        context.root.GetDeterministicHashCode();
        units.Add(enemy);
        context.root.GetDeterministicHashCode();

        if (rand.Next(0, 3) == 0) {
          enemy.items.Add(new ArmorAsIItem(context.root.EffectArmorCreate()));
        }
        if (rand.Next(0, 3) == 0) {
          enemy.items.Add(new GlaiveAsIItem(context.root.EffectGlaiveCreate()));
        }
      }
    }

    public static Unit MakePlayer(
        SSContext context,
        Rand rand,
        UnitMutSet units,
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
              100, 100,
              600,
              0,
              IUnitComponentMutBunch.New(context.root),
              IItemMutBunch.New(context.root),
              true);
      units.Add(player);
      return player;
    }

  }
}
