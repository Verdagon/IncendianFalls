using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class DownStaircaseTerrainTileComponentExtensions {
    public static Atharia.Model.Void Destruct(
        this DownStaircaseTerrainTileComponent obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
  }
}
