using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseOffenseUCIncarnation {
  public readonly int outgoingDamageAddConstant;
  public readonly int outgoingDamageMultiplierPercent;
  public BaseOffenseUCIncarnation(
      int outgoingDamageAddConstant,
      int outgoingDamageMultiplierPercent) {
    this.outgoingDamageAddConstant = outgoingDamageAddConstant;
    this.outgoingDamageMultiplierPercent = outgoingDamageMultiplierPercent;
  }
}

}
