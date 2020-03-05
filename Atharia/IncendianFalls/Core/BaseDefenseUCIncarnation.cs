using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseDefenseUCIncarnation {
  public readonly int incomingDamageAddConstant;
  public readonly int incomingDamageMultiplierPercent;
  public BaseDefenseUCIncarnation(
      int incomingDamageAddConstant,
      int incomingDamageMultiplierPercent) {
    this.incomingDamageAddConstant = incomingDamageAddConstant;
    this.incomingDamageMultiplierPercent = incomingDamageMultiplierPercent;
  }
}

}
