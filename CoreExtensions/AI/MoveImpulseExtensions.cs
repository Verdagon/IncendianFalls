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

    public static Atharia.Model.Void Enact(this MoveImpulse obj, Unit unit, Game game) {
      var liveUnitByLocationMap = new LiveUnitByLocationMap(game);
      Actions.Step(game, liveUnitByLocationMap, unit, obj.stepLocation);
      return new Atharia.Model.Void();
    }
  }
}
