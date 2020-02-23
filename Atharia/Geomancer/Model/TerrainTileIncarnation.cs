using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {
public class TerrainTileIncarnation {
  public  int elevation;
  public readonly int members;
  public TerrainTileIncarnation(
      int elevation,
      int members) {
    this.elevation = elevation;
    this.members = members;
  }
}

}
