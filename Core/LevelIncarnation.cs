using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class LevelIncarnation {
  public string name;
  public bool considerCornersAdjacent;
  public int terrain;
  public int units;
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
  public int GetDeterministicHashCode() {
    int s = 0;
    s = s * 37 + name.GetDeterministicHashCode();
    s = s * 37 + considerCornersAdjacent.GetDeterministicHashCode();
    s = s * 37 + terrain.GetDeterministicHashCode();
    s = s * 37 + units.GetDeterministicHashCode();
    return s;
  }
}

}
