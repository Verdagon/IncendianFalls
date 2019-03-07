using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class AttackImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this AttackImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetWeight(this AttackImpulse obj) {
      return obj.weight;
    }

    public static bool Enact(
        this AttackImpulse obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      Actions.Bump(game, superstate, unit, obj.targetUnit, 1.0f);
      return true;
    }

  }
}
