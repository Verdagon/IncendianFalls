using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseOffenseUCIncarnation : IBaseOffenseUCEffectVisitor {
  public readonly int outgoingDamageAddConstant;
  public readonly int outgoingDamageMultiplierPercent;
  public BaseOffenseUCIncarnation(
      int outgoingDamageAddConstant,
      int outgoingDamageMultiplierPercent) {
    this.outgoingDamageAddConstant = outgoingDamageAddConstant;
    this.outgoingDamageMultiplierPercent = outgoingDamageMultiplierPercent;
  }
  public BaseOffenseUCIncarnation Copy() {
    return new BaseOffenseUCIncarnation(
outgoingDamageAddConstant,
outgoingDamageMultiplierPercent    );
  }

  public void visitBaseOffenseUCCreateEffect(BaseOffenseUCCreateEffect e) {}
  public void visitBaseOffenseUCDeleteEffect(BaseOffenseUCDeleteEffect e) {}


  public void ApplyEffect(IBaseOffenseUCEffect effect) { effect.visitIBaseOffenseUCEffect(this); }
}

}
