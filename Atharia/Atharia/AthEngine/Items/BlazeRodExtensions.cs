using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class BlazeRodExtensions {
    public static Atharia.Model.Void Destruct(this BlazeRod obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static ICloneableUC ClonifyAndReturnNewReal(this BlazeRod armor, Root newRoot) {
      return newRoot.EffectBlazeRodCreate().AsICloneableUC();
    }

    public static Void ReactToPickUp(this BlazeRod self, Game game, Superstate superstate, Unit unit) {
      game.ShowComm("Got it!", "You've found the Blaze Rod!\n\nHit 'B' and click on a location to use it, for " + Actions.BLAZE_COST + "mp.\n\nIt will do " + Actions.BLAZE_DAMAGE + " damage there for " + Actions.BLAZE_DURATION + " turns!");
      return new Atharia.Model.Void();
    }
  }
}
