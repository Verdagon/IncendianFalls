using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class TimeAnchorTTCExtensions {
    public static Atharia.Model.Void Destruct(
        this TimeAnchorTTC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
  }
}
