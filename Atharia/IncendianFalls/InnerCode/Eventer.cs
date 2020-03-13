﻿using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class Eventer {
    public static void broadcastUnitUnleashBideEvent(
        Root root,
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
      attacker.AddEvent(game, unleashBideEvent.AsIUnitEvent());
      foreach (var victim in victims) {
        victim.AddEvent(game, unleashBideEvent.AsIUnitEvent());
      }
      foreach (var location in otherLocations) {
        game.level.terrain.tiles[location].AddEvent(game, unleashBideEvent.AsITerrainTileEvent());
      }
    }
    public static void broadcastUnitAttackEvent(
        Root root,
        Game game,
        Unit attacker,
        Unit victim) {
      var attackEvent = new UnitAttackEventAsIUnitEvent(new UnitAttackEvent(game.time, attacker.id, victim.id));
      attacker.AddEvent(game, attackEvent);
      if (victim.id != attacker.id) {
        victim.AddEvent(game, attackEvent);
      }
    }
    public static void broadcastUnitFireEvent(
        Root root,
        Game game,
        Unit attacker,
        Unit victim) {
      var attackEvent = new UnitFireEventAsIUnitEvent(new UnitFireEvent(game.time, attacker.id, victim.id));
      attacker.AddEvent(game, attackEvent);
      if (victim.id != attacker.id) {
        victim.AddEvent(game, attackEvent);
      }
    }
    public static void broadcastUnitFireBombedEvent(
        Root root,
        Game game,
        Unit victim) {
      var e = new UnitFireBombedEvent(game.time, victim.id).AsIUnitEvent();
      victim.AddEvent(game, e);
    }
    public static void broadcastUnitStepEvent(
        Root root,
        Game game,
        Unit unit,
        Location from,
        Location to) {
      var attackEvent = new UnitStepEventAsIUnitEvent(new UnitStepEvent(game.time, unit.id, from, to));
      unit.AddEvent(game, attackEvent);
    }
    public static void broadcastUnitDefyingEvent(
        Root root,
        Game game,
        Unit unit) {
      var e = new UnitDefyingEvent(game.time);
      unit.AddEvent(game, e.AsIUnitEvent());
    }
    public static void broadcastUnitMiredEvent(
        Root root,
        Game game,
        Unit attacker,
        Unit victim) {
      var e = new UnitMireEvent(game.time, attacker.id, victim.id);
      attacker.AddEvent(game, e.AsIUnitEvent());
      if (victim.id != attacker.id) {
        victim.AddEvent(game, e.AsIUnitEvent());
      }
    }
    public static void broadcastUnitCounteringEvent(
        Root root,
        Game game,
        Unit unit) {
      var e = new UnitCounteringEvent(game.time);
      unit.AddEvent(game, e.AsIUnitEvent());
    }
  }
}
