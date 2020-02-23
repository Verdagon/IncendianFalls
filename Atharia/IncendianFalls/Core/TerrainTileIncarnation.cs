using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TerrainTileIncarnation {
  public  int elevation;
  public readonly int components;
  public TerrainTileIncarnation(
      int elevation,
      int components) {
    this.elevation = elevation;
    this.components = components;
  }
}

}
