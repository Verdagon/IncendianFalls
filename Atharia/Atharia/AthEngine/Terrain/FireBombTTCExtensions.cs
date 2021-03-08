using System;
using System.Collections.Generic;
using System.Text;

namespace Atharia.Model {
  public static class FireBombTTCExtensions {
    public static Atharia.Model.Void Destruct(this FireBombTTC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void Act(
        this FireBombTTC obj,
        Game game,
        Superstate superstate,
        Location containingTileLocation) {
      if (obj.turnsUntilExplosion > 0) {
        obj.turnsUntilExplosion = obj.turnsUntilExplosion - 1;
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
