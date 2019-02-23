using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class PursueImpulseIncarnation {
  public readonly int weight;
  public readonly Location stepLocation;
  public PursueImpulseIncarnation(
      int weight,
      Location stepLocation) {
    this.weight = weight;
    this.stepLocation = stepLocation;
  }
}

}
