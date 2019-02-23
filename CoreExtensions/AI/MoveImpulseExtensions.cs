using System;
using Atharia.Model;

namespace IncendianFalls {
  public static class MoveImpulseExtensions {
    public static int GetWeightImpl(this MoveImpulse obj) {
      return obj.weight;
    }

    public static Atharia.Model.Void EnactImpl(this MoveImpulse obj, Unit unit, Game game) {
      var liveUnitByLocationMap = new LiveUnitByLocationMap(game);
      Actions.Step(game, liveUnitByLocationMap, unit, obj.stepLocation);
      return new Atharia.Model.Void();
    }
  }
}
