using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class MoveDirectiveUCExtensions {
    public static Atharia.Model.Void Destruct(
        this MoveDirectiveUC obj) {
      var thing = obj.path;
      obj.Delete();
      thing.Destruct();
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void AfterImpulse(
        this MoveDirectiveUC obj,
        Game game,
        Superstate superstate,
        Unit unit,
        IImpulse iimpulse) {
      if (iimpulse is MoveImpulseAsIImpulse miI) {
        var impulse = miI.obj;
        Asserts.Assert(obj.path[0] == impulse.stepLocation);
        obj.path.RemoveAt(0);

        if (obj.path.Count == 0) {
          unit.ClearDirective();
        }
      } else {
        Asserts.Assert(false);
      }
      return new Atharia.Model.Void();
    }
  }
}
