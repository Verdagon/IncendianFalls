using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class KillDirectiveUCExtensions {
    public static Atharia.Model.Void Destruct(
        this KillDirectiveUC obj) {
      var thing = obj.pathToLastSeenLocation;
      obj.Delete();
      thing.Destruct();
      return new Atharia.Model.Void();
    }
  }
}
