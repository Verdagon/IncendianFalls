using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseDefenseUCIncarnation : IBaseDefenseUCEffectVisitor {
  public readonly int incomingDamageAddConstant;
  public readonly int incomingDamageMultiplierPercent;
  public BaseDefenseUCIncarnation(
      int incomingDamageAddConstant,
      int incomingDamageMultiplierPercent) {
    this.incomingDamageAddConstant = incomingDamageAddConstant;
    this.incomingDamageMultiplierPercent = incomingDamageMultiplierPercent;
  }
  public BaseDefenseUCIncarnation Copy() {
    return new BaseDefenseUCIncarnation(
incomingDamageAddConstant,
incomingDamageMultiplierPercent    );
  }

  public void visitBaseDefenseUCCreateEffect(BaseDefenseUCCreateEffect e) {}
  public void visitBaseDefenseUCDeleteEffect(BaseDefenseUCDeleteEffect e) {}


  public void ApplyEffect(IBaseDefenseUCEffect effect) { effect.visitIBaseDefenseUCEffect(this); }
}

}
