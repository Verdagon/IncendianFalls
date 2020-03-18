using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class SlowRodExtensions {
    public static Atharia.Model.Void Destruct(this SlowRod obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static ICloneableUC ClonifyAndReturnNewReal(this SlowRod armor, Root newRoot) {
      return newRoot.EffectSlowRodCreate().AsICloneableUC();
    }

    public static Void ReactToPickUp(this SlowRod self, Game game, Superstate superstate, Unit unit) {
      game.AddEvent(
        new ShowOverlayEvent(
          "You've found the Mire Staff!\n\nUse 'S' and click on an enemy to use it.\n\nFor " + Actions.MIRE_COST + "mp, it will slightly delay their next action... but only a bit.\n\nBest have a time clone cast it!",
            "normal",
          "narrator",
          true,
          true,
          false,
          new ButtonImmList(new Button[] { new Button("Got it!", "") } ))
        .AsIGameEvent());
      return new Atharia.Model.Void();
    }
  }
}
