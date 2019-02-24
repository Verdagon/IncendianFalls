using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public static class DecorativeTerrainTileComponentExtensions {
    public static Atharia.Model.Void Destruct(
        this DecorativeTerrainTileComponent obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
  }
}
