using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class StaircaseTTCExtensions {
    public static Atharia.Model.Void Destruct(
        this StaircaseTTC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
  }
}
