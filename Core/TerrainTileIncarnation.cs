using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class TerrainTileIncarnation {
  public int elevation;
  public bool walkable;
  public string classId;
  public int features;
  public TerrainTileIncarnation(
      int elevation,
      bool walkable,
      string classId,
      int features) {
    this.elevation = elevation;
    this.walkable = walkable;
    this.classId = classId;
    this.features = features;
  }
  public int GetDeterministicHashCode() {
    int s = 0;
    s = s * 37 + elevation.GetDeterministicHashCode();
    s = s * 37 + walkable.GetDeterministicHashCode();
    s = s * 37 + classId.GetDeterministicHashCode();
    s = s * 37 + features.GetDeterministicHashCode();
    return s;
  }
}

}
