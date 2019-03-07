using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireImpulseIncarnation {
  public readonly int weight;
  public readonly int targetUnit;
  public FireImpulseIncarnation(
      int weight,
      int targetUnit) {
    this.weight = weight;
    this.targetUnit = targetUnit;
  }
}

}
