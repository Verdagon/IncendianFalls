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

      var interactables = tile.components.GetAllIInteractableTTC();
      if (interactables.Count > 0) {
        var interactable = interactables[0];
        return interactable.Interact(game, superstate, unit, unit.location);
      } else {
        return "Nothing to interact with!";
      }
    }

    public static bool CanStep(
        Game game,
        Superstate superstate,
        Unit unit,
        Location destination) {
      if (!game.level.terrain.tiles.ContainsKey(destination)) {
        return false;
      }
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
