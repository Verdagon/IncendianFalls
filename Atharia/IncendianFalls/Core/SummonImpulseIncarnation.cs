using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SummonImpulseIncarnation {
  public readonly int weight;
  public readonly string blueprintName;
  public readonly Location location;
  public SummonImpulseIncarnation(
      int weight,
      string blueprintName,
      Location location) {
    this.weight = weight;
    this.blueprintName = blueprintName;
    this.location = location;
  }
}

}
