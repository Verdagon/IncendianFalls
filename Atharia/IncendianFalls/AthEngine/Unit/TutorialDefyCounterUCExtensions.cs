using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class TutorialDefyCounterUCExtensions {
    public static Atharia.Model.Void Destruct(
        this TutorialDefyCounterUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void AfterImpulse(
        this TutorialDefyCounterUC self,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        Unit unit,
        IAICapabilityUC originatingCapability,
        IImpulse impulse) {
      var bunch = IImpulseStrongMutBunch.New(game.root);
      bunch.Add(impulse);
      var defyImpulse = bunch.GetOnlyDefyImpulseOrNull();
      bunch.Destruct();

      self.numDefiesRemaining = self.numDefiesRemaining - 1;

      //game.root.logger.Error("In afterimpulse! " + defyImpulse + " " + self.numDefiesRemaining + " remaining");

      if (defyImpulse.Exists()) {
        game.level.controller.SimpleTrigger(context, game, superstate, self.onChangeTriggerName);
      }

      return new Atharia.Model.Void();
    }
  }
}
