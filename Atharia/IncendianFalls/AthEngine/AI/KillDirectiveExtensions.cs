using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class KillDirectiveExtensions {
    public static Atharia.Model.Void Destruct(
        this KillDirective obj) {
      var thing = obj.pathToLastSeenLocation;
      obj.Delete();
      thing.Destruct();
      return new Atharia.Model.Void();
    }
  }
}
