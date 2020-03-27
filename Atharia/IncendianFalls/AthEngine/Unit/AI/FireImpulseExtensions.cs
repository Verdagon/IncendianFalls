using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class FireImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this FireImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetWeight(this FireImpulse obj) {
      return obj.weight;
    }

    public static Atharia.Model.Void Enact(
        this FireImpulse obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      Actions.Fire(game, superstate, unit, obj.targetUnit);
      return new Atharia.Model.Void();
    }

  }
}
