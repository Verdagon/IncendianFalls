using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class KillDirectiveUCExtensions {
    public static Atharia.Model.Void Destruct(
        this KillDirectiveUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
  }
}
