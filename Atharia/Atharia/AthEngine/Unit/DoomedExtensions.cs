using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class DoomedUCExtensions {
    public static Atharia.Model.Void Destruct(
        this DoomedUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static Void PreAct(
        this Atharia.Model.DoomedUC self,
        Game game,
        Superstate superstate,
        Unit unit) {
      if (game.time >= self.deathTime) {
        unit.lifeEndTime = game.time;
        // Bump the victim up to be the next acting unit.
        unit.nextActionTime = game.time;
        superstate.levelSuperstate.RemoveUnit(unit);
      }
      return new Atharia.Model.Void();
    }
  }
}
