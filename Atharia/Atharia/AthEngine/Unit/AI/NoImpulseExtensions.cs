﻿using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class NoImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this NoImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetWeight(this NoImpulse obj) {
      return 0;
    }

    public static Atharia.Model.Void Enact(
        this NoImpulse obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      unit.nextActionTime = unit.nextActionTime + unit.CalculateCombatTimeCost(300);
      return new Atharia.Model.Void();
    }
  }
}
