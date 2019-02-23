using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TerrainIncarnation {
  public  Pattern pattern;
  public readonly float elevationStepHeight;
  public readonly int tiles;
  public TerrainIncarnation(
      Pattern pattern,
      float elevationStepHeight,
      int tiles) {
    this.pattern = pattern;
    this.elevationStepHeight = elevationStepHeight;
    this.tiles = tiles;
  }
}

}
