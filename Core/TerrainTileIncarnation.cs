using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TerrainTileIncarnation {
  public  int elevation;
  public readonly bool walkable;
  public readonly string classId;
  public readonly int components;
  public TerrainTileIncarnation(
      int elevation,
      bool walkable,
      string classId,
      int components) {
    this.elevation = elevation;
    this.walkable = walkable;
    this.classId = classId;
    this.components = components;
  }
}

}
