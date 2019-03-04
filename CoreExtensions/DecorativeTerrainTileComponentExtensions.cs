using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class DecorativeTTCExtensions {
    public static Atharia.Model.Void Destruct(
        this DecorativeTTC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
  }
}
