using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class SetupCommon {
    public static void FillWithUnits(
        SSContext context,
        Game game,
        Superstate superstate,
        Level level,
        LevelSuperstate levelSuperstate,
        int numUnits) {
      var locations = levelSuperstate.GetNRandomWalkableLocations(game.rand, numUnits, true);

      for (int i = 0; i < numUnits; i++) {
        var enemyLocation = locations[i];

        var components = IUnitComponentMutBunch.New(context.root);
        components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
        components.Add(context.root.EffectAttackAICapabilityUCCreate().AsIUnitComponent());

        Unit enemy;
        if (game.rand.Next(0, 5) == 0) {
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
                  game.time + 10,
                  components,
                  IItemMutBunch.New(context.root),
                  false);
        } else {
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
                  game.time + 10,
                  components,
                  IItemMutBunch.New(context.root),
                  false);
        }
        level.units.Add(enemy);
        levelSuperstate.Add(enemy);

        if (game.rand.Next(0, 3) == 0) {
          enemy.items.Add(new ArmorAsIItem(context.root.EffectArmorCreate()));
        }
        if (game.rand.Next(0, 3) == 0) {
          enemy.items.Add(new GlaiveAsIItem(context.root.EffectGlaiveCreate()));
        }
      }
    }

    //public static Unit MakePlayer(
    //    SSContext context,
    //    Rand rand,
    //    UnitMutSet units,
    //    WalkableLocations walkableLocations) {

    //  var playerLocation = walkableLocations.GetRandom(rand.Next());
    //  walkableLocations.Remove(playerLocation);
    //  var player =
    //      context.root.EffectUnitCreate(
    //          context.root.EffectIUnitEventMutListCreate(),
    //          true,
    //          0,
    //          playerLocation,
    //          "chronomancer",
    //          90, 90,
    //          100, 100,
    //          600,
    //          0,
    //          IUnitComponentMutBunch.New(context.root),
    //          IItemMutBunch.New(context.root),
    //          true);
    //  units.Add(player);
    //  return player;
    //}

  }
}
