using System;
using System.Collections.Generic;
using System.Text;

namespace Atharia.Model {
  public static class SimplePresenceTriggerTTCExtensions {
    public static Atharia.Model.Void Destruct(this SimplePresenceTriggerTTC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void Trigger(
        this SimplePresenceTriggerTTC walkTrigger,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location) {
      game.level.controller.SimpleUnitTrigger(context, game, superstate, triggeringUnit, location, walkTrigger.name);
      return new Atharia.Model.Void();
    }
  }
}
