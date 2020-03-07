using System;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class DefyImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this DefyImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetWeight(this DefyImpulse obj) {
      return obj.weight;
    }

    public static bool Enact(
        this DefyImpulse obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      Actions.Defy(game, unit);
      return false;
    }
  }
}
