using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class UpStaircaseTerrainTileComponentExtensions {
    public static Atharia.Model.Void Destruct(
        this UpStaircaseTerrainTileComponent obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
  }
}
