using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class Actions {
    public static void UnleashBide(
        Game game,
        Superstate superstate,
        Unit attacker,
        List<Unit> victims) {
      Eventer.broadcastUnitUnleashBideEvent(game.root, game, attacker, victims);
      foreach (var victim in victims) {
        AttackInner(
            game, superstate, attacker, victim, attacker.strength * 3, true);
      }
      attacker.nextActionTime = attacker.nextActionTime + attacker.CalculateInertia() * 3 / 2;
    }

    public static void Bump(
        Game game,
        Superstate superstate,
        Unit attacker,
        Unit victim,
        float multiplier,
        bool takeTime) {
      Eventer.broadcastUnitAttackEvent(game.root, game, attacker, victim);
      AttackInner(
          game,
          superstate,
          attacker,
          victim,
          (int)Math.Floor(attacker.strength * multiplier),
          true);
      if (takeTime) {
        attacker.nextActionTime = attacker.nextActionTime + attacker.CalculateInertia();
      }
    }

    private static void AttackInner(
        Game game,
        Superstate superstate,
        Unit attacker,
        Unit victim,
        int damage,
        bool physical) {
      foreach (var item in attacker.components.GetAllIOffenseItem()) {
        damage = item.AffectOutgoingDamage(physical, damage);
      }
      foreach (var detail in victim.components.GetAllIDefenseUC()) {
        damage = detail.AffectIncomingDamage(damage);
      }
      foreach (var detail in victim.components.GetAllIDefenseItem()) {
        damage = detail.AffectIncomingDamage(damage);
      }
      //game.root.logger.Info((attacker.Is(game.player) ? "Player" : "Enemy") + " does " + damage + " damage to " + (victim.Is(game.player) ? "player" : "enemy") + "!");
      victim.hp = victim.hp - damage;

      if (victim.hp <= 0) {
        victim.alive = false;
        victim.lifeEndTime = game.time;
        // Bump the victim up to be the next acting unit.
        victim.nextActionTime = game.time;
        superstate.levelSuperstate.Remove(victim);
      } else {
        foreach (var reactor in new List<IReactingToAttacksUC>(victim.components.GetAllIReactingToAttacksUC())) {
          if (victim.Exists()) {
            reactor.React(game, superstate, victim, attacker);
          }
        }
      }
    }

    public static void Defend(
        Game game,
        Unit unit) {
      var detail = game.root.EffectShieldingUCCreate();
      unit.components.Add(detail.AsIUnitComponent());

      unit.nextActionTime = unit.nextActionTime + unit.CalculateInertia();
      Eventer.broadcastUnitShieldingEvent(game.root, game, unit);
    }

    public static void Counter(
        Game game,
        Unit unit) {
      var detail = game.root.EffectCounteringUCCreate();
      unit.components.Add(detail.AsIUnitComponent());

      unit.mp = unit.mp - 1;

      unit.nextActionTime = unit.nextActionTime + unit.CalculateInertia();
      Eventer.broadcastUnitCounteringEvent(game.root, game, unit);
    }

    public static string Interact(
        SSContext context,
        Game game,
        Superstate superstate,
        Unit unit) {
      var tile = game.level.terrain.tiles[unit.location];
      var staircase = tile.components.GetOnlyStaircaseTTCOrNull();
      if (staircase.Exists()) {
        var previousLevel = game.level;
        var previousLevelPortalIndex = staircase.portalIndex;

        // Move the player from this level to the next one.
        if (!staircase.destinationLevel.Exists()) {
          // Unless this is going to -1, which means it's the first level staircase,
          // so do nothing.
          if (staircase.destinationLevelPortalIndex == -1) {
            return "I can't go back, I must go forward!";
          }

          game.level.ExitUnit(game, superstate.levelSuperstate, unit);

          MakeLevel.MakeNextLevel(
              out var nextLevel,
              out var nextLevelSuperstate,
              context,
              game,
              superstate,
              game.level,
              game.level.depth + 1);

          game.level = nextLevel;
          superstate.levelSuperstate = nextLevelSuperstate;

          staircase.destinationLevel = nextLevel;
          staircase.destinationLevelPortalIndex = 0;

          game.level.EnterUnit(game, superstate.levelSuperstate, unit, previousLevel, previousLevelPortalIndex);
        } else {
          game.level.ExitUnit(game, superstate.levelSuperstate, unit);

          game.level = staircase.destinationLevel;
          superstate.levelSuperstate = new LevelSuperstate(game.level);

          // These will likely be in the distant past, since it's been a while since we've
          // visited here. We'll want to bump them all up to the near future.
          Asserts.Assert(game.time >= game.level.time);

          // Add that amount to every unit, so it's as if we just left this level.
          foreach (var nativeUnit in game.level.units) {
            nativeUnit.nextActionTime =
                nativeUnit.nextActionTime + (game.time - game.level.time);
          }

          game.level.EnterUnit(game, superstate.levelSuperstate, unit, previousLevel, previousLevelPortalIndex);
        }

        // Note how we are NOT setting unit.nextActionTime here. That's because
        // we want the player to have the first action after they descend.
        return "";
      }

      var items = tile.components.GetAllIItem();
      if (items.Count > 0) {
        var item = items[0];

        tile.components.Remove(item.AsITerrainTileComponent());

        if (item is ArmorAsIItem && unit.components.GetOnlyArmorOrNull().Exists()) {
          item.Destruct();
        } else if (item is GlaiveAsIItem && unit.components.GetOnlyGlaiveOrNull().Exists()) {
          item.Destruct();
        } else if (item is InertiaRingAsIItem && unit.components.GetOnlyInertiaRingOrNull().Exists()) {
          item.Destruct();
        } else {
          unit.components.Add(item.AsIUnitComponent());

          if (item is HealthPotionAsIItem hp) {
            hp.obj.Use(game, superstate, unit);
          } else if (item is ManaPotionAsIItem mp) {
            mp.obj.Use(game, superstate, unit);
          }
        }
        return "";
      }

      return "Nothing to interact with!";
    }

    public static bool CanStep(
        Game game,
        Superstate superstate,
        Unit unit,
        Location destination) {
      if (!game.level.terrain.tiles[destination].IsWalkable()) {
        return false;
      }
      if (!game.level.terrain.pattern.LocationsAreAdjacent(unit.location, destination, game.level.ConsiderCornersAdjacent())) {
        return false;
      }
      if (game.level.terrain.GetElevationDifference(unit.location, destination) > 2) {
        return false;
      }
      if (superstate.levelSuperstate.ContainsKey(destination)) {
        return false;
      }
      return true;
    }

    public static void Step(
          Game game,
          Superstate superstate,
          Unit unit,
          Location destination,
          bool overrideAdjacentCheck) {
      Asserts.Assert(game.level.terrain.tiles[destination].IsWalkable());
      Asserts.Assert(overrideAdjacentCheck || game.level.terrain.pattern.LocationsAreAdjacent(unit.location, destination, game.level.ConsiderCornersAdjacent()));
      Asserts.Assert(!superstate.levelSuperstate.ContainsKey(destination));

      bool removed = superstate.levelSuperstate.Remove(unit);
      Asserts.Assert(removed);
      unit.location = destination;
      superstate.levelSuperstate.Add(unit);

      unit.nextActionTime = unit.nextActionTime + unit.CalculateInertia();
    }

    public static void Evaporate(
        Game game,
        Superstate superstate,
        Unit unit) {
      unit.alive = false;
      unit.lifeEndTime = game.time;
      superstate.levelSuperstate.Remove(unit);
    }

    public static readonly int FIRE_COST = 18;
    public static readonly int FIRE_DAMAGE = 23;

    public static void Fire(
        Game game,
        Superstate superstate,
        Unit attacker,
        Unit victim) {
      Eventer.broadcastUnitFireEvent(game.root, game, attacker, victim);
      AttackInner(
          game, superstate, attacker, victim, FIRE_DAMAGE, false);
      attacker.nextActionTime = attacker.nextActionTime + attacker.CalculateInertia();

      attacker.mp = attacker.mp - FIRE_COST;
    }
  }
}
