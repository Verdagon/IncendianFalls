using System;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class MireImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this MireImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetWeight(this MireImpulse obj) {
      return obj.weight;
    }

    public static Atharia.Model.Void Enact(
        this MireImpulse obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      Actions.Mire(game, superstate, unit, obj.targetUnit);
      return new Atharia.Model.Void();
    }
  }
}
