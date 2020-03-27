using System;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class CounterImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this CounterImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetWeight(this CounterImpulse obj) {
      return obj.weight;
    }

    public static Atharia.Model.Void Enact(
        this CounterImpulse obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      Actions.Counter(game, unit);
      return new Atharia.Model.Void();
    }
  }
}
