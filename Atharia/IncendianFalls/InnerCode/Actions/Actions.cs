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
            game, superstate, attacker, victim, 15, true);
      }
      attacker.nextActionTime = attacker.nextActionTime + attacker.CalculateCombatTimeCost(900);
    }

    public static void Bump(
        Game game,
        Superstate superstate,
        Unit attacker,
        Unit victim,
        float multiplier,
        bool takeTime) {
      Eventer.broadcastUnitAttackEvent(game.root, game, attacker, victim);
      int initialDamage = 5;
      AttackInner(
          game,
          superstate,
          attacker,
          victim,
          initialDamage,
          true);
      if (takeTime) {
        attacker.nextActionTime = attacker.nextActionTime + attacker.CalculateCombatTimeCost(600);
      }
    }

    private static void AttackInner(
        Game game,
        Superstate superstate,
        Unit attacker,
        Unit victim,
        // A strong human's punch is about 5 damage.
        int initialDamage,
        bool physical) {
      int outgoingDamage = attacker.CalculateOutgoingDamage(initialDamage);
      int incomingDamage = victim.CalculateIncomingDamage(outgoingDamage);
      //game.root.logger.Info((attacker.Is(game.player) ? "Player" : "Enemy") + " does " + damage + " damage to " + (victim.Is(game.player) ? "player" : "enemy") + "!");
      victim.hp = victim.hp - incomingDamage;

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

    public static void Defy(
        Game game,
        Unit unit) {
      var detail = game.root.EffectDefyingUCCreate();
      unit.components.Add(detail.AsIUnitComponent());

      unit.nextActionTime = unit.nextActionTime + unit.CalculateCombatTimeCost(600);
      Eventer.broadcastUnitDefyingEvent(game.root, game, unit);
    }

    public static void Mire(
        Game game,
        Superstate superstate,
        Unit attacker,
        Unit victim) {
      // If they're already mired, then make it half as effective. We can
      // never completely time-stop them.
      int delay = 600;
      for (int i = 0; i < victim.components.GetAllMiredUC().Count; i++) {
        delay /= 2;
      }

      var detail = game.root.EffectMiredUCCreate();
      victim.components.Add(detail.AsIUnitComponent());
      // ...but DO delay their nextActionTime.

      var sorcerous = attacker.components.GetOnlySorcerousUCOrNull();
      Asserts.Assert(sorcerous != null);
      sorcerous.mp = sorcerous.mp - MIRE_COST;

      attacker.nextActionTime = attacker.nextActionTime + attacker.CalculateCombatTimeCost(600);
      victim.nextActionTime = victim.nextActionTime + victim.CalculateCombatTimeCost(delay);
      Eventer.broadcastUnitMiredEvent(game.root, game, attacker, victim);
    }

    public static void Counter(
        Game game,
        Unit unit) {
      var detail = game.root.EffectCounteringUCCreate();
      unit.components.Add(detail.AsIUnitComponent());

      var sorcerous = unit.components.GetOnlySorcerousUCOrNull();
      Asserts.Assert(sorcerous != null);
      sorcerous.mp = sorcerous.mp - 1;

      unit.nextActionTime = unit.nextActionTime + unit.CalculateCombatTimeCost(600);
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

      unit.nextActionTime = unit.nextActionTime + unit.CalculateMovementTimeCost(600);
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
    public static readonly int MIRE_COST = 2;
    public static readonly int FIRE_DAMAGE = 23;

    public static void Fire(
        Game game,
        Superstate superstate,
        Unit attacker,
        Unit victim) {
      Eventer.broadcastUnitFireEvent(game.root, game, attacker, victim);
      AttackInner(
          game, superstate, attacker, victim, FIRE_DAMAGE, false);
      attacker.nextActionTime = attacker.nextActionTime + attacker.CalculateCombatTimeCost(600);

      var sorcerous = attacker.components.GetOnlySorcerousUCOrNull();
      Asserts.Assert(sorcerous != null);
      sorcerous.mp = sorcerous.mp - FIRE_COST;
    }
  }
}
