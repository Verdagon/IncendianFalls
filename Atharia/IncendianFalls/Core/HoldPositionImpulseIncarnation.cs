using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class HoldPositionImpulseIncarnation {
  public readonly int weight;
  public readonly int duration;
  public HoldPositionImpulseIncarnation(
      int weight,
      int duration) {
    this.weight = weight;
    this.duration = duration;
  }
}

}
