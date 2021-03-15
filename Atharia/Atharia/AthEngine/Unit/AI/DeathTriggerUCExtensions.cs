using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class DeathTriggerUCExtensions {
    public static Atharia.Model.Void Destruct(
        this DeathTriggerUC self) {
      self.Delete();
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void BeforeDeath(
        DeathTriggerUC self,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        Unit unit) {
      var triggerName = self.triggerName;
      self.root.logger.Info("In beforedeath for " + triggerName);
      game.level.controller.SimpleTrigger(context, game, superstate, triggerName);
      return new Atharia.Model.Void();
    }
  }
}
