using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class CliffLevelControllerExtensions {
    public static Atharia.Model.Void Destruct(this CliffLevelController self) {
      self.Delete();
      return new Atharia.Model.Void();
    }

    public static string GetName(this CliffLevelController obj) {
      return "Cliff" + obj.depth;
    }

    public static bool ConsiderCornersAdjacent(this CliffLevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this CliffLevelController obj,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        string triggerName) {
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this CliffLevelController obj,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      return new Atharia.Model.Void();
    }
  }
}
