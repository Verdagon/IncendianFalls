using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SummonAICapabilityUCIncarnation {
  public readonly string blueprintName;
  public  int charges;
  public SummonAICapabilityUCIncarnation(
      string blueprintName,
      int charges) {
    this.blueprintName = blueprintName;
    this.charges = charges;
  }
}

}
