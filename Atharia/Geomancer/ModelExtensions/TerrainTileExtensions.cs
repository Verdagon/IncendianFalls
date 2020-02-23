using System;
using System.Collections.Generic;

namespace Geomancer.Model {
  public static class TerrainTileExtensions {
    public static Geomancer.Model.Void Destruct(
        this TerrainTile obj) {
      var members = obj.members;
      obj.Delete();
      members.Destruct();
      return new Geomancer.Model.Void();
    }
  }
}
