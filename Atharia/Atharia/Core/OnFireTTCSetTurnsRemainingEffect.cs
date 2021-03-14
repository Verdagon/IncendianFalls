using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct OnFireTTCSetTurnsRemainingEffect : IOnFireTTCEffect {
  public readonly int id;
  public readonly int newValue;
  public OnFireTTCSetTurnsRemainingEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IOnFireTTCEffect.id => id;

  public void visitIOnFireTTCEffect(IOnFireTTCEffectVisitor visitor) {
    visitor.visitOnFireTTCSetTurnsRemainingEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitOnFireTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
