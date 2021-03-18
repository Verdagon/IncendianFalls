using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class ChallengingUCExtensions {
    public static Atharia.Model.Void Destruct(
        this ChallengingUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
  }
}
