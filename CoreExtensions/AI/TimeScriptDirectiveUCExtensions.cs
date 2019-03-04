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
  }
}
