using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TerrainTileIncarnation {
  public readonly int events;
  public  int elevation;
  public readonly int components;
  public TerrainTileIncarnation(
      int events,
      int elevation,
      int components) {
    this.events = events;
    this.elevation = elevation;
    this.components = components;
  }
}

}
