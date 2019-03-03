using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LevelIncarnation {
  public readonly string name;
  public readonly bool considerCornersAdjacent;
  public readonly int terrain;
  public readonly int units;
  public LevelIncarnation(
      string name,
      bool considerCornersAdjacent,
      int terrain,
      int units) {
    this.name = name;
    this.considerCornersAdjacent = considerCornersAdjacent;
    this.terrain = terrain;
    this.units = units;
  }
}

}
