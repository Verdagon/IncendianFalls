using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LevelIncarnation {
  public readonly int terrain;
  public readonly int units;
  public readonly int depth;
  public  int controller;
  public LevelIncarnation(
      int terrain,
      int units,
      int depth,
      int controller) {
    this.terrain = terrain;
    this.units = units;
    this.depth = depth;
    this.controller = controller;
  }
}

}
