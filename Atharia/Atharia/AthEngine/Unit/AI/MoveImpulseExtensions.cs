using System;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class MoveImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this MoveImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetWeight(this MoveImpulse obj) {
      return obj.weight;
    }

    public static Atharia.Model.Void Enact(
        this MoveImpulse obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      Actions.Step(game, superstate, unit, obj.stepLocation, false, true);
      return new Atharia.Model.Void();
    }
  }
}
