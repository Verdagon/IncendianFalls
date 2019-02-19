using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class TerrainIncarnation {
  public Pattern pattern;
  public float elevationStepHeight;
  public int tiles;
  public TerrainIncarnation(
      Pattern pattern,
      float elevationStepHeight,
      int tiles) {
    this.pattern = pattern;
    this.elevationStepHeight = elevationStepHeight;
    this.tiles = tiles;
  }
  public int GetDeterministicHashCode() {
    int s = 0;
    s = s * 37 + pattern.GetDeterministicHashCode();
    s = s * 37 + elevationStepHeight.GetDeterministicHashCode();
    s = s * 37 + tiles.GetDeterministicHashCode();
    return s;
  }
}

}
