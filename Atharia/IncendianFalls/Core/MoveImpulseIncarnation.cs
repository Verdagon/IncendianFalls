using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MoveImpulseIncarnation {
  public readonly int weight;
  public readonly Location stepLocation;
  public MoveImpulseIncarnation(
      int weight,
      Location stepLocation) {
    this.weight = weight;
    this.stepLocation = stepLocation;
  }
}

}
