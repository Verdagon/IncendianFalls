using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class AttackImpulseIncarnation {
  public readonly int weight;
  public readonly int targetUnit;
  public AttackImpulseIncarnation(
      int weight,
      int targetUnit) {
    this.weight = weight;
    this.targetUnit = targetUnit;
  }
}

}
