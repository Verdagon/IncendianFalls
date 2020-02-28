using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LevelIncarnation {
  public readonly int terrain;
  public readonly int units;
  public  int controller;
  public  int time;
  public LevelIncarnation(
      int terrain,
      int units,
      int controller,
      int time) {
    this.terrain = terrain;
    this.units = units;
    this.controller = controller;
    this.time = time;
  }
}

}
