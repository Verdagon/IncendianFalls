using System;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class DefendImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this DefendImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetWeight(this DefendImpulse obj) {
      return obj.weight;
    }

    public static bool Enact(
        this DefendImpulse obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      Actions.Defend(game, unit);
      return false;
    }
  }
}
