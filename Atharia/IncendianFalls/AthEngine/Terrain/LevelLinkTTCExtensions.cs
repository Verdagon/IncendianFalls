using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class LevelLinkTTCExtensions {
    public static Atharia.Model.Void Destruct(this LevelLinkTTC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static string Interact(
        this LevelLinkTTC levelLink,
        Game game,
        Superstate superstate,
        Unit interactingUnit,
        Location containingTileLocation) {

      game.level.ExitUnit(game, superstate.levelSuperstate, interactingUnit);

      game.level = levelLink.destinationLevel;
      superstate.levelSuperstate = new LevelSuperstate(game.level);

      // These will likely be in the distant past, since it's been a while since we've
      // visited here. We'll want to bump them all up to the near future.
      Asserts.Assert(game.time >= game.level.time);

      // Add that amount to every unit, so it's as if we just left this level.
      foreach (var nativeUnit in game.level.units) {
        nativeUnit.nextActionTime =
            nativeUnit.nextActionTime + (game.time - game.level.time);
      }

      game.level.EnterUnit(game, superstate.levelSuperstate, interactingUnit, levelLink.destinationLevelLocation);

      return "";
    }
  }
}
