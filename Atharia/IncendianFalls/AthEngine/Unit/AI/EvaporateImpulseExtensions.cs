using System;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class EvaporateImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this EvaporateImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetWeight(this EvaporateImpulse obj) {
      return 1000;
    }

    public static Atharia.Model.Void Enact(
        this EvaporateImpulse obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      Actions.Evaporate(game, superstate, unit);
      return new Atharia.Model.Void();
    }
  }
}
