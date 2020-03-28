using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class BlastRodExtensions {
    public static Atharia.Model.Void Destruct(this BlastRod obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
    public static ICloneableUC ClonifyAndReturnNewReal(this BlastRod armor, Root newRoot) {
      return newRoot.EffectBlastRodCreate().AsICloneableUC();
    }

    public static Void ReactToPickUp(this BlastRod self, Game game, Superstate superstate, Unit unit) {
      game.ShowComm("Got it!", "You've found the Fire Bomb Staff!\n\nUse 'F' and click on a location to use it, for " + Actions.FIRE_BOMB_COST + "mp.\n\nIt will explode after 2 more turns, for " + Actions.FIRE_BOMB_DAMAGE + " damage!");
      return new Atharia.Model.Void();
    }
  }
}
