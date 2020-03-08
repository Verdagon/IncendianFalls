using System;
using System.Collections.Generic;
using System.Text;

namespace Atharia.Model {
  public static class KamikazeTargetTTCExtensions {
    public static Atharia.Model.Void Destruct(this KamikazeTargetTTC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
  }
}
