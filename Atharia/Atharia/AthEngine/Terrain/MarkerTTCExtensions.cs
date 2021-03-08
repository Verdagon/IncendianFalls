using System;
using System.Collections.Generic;
using System.Text;

namespace Atharia.Model {
  public static class MarkerTTCExtensions {
    public static Atharia.Model.Void Destruct(this MarkerTTC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
  }
}
