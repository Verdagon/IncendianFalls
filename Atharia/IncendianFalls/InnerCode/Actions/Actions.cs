
using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class Actions {
    public static readonly int FIRE_COST = 18;
    public static readonly int FIRE_BOMB_COST = 10;
    public static readonly int MIRE_COST = 2;
    public static readonly int FIRE_DAMAGE = 23;
    public static readonly int FIRE_BOMB_DAMAGE = 32;
    public static readonly int LIGHTNING_CHARGE_DAMAGE = 4;
    public static readonly int BUMP_TIME_COST = 600;

    public static void UnleashBide(
        Game game,
        Superstate superstate,
        Unit attacker,
        SortedSet<Location> affectedTileLocations) {
      List<Unit> victims = new List<Unit>();
      List<Location> otherLocations = new List<Location>();
      foreach (var affectedTileLocation in affectedTileLocations) {
        if (game.level.terrain.tiles.ContainsKey(affectedTileLocation)) {
          var victim = superstate.levelSuperstate.GetLiveUnitAt(affectedTileLocation);
          if (victim.Exists() && !victim.Is(attacker) && victim.Alive()) {
            victims.Add(victim);
            AttackInner(game, superstate, attacker, victim, 15, true);
          } else {
            otherLocations.Add(affectedTileLocation);
          }
        }
      }
      Eventer.broadcastUnitUnleashBideEvent(game.root, game, attacker, victims, otherLocations);

      attacker.nextActionTime = attacker.nextActionTime + attacker.CalculateCombatTimeCost(900);
    }

    public static void Bump(
        Game game,
        Superstate superstate,
        Unit attacker,
        Unit victim,
        int multiplierPercent,
        bool takeTime) {
      Eventer.broadcastUnitAttackEvent(game.root, game, attacker, victim);
      int initialDamage = 5 * multiplierPercent / 100;
      AttackInner(
          game,
          superstate,
          attacker,
          victim,
          initialDamage,
          true);
      if (takeTime) {
        attacker.nextActionTime = attacker.nextActionTime + attacker.CalculateCombatTimeCost(BUMP_TIME_COST);
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
      AttackedInner(game, superstate, victim, outgoingDamage, physical);
      if (victim.Alive()) {
        foreach (var reactor in new List<IReactingToAttacksUC>(victim.components.GetAllIReactingToAttacksUC())) {
          if (victim.Exists()) {
            reactor.React(game, superstate, victim, attacker);
          }
        }
      }
    }

    private static void AttackedInner(
        Game game,
        Superstate superstate,
        Unit victim,
        // A strong human's punch is about 5 damage.
        int outgoingDamage,
        bool physical) {
      int incomingDamage = victim.CalculateIncomingDamage(outgoingDamage);
      //game.root.logger.Info((attacker.Is(game.player) ? "Player" : "Enemy") + " does " + damage + " damage to " + (victim.Is(game.player) ? "player" : "enemy") + "!");
      victim.hp = victim.hp - incomingDamage;

      if (victim.hp <= 0) {
        victim.lifeEndTime = game.time;
        // Bump the victim up to be the next acting unit.
        victim.nextActionTime = game.time;
        superstate.levelSuperstate.RemoveUnit(victim);
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
      victim.nextActionTime = victim.nextActionTime + delay;
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
        return interactable.Interact(context, game, superstate, unit, unit.location);
      } else {
        return "Nothing to interact with!";
      }
    }

    public static bool CanTeleportTo(
        Game game,
        Superstate superstate,
        Location destination) {
      if (!game.level.terrain.tiles.ContainsKey(destination)) {
        return false;
      }
      if (!game.level.terrain.tiles[destination].IsWalkable()) {
        return false;
      }
      if (superstate.levelSuperstate.LocationContainsUnit(destination)) {
        return false;
      }
      return true;
    }

    public static bool CanStep(
        Game game,
        Superstate superstate,
        Unit unit,
        Location destination) {
      if (!game.level.terrain.pattern.LocationsAreAdjacent(unit.location, destination, game.level.ConsiderCornersAdjacent())) {
        return false;
      }
      if (game.level.terrain.GetElevationDifference(unit.location, destination) > 2) {
        return false;
      }
      if (!CanTeleportTo(game, superstate, destination)) {
        return false;
      }
      return true;
    }

    public static void Step(
          Game game,
          Superstate superstate,
          Unit unit,
          Location destination,
          bool overrideAdjacentCheck,
          bool costsTime) {
      Asserts.Assert(game.level.terrain.tiles[destination].IsWalkable(), "Not walkable!");
      Asserts.Assert(
        overrideAdjacentCheck || game.level.terrain.pattern.LocationsAreAdjacent(unit.location, destination, game.level.ConsiderCornersAdjacent()),
        "Adjacent check failed!");
      Asserts.Assert(!superstate.levelSuperstate.LocationContainsUnit(destination), "Unit is there!");

      bool removed = superstate.levelSuperstate.RemoveUnit(unit);
      Asserts.Assert(removed, "Not removed!");
      unit.location = destination;
      superstate.levelSuperstate.AddUnit(unit);

      if (costsTime) {
        unit.nextActionTime = unit.nextActionTime + unit.CalculateMovementTimeCost(600);
      }
    }

    public static void Evaporate(
        Game game,
        Superstate superstate,
        Unit unit) {
      unit.lifeEndTime = game.time;
      // Bump the victim up to be the next acting unit.
      unit.nextActionTime = game.time;
      superstate.levelSuperstate.RemoveUnit(unit);
    }

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

    public static void PlaceFireBomb(
        Game game,
        Superstate superstate,
        Unit attacker,
        Location location) {
      game.level.terrain.tiles[location].components.Add(
        game.root.EffectFireBombTTCCreate(2).AsITerrainTileComponent());
      superstate.levelSuperstate.AddedActingTTC(location);
      attacker.nextActionTime = attacker.nextActionTime + attacker.CalculateCombatTimeCost(600);

      var sorcerous = attacker.components.GetOnlySorcerousUCOrNull();
      Asserts.Assert(sorcerous != null);
      sorcerous.mp = sorcerous.mp - FIRE_BOMB_COST;
    }

    public static void ExplodeFireBomb(
        Game game,
        Superstate superstate,
        Location location) {
      Unit poorSuckerOnThisTile = superstate.levelSuperstate.GetLiveUnitAt(location);
      Eventer.broadcastUnitFireBombedEvent(game.root, game, poorSuckerOnThisTile, location);
      if (poorSuckerOnThisTile.Exists()) {
        AttackedInner(game, superstate, poorSuckerOnThisTile, FIRE_BOMB_DAMAGE, false);
      }
    }

    public static void LightningCharge(
        Game game,
        Superstate superstate,
        Unit unit) {
      unit.components.Add(game.root.EffectLightningChargedUCCreate().AsIUnitComponent());
      Eventer.broadcastUnitFireBombedEvent(game.root, game, unit, unit.location);
      AttackedInner(game, superstate, unit, LIGHTNING_CHARGE_DAMAGE, false);
    }
  }
}
