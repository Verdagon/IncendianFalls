using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseMovementTimeUCIncarnation : IBaseMovementTimeUCEffectVisitor {
  public readonly int movementTimeAddConstant;
  public readonly int movementTimeMultiplierPercent;
  public BaseMovementTimeUCIncarnation(
      int movementTimeAddConstant,
      int movementTimeMultiplierPercent) {
    this.movementTimeAddConstant = movementTimeAddConstant;
    this.movementTimeMultiplierPercent = movementTimeMultiplierPercent;
  }
  public BaseMovementTimeUCIncarnation Copy() {
    return new BaseMovementTimeUCIncarnation(
movementTimeAddConstant,
movementTimeMultiplierPercent    );
  }

  public void visitBaseMovementTimeUCCreateEffect(BaseMovementTimeUCCreateEffect e) {}
  public void visitBaseMovementTimeUCDeleteEffect(BaseMovementTimeUCDeleteEffect e) {}


  public void ApplyEffect(IBaseMovementTimeUCEffect effect) { effect.visitIBaseMovementTimeUCEffect(this); }
}

}
