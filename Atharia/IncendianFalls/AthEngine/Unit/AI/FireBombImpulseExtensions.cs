using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class FireBombImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this FireBombImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetWeight(this FireBombImpulse obj) {
      return obj.weight;
    }

    public static Atharia.Model.Void Enact(
        this FireBombImpulse obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      Actions.PlaceFireBomb(game, superstate, unit, obj.location);
      return new Atharia.Model.Void();
    }
  }
}
