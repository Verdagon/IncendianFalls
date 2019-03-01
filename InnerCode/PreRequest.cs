using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atharia.Model;

namespace IncendianFalls {
  public class PreRequest {
    public static void Do(Game game) {
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
