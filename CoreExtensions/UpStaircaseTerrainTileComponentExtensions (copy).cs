using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class UpStaircaseTTCExtensions {
    public static Atharia.Model.Void Destruct(
        this UpStaircaseTTC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
  }
}
