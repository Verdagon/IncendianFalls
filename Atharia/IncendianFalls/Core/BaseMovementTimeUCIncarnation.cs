using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseMovementTimeUCIncarnation {
  public readonly int movementTimeAddConstant;
  public readonly int movementTimeMultiplierPercent;
  public BaseMovementTimeUCIncarnation(
      int movementTimeAddConstant,
      int movementTimeMultiplierPercent) {
    this.movementTimeAddConstant = movementTimeAddConstant;
    this.movementTimeMultiplierPercent = movementTimeMultiplierPercent;
  }
}

}
