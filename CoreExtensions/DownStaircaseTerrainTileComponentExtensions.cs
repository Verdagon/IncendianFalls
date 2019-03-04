using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class DownStaircaseTTCExtensions {
    public static Atharia.Model.Void Destruct(
        this DownStaircaseTTC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
  }
}
