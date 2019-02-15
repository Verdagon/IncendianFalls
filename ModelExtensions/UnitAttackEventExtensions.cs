using System;

namespace IncendianFalls {
  public static class UnitAttackEventExtensions {
    public static int GetTime(this Atharia.Model.UnitAttackEvent e) {
      return e.time;
    }
  }
}
