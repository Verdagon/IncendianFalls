using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class ItemTerrainTileComponentExtensions {
    public static Atharia.Model.Void Destruct(
        this ItemTerrainTileComponent obj) {
      var item = obj.item;
      obj.Delete();
      item.Delete();
      return new Atharia.Model.Void();
    }
  }
}
