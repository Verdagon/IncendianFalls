using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class Eventer {
    public static void broadcastUnitUnleashBideEvent(
        Game game,
        Unit attacker,
        List<Unit> victims,
        List<Location> otherLocations) {
      List<int> victimsIds = new List<int>();
      foreach (var victim in victims) {
        victimsIds.Add(victim.id);
      }
      var unleashBideEvent =
          new UnitUnleashBideEvent(
              game.time,
              attacker.id,
              new IntImmList(victimsIds),
              new LocationImmList(otherLocations));
      attacker.AddEvent(unleashBideEvent.AsIUnitEvent());
      foreach (var victim in victims) {
        victim.AddEvent(unleashBideEvent.AsIUnitEvent());
      }
      foreach (var location in otherLocations) {
        game.level.terrain.tiles[location].AddEvent(unleashBideEvent.AsITerrainTileEvent());
      }
    }
    public static void broadcastUnitAttackEvent(
        Game game,
        Unit attacker,
        Unit victim) {
      var attackEvent = new UnitAttackEventAsIUnitEvent(new UnitAttackEvent(game.time, attacker.id, victim.id));
      attacker.AddEvent(attackEvent);
      if (victim.id != attacker.id) {
        victim.AddEvent(attackEvent);
      }
    }
    public static void broadcastUnitFireEvent(
        Game game,
        Unit attacker,
        Unit victim) {
      var attackEvent = new UnitFireEventAsIUnitEvent(new UnitFireEvent(game.time, attacker.id, victim.id));
      attacker.AddEvent(attackEvent);
      if (victim.id != attacker.id) {
        victim.AddEvent(attackEvent);
      }
    }
    public static void broadcastUnitBlazeEvent(
        Game game,
        Unit attacker,
        Location targetLoc) {
      var attackEvent = new UnitBlazeEventAsIUnitEvent(new UnitBlazeEvent(game.time, attacker.id, targetLoc));
      attacker.AddEvent(attackEvent);
    }
    public static void broadcastUnitExplosionEvent(
        Game game,
        Unit attacker,
        Location targetLoc) {
      var attackEvent = new UnitExplosionEventAsIUnitEvent(new UnitExplosionEvent(game.time, attacker.id, targetLoc));
      attacker.AddEvent(attackEvent);
    }
    public static void broadcastUnitFireBombedEvent(
        Game game,
        Unit victim,
        Location location) {
      var e = new UnitFireBombedEvent(game.time, victim.id, location);
      victim.AddEvent(e.AsIUnitEvent());
    }
    public static void broadcastTileExplodingEvent(
        Game game,
        Location location) {
      var e = new TileExplodingEvent(game.time, location);
      game.level.terrain.tiles[location].AddEvent(e.AsITerrainTileEvent());
    }
    public static void broadcastTileBurningEvent(
        Game game,
        Location location) {
      var e = new TileBurningEvent(game.time, location);
      game.level.terrain.tiles[location].AddEvent(e.AsITerrainTileEvent());
    }
    public static void broadcastUnitBurningEvent(
        Game game,
        Unit victim) {
      var e = new UnitBurningEvent(game.time, victim.id);
      victim.AddEvent(e.AsIUnitEvent());
    }
    public static void broadcastUnitExplosionedEvent(
        Game game,
        Unit victim,
        Location location) {
      var e = new UnitExplosionedEvent(game.time, victim.id, location);
      victim.AddEvent(e.AsIUnitEvent());
    }
    public static void broadcastUnitStepEvent(
        Game game,
        Unit unit,
        Location from,
        Location to) {
      var attackEvent = new UnitStepEventAsIUnitEvent(new UnitStepEvent(game.time, unit.id, from, to));
      unit.AddEvent(attackEvent);
    }
    public static void broadcastUnitDefyingEvent(
        Game game,
        Unit unit) {
      var e = new UnitDefyingEvent(game.time);
      unit.AddEvent(e.AsIUnitEvent());
    }
    public static void broadcastUnitMiredEvent(
        Game game,
        Unit attacker,
        Unit victim) {
      var e = new UnitMireEvent(game.time, attacker.id, victim.id);
      attacker.AddEvent(e.AsIUnitEvent());
      if (victim.id != attacker.id) {
        victim.AddEvent(e.AsIUnitEvent());
      }
    }
    public static void broadcastUnitCounteringEvent(
        Game game,
        Unit unit) {
      var e = new UnitCounteringEvent(game.time);
      unit.AddEvent(e.AsIUnitEvent());
    }
  }
}
