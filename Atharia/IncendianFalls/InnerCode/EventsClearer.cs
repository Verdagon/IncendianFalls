using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atharia.Model;

namespace IncendianFalls {
  public class EventsClearer {
    public static void Clear(Game game) {
      game.events.Clear();
      // If we can have an index on which units have events, that'd be pretty cool.
      // Not sure how to generalize that though...
      foreach (var unitMaybeWithEvents in game.level.units) {
        if (unitMaybeWithEvents.events.Count > 0) {
          unitMaybeWithEvents.events.Clear();
        }
      }
    }
  }
}
