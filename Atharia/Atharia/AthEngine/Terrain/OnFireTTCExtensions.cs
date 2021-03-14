using System;
using System.Collections.Generic;
using System.Text;

namespace Atharia.Model {
  public static class OnFireTTCExtensions {
    public static Atharia.Model.Void Destruct(this OnFireTTC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void Act(
        this OnFireTTC obj,
        Game game,
        Superstate superstate,
        Location containingTileLocation) {
      if (obj.turnsRemaining >= 0) {
        obj.turnsRemaining = obj.turnsRemaining - 1;
      } else {
        game.level.terrain.tiles[containingTileLocation].components.Remove(obj.AsITerrainTileComponent());
        obj.Destruct();
        superstate.levelSuperstate.RemovedActingTTC(containingTileLocation);
        IncendianFalls.Actions.ExplodeFireBomb(game, superstate, containingTileLocation);
      }

      return new Atharia.Model.Void();
    }
  }
}
