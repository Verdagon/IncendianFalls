using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class PursueImpulseIncarnation {
  public readonly int weight;
  public readonly bool isClearPath;
  public PursueImpulseIncarnation(
      int weight,
      bool isClearPath) {
    this.weight = weight;
    this.isClearPath = isClearPath;
  }
}

}
