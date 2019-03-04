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
        AttackInner(game, superstate, attacker, victim);
      }
      attacker.nextActionTime = attacker.nextActionTime + attacker.inertia * 3 / 2;
    }

    public static void Bump(
        Game game,
        Superstate superstate,
        Unit attacker,
        Unit victim) {
      Eventer.broadcastUnitAttackEvent(game.root, game, attacker, victim);
      AttackInner(game, superstate, attacker, victim);
      attacker.nextActionTime = attacker.nextActionTime + attacker.inertia;
    }

    private static void AttackInner(
        Game game,
        Superstate superstate,
        Unit attacker,
        Unit victim) {
      int damage = 5;
      foreach (var item in attacker.items) {
        damage = item.AffectOutgoingDamage(damage);
      }
      foreach (var detail in victim.components.GetAllIDefenseUC()) {
        damage = detail.AffectIncomingDamage(damage);
      }
      foreach (var item in victim.items) {
        damage = item.AffectIncomingDamage(damage);
      }
      //game.root.logger.Info((attacker.Is(game.player) ? "Player" : "Enemy") + " does " + damage + " damage to " + (victim.Is(game.player) ? "player" : "enemy") + "!");
      victim.hp = victim.hp - damage;

      if (victim.hp <= 0) {
        victim.alive = false;
        victim.lifeEndTime = game.time;
        // Bump the victim up to be the next acting unit.
        victim.nextActionTime = game.time;
        superstate.liveUnitByLocationMap.Remove(victim);
      }
    }

    public static void Defend(
        Game game,
        Unit unit) {
      var detail = game.root.EffectShieldingUCCreate();
      unit.components.Add(detail.AsIUnitComponent());

      unit.nextActionTime = unit.nextActionTime + unit.inertia;
    }

    private static bool TileHasDownStaircase(
        SSContext context,
        Game game,
        Location location) {
      foreach (var thing in game.level.terrain.tiles[location].components) {
        if (thing is DownStaircaseTTCAsITerrainTileComponent down) {
          return true;
        }
      }
      return false;
    }

    public static bool Interact(
        SSContext context,
        Game game,
        Superstate superstate,
        Unit unit) {
      if (TileHasDownStaircase(context, game, unit.location)) {
        string levelName = "Falls" + game.levels.Count;

        // Move the player from this level to the next one.
        game.level.units.Remove(unit);
        var nextLevel =
            MakeLevel.MakeNextLevel(
                context, game.rand, game.time, game.squareLevelsOnly, levelName);
        game.levels.Add(nextLevel);

        var walkableLocations = new WalkableLocations(nextLevel.terrain, nextLevel.units);
        unit.location = walkableLocations.GetRandom(game.rand.Next());
        nextLevel.units.Add(unit);

        game.level = nextLevel;
        superstate.liveUnitByLocationMap.Reconstruct(game);

        // Note how we are NOT setting unit.nextActionTime here. That's because
        // we want the player to have the first action after they descend.
        return true;
      } else {
        return false;
      }
    }

    public static bool CanStep(
        Game game,
        Superstate superstate,
        Unit unit,
        Location destination) {
      if (!game.level.terrain.tiles[destination].walkable) {
        return false;
      }
      if (!game.level.terrain.pattern.LocationsAreAdjacent(unit.location, destination, game.level.considerCornersAdjacent)) {
        return false;
      }
      if (superstate.liveUnitByLocationMap.ContainsKey(destination)) {
        return false;
      }
      return true;
    }

    public static void Step(
          Game game,
          Superstate superstate,
          Unit unit,
          Location destination) {
      Asserts.Assert(game.level.terrain.tiles[destination].walkable);
      Asserts.Assert(game.level.terrain.pattern.LocationsAreAdjacent(unit.location, destination, game.level.considerCornersAdjacent));
      Asserts.Assert(!superstate.liveUnitByLocationMap.ContainsKey(destination));

      bool removed = superstate.liveUnitByLocationMap.Remove(unit);
      Asserts.Assert(removed);
      unit.location = destination;
      superstate.liveUnitByLocationMap.Add(unit);

      unit.nextActionTime = unit.nextActionTime + unit.inertia;
    }

    public static void Evaporate(
        Game game,
        Superstate superstate,
        Unit unit) {
      unit.alive = false;
      unit.lifeEndTime = game.time;
      superstate.liveUnitByLocationMap.Remove(unit);
    }
  }
}
