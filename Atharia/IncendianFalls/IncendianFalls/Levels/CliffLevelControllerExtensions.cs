using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class CliffLevelControllerExtensions {
    public static string GetName(this CliffLevelController obj) {
      return "Cliff" + obj.depth;
    }

    public static bool ConsiderCornersAdjacent(this CliffLevelController obj) {
      return false;
    }
  }
}
