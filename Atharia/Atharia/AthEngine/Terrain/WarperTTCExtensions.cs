﻿using System;
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
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        Unit interactingUnit,
        Location containingTileLocation) {
      if (!IncendianFalls.Actions.CanTeleportTo(superstate.levelSuperstate, game.level.terrain, warper.destinationLocation, true)) {
        return "Can't teleport there!";
      }
      IncendianFalls.Actions.Teleport(game, superstate, interactingUnit, warper.destinationLocation);
      return "";
    }
  }
}
