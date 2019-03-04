using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class ItemTTCExtensions {
    public static Atharia.Model.Void Destruct(
        this ItemTTC obj) {
      var item = obj.item;
      obj.Delete();
      item.Delete();
      return new Atharia.Model.Void();
    }
  }
}
