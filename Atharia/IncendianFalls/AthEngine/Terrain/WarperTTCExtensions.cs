using System;
using System.Collections.Generic;
using System.Text;

namespace Atharia.Model {
  public static class WarperTTCExtensions {
    public static Atharia.Model.Void Destruct(this WarperTTC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static string Interact(
        this WarperTTC warper,
        Game game,
        Superstate superstate,
        Unit interactingUnit,
        Location containingTileLocation) {
      if (!IncendianFalls.Actions.CanTeleportTo(game, superstate, warper.destinationLocation)) {
        return "Can't teleport there!";
      }
      IncendianFalls.Actions.Step(game, superstate, interactingUnit, warper.destinationLocation, true);
      return "";
    }
  }
}
