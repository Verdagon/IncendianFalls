using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseSightRangeUCIncarnation {
  public readonly int sightRangeAddConstant;
  public readonly int sightRangeMultiplierPercent;
  public BaseSightRangeUCIncarnation(
      int sightRangeAddConstant,
      int sightRangeMultiplierPercent) {
    this.sightRangeAddConstant = sightRangeAddConstant;
    this.sightRangeMultiplierPercent = sightRangeMultiplierPercent;
  }
}

}
