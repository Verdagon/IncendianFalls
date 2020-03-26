using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseSightRangeUCIncarnation : IBaseSightRangeUCEffectVisitor {
  public readonly int sightRangeAddConstant;
  public readonly int sightRangeMultiplierPercent;
  public BaseSightRangeUCIncarnation(
      int sightRangeAddConstant,
      int sightRangeMultiplierPercent) {
    this.sightRangeAddConstant = sightRangeAddConstant;
    this.sightRangeMultiplierPercent = sightRangeMultiplierPercent;
  }
  public BaseSightRangeUCIncarnation Copy() {
    return new BaseSightRangeUCIncarnation(
sightRangeAddConstant,
sightRangeMultiplierPercent    );
  }

  public void visitBaseSightRangeUCCreateEffect(BaseSightRangeUCCreateEffect e) {}
  public void visitBaseSightRangeUCDeleteEffect(BaseSightRangeUCDeleteEffect e) {}


  public void ApplyEffect(IBaseSightRangeUCEffect effect) { effect.visitIBaseSightRangeUCEffect(this); }
}

}
