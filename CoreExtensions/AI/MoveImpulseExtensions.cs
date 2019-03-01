﻿using System;
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

    public static bool Enact(
        this MoveImpulse obj,
        Game game,
        LiveUnitByLocationMap liveUnitByLocationMap,
        Unit unit) {
      Actions.Step(game, liveUnitByLocationMap, unit, obj.stepLocation);
      return false;
    }
  }
}
