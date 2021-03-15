using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class ExplosionRodExtensions {
    public static Atharia.Model.Void Destruct(this ExplosionRod obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static ICloneableUC ClonifyAndReturnNewReal(this ExplosionRod armor, Root newRoot) {
      return newRoot.EffectExplosionRodCreate().AsICloneableUC();
    }

    public static Void ReactToPickUp(this ExplosionRod self, Game game, Superstate superstate, Unit unit) {
      game.ShowComm("Got it!", "You've found the Explosion Staff!\n\nHit 'X' and click on a location to use it, for " + Actions.EXPLOSION_COST + "mp.\n\nIt will explode after " + Actions.EXPLOSION_DELAY + " more turns, for " + Actions.EXPLOSION_DAMAGE + " damage!");
      return new Atharia.Model.Void();
    }
  }
}
