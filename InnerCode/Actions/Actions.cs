using System;
using Atharia.Model;

namespace IncendianFalls {
  public class Actions {
    public static void Attack(
        Game game,
        LiveUnitByLocationMap liveUnitByLocationMap,
        Unit attacker,
        Unit victim) {
      Eventer.broadcastUnitAttackEvent(game.root, game, attacker, victim);

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
      game.root.logger.Info((attacker.Is(game.player) ? "Player" : "Enemy") + " does " + damage + " damage to " + (victim.Is(game.player) ? "player" : "enemy") + "!");
      victim.hp = victim.hp - damage;

      if (victim.hp <= 0) {
        victim.alive = false;
        victim.lifeEndTime = game.time;
        // Bump the victim up to be the next acting unit.
        victim.nextActionTime = game.time;
        liveUnitByLocationMap.Remove(victim);
      }

      attacker.nextActionTime = attacker.nextActionTime + attacker.inertia;
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
        if (thing is DownStaircaseTerrainTileComponentAsITerrainTileComponent down) {
          return true;
        }
      }
      return false;
    }

    public static bool Interact(
        SSContext context,
        Game game,
        Unit unit) {
      Asserts.Assert(unit.Is(game.player));
      var player = game.player;

      if (TileHasDownStaircase(context, game, player.location)) {
        string levelName = "Falls" + game.levels.Count;

        // Remove the player, player now has no level.
        game.level.units.Remove(player);
        var nextLevel =
            MakeLevel.MakeNextLevel(
                context, game.rand, game.time, game.squareLevelsOnly, levelName);
        game.levels.Add(nextLevel);

        var walkableLocations = new WalkableLocations(nextLevel.terrain, nextLevel.units);
        player.location = walkableLocations.GetRandom(game.rand.Next());
        nextLevel.units.Add(player);

        game.level = nextLevel;

        unit.nextActionTime = unit.nextActionTime + unit.inertia;

        return true;
      } else {
        return false;
      }
    }

    public static bool CanStep(
        Game game,
        LiveUnitByLocationMap liveUnitByLocationMap,
        Unit unit,
        Location destination) {
      if (!game.level.terrain.tiles[destination].walkable) {
        return false;
      }
      if (!game.level.terrain.pattern.LocationsAreAdjacent(unit.location, destination, game.level.considerCornersAdjacent)) {
        return false;
      }
      if (liveUnitByLocationMap.ContainsKey(destination)) {
        return false;
      }
      return true;
    }

    public static void Step(
          Game game,
          LiveUnitByLocationMap liveUnitByLocationMap,
          Unit unit,
          Location destination) {
      Asserts.Assert(game.level.terrain.tiles[destination].walkable);
      Asserts.Assert(game.level.terrain.pattern.LocationsAreAdjacent(unit.location, destination, game.level.considerCornersAdjacent));
      Asserts.Assert(!liveUnitByLocationMap.ContainsKey(destination));

      bool removed = liveUnitByLocationMap.Remove(unit);
      Asserts.Assert(removed);
      unit.location = destination;
      liveUnitByLocationMap.Add(unit);

      unit.nextActionTime = unit.nextActionTime + unit.inertia;
    }

  }
}
