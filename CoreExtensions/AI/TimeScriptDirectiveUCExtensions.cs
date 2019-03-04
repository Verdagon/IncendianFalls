using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class TimeScriptDirectiveUCExtensions {
    public static Atharia.Model.Void Destruct(
        this TimeScriptDirectiveUC obj) {
      var thing = obj.script;
      obj.Delete();
      thing.Destruct();
      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void AfterImpulse(
        this TimeScriptDirectiveUC obj,
        Game game,
        Superstate superstate,
        Unit unit,
        IImpulse impulse) {
      if (impulse is EvaporateImpulseAsIImpulse eiI) {
        // Funny, we'll never get here, because the unit dies before AfterImpulse is called.
      } else {
        Asserts.Assert(obj.script.Count > 0);
        obj.script.RemoveAt(0);
      }
      return new Atharia.Model.Void();
    }
  }
}
