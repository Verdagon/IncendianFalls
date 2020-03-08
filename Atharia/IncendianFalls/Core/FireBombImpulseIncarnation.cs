using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireBombImpulseIncarnation {
  public readonly int weight;
  public readonly Location location;
  public FireBombImpulseIncarnation(
      int weight,
      Location location) {
    this.weight = weight;
    this.location = location;
  }
}

}
