using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LevelIncarnation {
  public readonly Vec3 cameraAngle;
  public readonly int terrain;
  public readonly int units;
  public  int controller;
  public  int time;
  public LevelIncarnation(
      Vec3 cameraAngle,
      int terrain,
      int units,
      int controller,
      int time) {
    this.cameraAngle = cameraAngle;
    this.terrain = terrain;
    this.units = units;
    this.controller = controller;
    this.time = time;
  }
}

}
