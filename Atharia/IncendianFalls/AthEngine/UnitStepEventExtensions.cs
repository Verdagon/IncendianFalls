using System;

namespace Atharia.Model {
  public static class UnitStepEventExtensions {
    public static int GetTime(this Atharia.Model.UnitStepEvent e) {
      return e.time;
    }
  }
}
