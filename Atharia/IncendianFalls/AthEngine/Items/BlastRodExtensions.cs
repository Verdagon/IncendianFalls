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
      game.AddEvent(
        new ShowOverlayEvent(
          "You've found the Fire Bomb Staff!\n\nUse 'F' and click on a location to use it, for " + Actions.FIRE_BOMB_COST + "mp.\n\nIt will explode after 2 more turns, for " + Actions.FIRE_BOMB_DAMAGE + " damage!",
          "normal",
          "narrator",
          true,
          true,
          false,
          new ButtonImmList(new Button[] { new Button("Got it!", "") }))
        //new ShowOverlayEvent(
        //  70, new Color(16, 16, 16, 224), 300, 0, 0, "",
        //  "You've found the Fire Bomb Staff!\n\nUse 'F' and click on a location to use it, for " + Actions.FIRE_BOMB_COST + "mp.\n\nIt will explode after 2 more turns, for " + Actions.FIRE_BOMB_DAMAGE + " damage!",
        //  new Color(255, 255, 255, 255), 300, 600, 0, 0, true, true,
        //  new ButtonImmList(new Button[] { new Button("Got it!", new Color(32, 32, 32, 255), "") }))
        .AsIGameEvent());
      return new Atharia.Model.Void();
    }
  }
}
