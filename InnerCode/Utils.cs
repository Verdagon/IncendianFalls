using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class Utils {
    private class LowerNextActionTimeComparer : IComparer<Unit> {
      public int Compare(Unit a, Unit b) {
        var tickDiff = Math.Sign(a.nextActionTime - b.nextActionTime);
        if (tickDiff != 0)
          return tickDiff;
        return Math.Sign(a.id - b.id);
      }
    }

    // Returns false if it couldn't figure out what the player should do and instead wants
    // direct input.
    public static Unit GetNextActingUnit(Game game) {
      if (game.level.units.Count == 0) {
        // Caller should check before calling us.
        throw new Exception("No units!");
      }
      var unitsOrderedByNextActionTime = new SortedDictionary<Unit, object>(new LowerNextActionTimeComparer());
      foreach (var u in game.level.units) {
        unitsOrderedByNextActionTime.Add(u, new object());
      }
      return DictionaryUtils.GetFirstKey(unitsOrderedByNextActionTime);
    }
  }
}
