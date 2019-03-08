using System;

namespace Atharia.Model {
  public static class UnitCounteringEventExtensions {
    public static int GetTime(this Atharia.Model.UnitCounteringEvent e) {
      return e.time;
    }
  }
}
