using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class Eventer {
    public static void broadcastUnitAttackEvent(
        Root root,
        Game game,
        Unit attacker,
        Unit victim) {
      var attackEvent = new UnitAttackEventAsIUnitEvent(new UnitAttackEvent(game.time, attacker.id, victim.id));
      attacker.events.Add(attackEvent);
      if (victim.id != attacker.id) {
        victim.events.Add(attackEvent);
      }
    }
    public static void broadcastUnitStepEvent(
        Root root,
        Game game,
        Unit unit,
        Location from,
        Location to) {
      var attackEvent = new UnitStepEventAsIUnitEvent(new UnitStepEvent(game.time, unit.id, from, to));
      unit.events.Add(attackEvent);
    }
  }
}
