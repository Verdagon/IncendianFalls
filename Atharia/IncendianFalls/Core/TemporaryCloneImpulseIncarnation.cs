using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TemporaryCloneImpulseIncarnation {
  public readonly int weight;
  public readonly string blueprintName;
  public readonly Location location;
  public readonly int hp;
  public TemporaryCloneImpulseIncarnation(
      int weight,
      string blueprintName,
      Location location,
      int hp) {
    this.weight = weight;
    this.blueprintName = blueprintName;
    this.location = location;
    this.hp = hp;
  }
}

}
