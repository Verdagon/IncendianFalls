using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class TerrainTileIncarnation {
  public int elevation;
  public bool walkable;
  public string classId;
  public TerrainTileIncarnation(
      int elevation,
      bool walkable,
      string classId) {
    this.elevation = elevation;
    this.walkable = walkable;
    this.classId = classId;
  }
}

}
