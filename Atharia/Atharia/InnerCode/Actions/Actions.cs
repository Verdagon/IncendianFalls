
using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class Actions {
    public static readonly int FIRE_COST = 18;
    public static readonly int FIRE_DAMAGE = 23;
    public static readonly int BLAZE_COST = 10;
    public static readonly int BLAZE_RANGE = 5;
    public static readonly int BLAZE_DAMAGE = 7;
    public static readonly int BLAZE_DURATION = 4;
    public static readonly int EXPLOSION_RANGE = 5;
    public static readonly int EXPLOSION_COST = 15;
    public static readonly int EXPLOSION_DELAY = 3;
    public static readonly int EXPLOSION_DAMAGE = 32;
    public static readonly int MIRE_COST = 2;
    public static readonly int FIRE_BOMB_COST = 10;
    public static readonly int FIRE_BOMB_DAMAGE = 32;
    public static readonly int LIGHTNING_CHARGE_DAMAGE = 4;
    public static readonly int BUMP_TIME_COST = 600;

    public const int LEAP_DISTANCE = 3;


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
      Eventer.broadcastUnitUnleashBideEvent(game, attacker, victims, otherLocations);

      attacker.nextActionTime = attacker.nextActionTime + attacker.CalculateCombatTimeCost(900);
      game.actionNum++;
    }

    public static void Bump(
        Game game,
        Superstate superstate,
        Unit attacker,
        Unit victim,
        int multiplierPercent,
        bool takeTime) {
      Eventer.broadcastUnitAttackEvent(game, attacker, victim);
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
      game.actionNum++;
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
      Eventer.broadcastUnitDefyingEvent(game, unit);
      game.actionNum++;
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
      Eventer.broadcastUnitMiredEvent(game, attacker, victim);
      game.actionNum++;
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
      Eventer.broadcastUnitCounteringEvent(game, unit);
      game.actionNum++;
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
      game.actionNum++;
    }

    public static bool CanTeleportTo(
        LevelSuperstate levelSuperstate,
        Terrain terrain,
        Location destination,
        bool checkUnitNotPresent) {
      if (!terrain.tiles.ContainsKey(destination)) {
        return false;
      }
      if (!terrain.tiles[destination].IsWalkable()) {
        return false;
      }
      if (checkUnitNotPresent && levelSuperstate.LocationContainsUnit(destination)) {
        return false;
      }
      return true;
    }

    // public static bool CanHopExpensive(
    //     LevelSuperstate levelSuperstate,
    //     Level level,
    //     Location from,
    //     Location to,
    //     bool checkUnitNotPresent) {
    //   return CanSttep(levelSuperstate, level, from, to, checkUnitNotPresent) ||
    //       CanLeapExpensive(levelSuperstate, level, from, to, checkUnitNotPresent);
    // }

    public static bool CanSttep(
        LevelSuperstate levelSuperstate, Level level, Location from, Location to, bool checkUnitNotPresent) {
      if (!CanTeleportTo(levelSuperstate, level.terrain, to, checkUnitNotPresent)) {
        return false;
      }
      if (!level.terrain.pattern.LocationsAreAdjacent(from, to, level.terrain.considerCornersAdjacent)) {
        return false;
      }
      var fromTile = level.terrain.tiles[from];
      var fromElevation = fromTile.elevation;
      var toTile = level.terrain.tiles[to];
      var toElevation = toTile.elevation;
      var elevationFine = toElevation <= fromElevation + 1 && toElevation >= fromElevation - 2;
      if (!elevationFine) {
        return false;
      }
      return true;
    }

    // public static bool CanLeapExpensive(
    //     LevelSuperstate levelSuperstate,
    //     Level level,
    //     Location source,
    //     Location destination,
    //     bool checkUnitNotPresent) {
    //   if (!CanTeleportTo(levelSuperstate, level.terrain, destination, checkUnitNotPresent)) {
    //     return false;
    //   }
    //   if (level.terrain.tiles[source].elevation != level.terrain.tiles[destination].elevation) {
    //     // Can only leap to the same elevation
    //     return false;
    //   }
    //   if (level.terrain.pattern.GetTileCenter(source).distance(level.terrain.pattern.GetTileCenter(destination)) > LEAP_DISTANCE) {
    //     // If it's further away then LEAP_DISTANCE, then we certainly wont find a path (which might not even
    //     // go in a bee-line!) to get there in under LEAP_DISTANCE.
    //     return false;
    //   }
    //   var inLeapingRangeExplorer =
    //       new AStarExplorer(
    //           new SortedSet<Location> {source},
    //           (to) => level.terrain.pattern.GetAdjacentLocations(to, level.terrain.considerCornersAdjacent),
    //           (a, b, totalCost) => {
    //             return totalCost <= LEAP_DISTANCE &&
    //                 // Can't leap over tiles that dont exist or arent walkable
    //                 CanTeleportTo(levelSuperstate, level.terrain, b, checkUnitNotPresent);
    //           },
    //           (a) => a == destination,
    //           AStarExplorer.MakeDistanceCostGuesser(level.terrain.pattern, destination),
    //           (a, b) => 1); // consider each space to be 1 distance
    //   if (!inLeapingRangeExplorer.WasExplored(destination)) {
    //     // We can't leap there, bail.
    //     return false;
    //   }
    //   
    //   var steppableInRangeExplorer =
    //       new AStarExplorer(
    //           new SortedSet<Location> {source},
    //           (to) => level.terrain.pattern.GetAdjacentLocations(to, level.terrain.considerCornersAdjacent),
    //           (from, to, totalCost) => {
    //             return totalCost <= LEAP_DISTANCE && CanSttep(levelSuperstate, level, from, to, false);
    //           },
    //           (a) => a == destination,
    //           AStarExplorer.MakeDistanceCostGuesser(level.terrain.pattern, destination),
    //           (a, b) => 1); // consider each space to be 1 distance
    //   var steppable = steppableInRangeExplorer.WasExplored(destination);
    //   if (steppable) {
    //     // We CAN step to there, so it's not eligible for leaping.
    //     return false;
    //   }
    //
    //   return true;
    // }

    public static SortedSet<Location> GetLeapableLocationsExpensive(
        LevelSuperstate levelSuperstate,
        Level level,
        Location source,
        bool checkUnitNotPresent) {
      var sourceLocElevation = level.terrain.tiles[source].elevation;

      // We choose an arbitrary* number to multiply each step by, let's say 4.
      // So, if the max leap distance is 3, and each step will cost 4, so we're
      // looking for paths <= 12.
      // *It's not actually arbitrary. We choose a number equal to the max leap range + 1.
      // Also, if we go over something that's higher elevation thus blocks the leap, we add 1.
      // This way, we can know at the end whether the path crossed something that blocked
      // the leap, by seeing if it's not a multiple of e.g. 4.
      var STEP_COST = LEAP_DISTANCE + 1;
      var inLeapingRangeExplorer =
          new AStarExplorer(
              new SortedSet<Location> {source},
              (to) => level.terrain.GetAdjacentExistingLocations(to, level.terrain.considerCornersAdjacent),
              (a, b, totalCost) => {
                // This would be totalCost <= 12, but we want to allow the +1s from the
                // things blocking visibility. There can be up to LEAP_DISTANCE of them
                // so really we want < 16, hence the + 1 here.
                return totalCost < (LEAP_DISTANCE + 1) * STEP_COST &&
                    // Can't leap over tiles that dont exist or arent walkable
                    CanTeleportTo(levelSuperstate, level.terrain, b, checkUnitNotPresent);
              },
              (a) => false,
              (a) => 0,
              (a, b) => {
                var blocksLeap = level.terrain.tiles[b].elevation > sourceLocElevation;
                return STEP_COST + (blocksLeap ? 1 : 0);
              });
      var locsInLeapingRange = inLeapingRangeExplorer.getClosedLocations();
      locsInLeapingRange.Remove(source);

      var steppableInLeapingRangeExplorer =
          new AStarExplorer(
              new SortedSet<Location> {source},
              (to) => level.terrain.pattern.GetAdjacentLocations(to, level.terrain.considerCornersAdjacent),
              (from, to, totalCost) => {
                return totalCost <= LEAP_DISTANCE &&
                    CanSttep(levelSuperstate, level, from, to, false);
              },
              (a) => false,
              (a) => 0,
              (a, b) => 1); // consider each space to be 1 distance
      var steppableLocsInLeapingRange = steppableInLeapingRangeExplorer.getClosedLocations();
      steppableLocsInLeapingRange.Remove(source);
      
      var leapableLocs = new SortedSet<Location>();
      foreach (var locInLeapingRange in locsInLeapingRange) {
        if (inLeapingRangeExplorer.GetCostTo(locInLeapingRange) % STEP_COST > 0) {
          // There was something that blocked the leap.
          continue;
        }
        if (steppableLocsInLeapingRange.Contains(locInLeapingRange)) {
          // Dont allow leaping to anywhere we could just take a few steps to
          continue;
        }
        if (level.terrain.tiles[locInLeapingRange].elevation != sourceLocElevation) {
          // Can only leap to the same elevation
          continue;
        }
        leapableLocs.Add(locInLeapingRange);
      }
      Asserts.Assert(!leapableLocs.Contains(source));
      return leapableLocs;
    }
    public static SortedSet<Location> GetStteppableLocations(
        LevelSuperstate levelSuperstate, Level level, Location source, bool checkUnitNotPresent) {
      var steppableLocs = new SortedSet<Location>();
      var adjacentLocs = level.terrain.pattern.GetAdjacentLocations(source, level.terrain.considerCornersAdjacent);
      foreach (var adjacentLoc in adjacentLocs) {
        if (CanSttep(levelSuperstate, level, source, adjacentLoc, checkUnitNotPresent)) {
          steppableLocs.Add(adjacentLoc);
        }
      }
      Asserts.Assert(!steppableLocs.Contains(source));
      return steppableLocs;
    }

    public static SortedSet<Location> GetHoppableLocationsExpensive(
        LevelSuperstate levelSuperstate,
        Level level,
        Location source,
        bool checkUnitNotPresent) {
      return SetUtils.Union(
          GetStteppableLocations(levelSuperstate, level, source, checkUnitNotPresent),
          GetLeapableLocationsExpensive(levelSuperstate, level, source, checkUnitNotPresent));
    }

    public static void Hop(
          Game game,
          Superstate superstate,
          Unit unit,
          Location destination,
          bool costsTime) {
      Asserts.Assert(superstate.levelSuperstate.CanHop(unit.location, destination, true));
      Teleport(game, superstate, unit, destination);
      if (costsTime) {
        unit.nextActionTime = unit.nextActionTime + unit.CalculateMovementTimeCost(600);
      }
      game.actionNum++;
    }
    
    public static void Teleport(
        Game game,
        Superstate superstate,
        Unit unit,
        Location destination) {
      Asserts.Assert(CanTeleportTo(superstate.levelSuperstate, game.level.terrain, destination, true));

      bool removed = superstate.levelSuperstate.RemoveUnit(unit);
      Asserts.Assert(removed, "Not removed!");
      unit.location = destination;
      superstate.levelSuperstate.AddUnit(unit);
    }

    public static void Sttep(
        Game game,
        Superstate superstate,
        Unit unit,
        Location destination,
        bool costsTime) {
      Asserts.Assert(CanSttep(superstate.levelSuperstate, game.level, unit.location, destination, true));
      Teleport(game, superstate, unit, destination);
      if (costsTime) {
        unit.nextActionTime = unit.nextActionTime + unit.CalculateMovementTimeCost(600);
      }
      game.actionNum++;
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
      Eventer.broadcastUnitFireEvent(game, attacker, victim);
      AttackInner(
          game, superstate, attacker, victim, FIRE_DAMAGE, false);
      attacker.nextActionTime = attacker.nextActionTime + attacker.CalculateCombatTimeCost(600);

      var sorcerous = attacker.components.GetOnlySorcerousUCOrNull();
      Asserts.Assert(sorcerous != null);
      sorcerous.mp = sorcerous.mp - FIRE_COST;
      game.actionNum++;
    }

    public static void Blaze(
        Game game,
        Superstate superstate,
        Unit attacker,
        Location targetLoc) {
      Eventer.broadcastUnitBlazeEvent(game, attacker, targetLoc);

      game.level.terrain.tiles[targetLoc].components.Add(
          game.root.EffectOnFireTTCCreate(Actions.BLAZE_DURATION).AsITerrainTileComponent());
      superstate.levelSuperstate.AddedActingTTC(targetLoc);
      
      attacker.nextActionTime = attacker.nextActionTime + attacker.CalculateCombatTimeCost(600);

      var sorcerous = attacker.components.GetOnlySorcerousUCOrNull();
      Asserts.Assert(sorcerous != null);
      sorcerous.mp = sorcerous.mp - BLAZE_COST;
      game.actionNum++;
    }

    public static void Explosion(
        Game game,
        Superstate superstate,
        Unit attacker,
        Location targetLoc) {
      Eventer.broadcastUnitExplosionEvent(game, attacker, targetLoc);

      var adjacentLocs = game.level.terrain.GetAdjacentExistingLocations(targetLoc, true);
      adjacentLocs.Add(targetLoc);
      var affectedLocs = new SortedSet<Location>();
      foreach (var adjacentLoc in adjacentLocs) {
        if (game.level.terrain.tiles[adjacentLoc].IsWalkable()) {
          affectedLocs.Add(adjacentLoc);
        }
      }
      foreach (var affectedLoc in affectedLocs) {
        game.level.terrain.tiles[affectedLoc].components.Add(
            game.root.EffectFireBombTTCCreate(Actions.EXPLOSION_DELAY).AsITerrainTileComponent());
        superstate.levelSuperstate.AddedActingTTC(affectedLoc);
      }

      attacker.nextActionTime = attacker.nextActionTime + attacker.CalculateCombatTimeCost(600);

      var sorcerous = attacker.components.GetOnlySorcerousUCOrNull();
      Asserts.Assert(sorcerous != null);
      sorcerous.mp = sorcerous.mp - EXPLOSION_COST;
      game.actionNum++;
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
      game.actionNum++;
    }

    public static void ExplodeFireBomb(
        Game game,
        Superstate superstate,
        Location location) {
      Unit poorSuckerOnThisTile = superstate.levelSuperstate.GetLiveUnitAt(location);
      Eventer.broadcastUnitFireBombedEvent(game, poorSuckerOnThisTile, location);
      if (poorSuckerOnThisTile.Exists()) {
        AttackedInner(game, superstate, poorSuckerOnThisTile, FIRE_BOMB_DAMAGE, false);
      }
      game.actionNum++;
    }
    
    public static void EffectBlaze(
        Game game,
        Superstate superstate,
        Location location) {
      Eventer.broadcastTileBurningEvent(game, location);
      Unit poorSuckerOnThisTile = superstate.levelSuperstate.GetLiveUnitAt(location);
      if (poorSuckerOnThisTile.Exists()) {
        Eventer.broadcastUnitBurningEvent(game, poorSuckerOnThisTile);
        AttackedInner(game, superstate, poorSuckerOnThisTile, BLAZE_DAMAGE, false);
        if (!poorSuckerOnThisTile.components.GetOnlyOnFireUCOrNull().Exists()) {
          poorSuckerOnThisTile.components.Add(
              game.root.EffectOnFireUCCreate(Actions.BLAZE_DURATION).AsIUnitComponent());
        }
      }
      game.actionNum++;
    }
    
    public static void EffectBurn(
        Game game,
        Superstate superstate,
        Location location) {
      Unit poorSuckerOnThisTile = superstate.levelSuperstate.GetLiveUnitAt(location);
      if (poorSuckerOnThisTile.Exists()) {
        Eventer.broadcastUnitBurningEvent(game, poorSuckerOnThisTile);
        AttackedInner(game, superstate, poorSuckerOnThisTile, BLAZE_DAMAGE, false);
        if (!poorSuckerOnThisTile.components.GetOnlyOnFireUCOrNull().Exists()) {
          poorSuckerOnThisTile.components.Add(
              game.root.EffectOnFireUCCreate(Actions.BLAZE_DURATION).AsIUnitComponent());
        }
      }
      game.actionNum++;
    }

    public static void EffectExplosion(
        Game game,
        Superstate superstate,
        Location location) {
      Eventer.broadcastTileExplodingEvent(game, location);
      Unit poorSuckerOnThisTile = superstate.levelSuperstate.GetLiveUnitAt(location);
      if (poorSuckerOnThisTile.Exists()) {
        Eventer.broadcastUnitExplosionedEvent(game, poorSuckerOnThisTile, location);
        AttackedInner(game, superstate, poorSuckerOnThisTile, EXPLOSION_DAMAGE, false);
      }
      game.actionNum++;
    }

    public static void LightningCharge(
        Game game,
        Superstate superstate,
        Unit unit) {
      unit.components.Add(game.root.EffectLightningChargedUCCreate().AsIUnitComponent());
      Eventer.broadcastUnitFireBombedEvent(game, unit, unit.location);
      AttackedInner(game, superstate, unit, LIGHTNING_CHARGE_DAMAGE, false);
      game.actionNum++;
    }
  }
}
