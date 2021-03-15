using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct OnFireUCSetTurnsRemainingEffect : IOnFireUCEffect {
  public readonly int id;
  public readonly int newValue;
  public OnFireUCSetTurnsRemainingEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IOnFireUCEffect.id => id;

  public void visitIOnFireUCEffect(IOnFireUCEffectVisitor visitor) {
    visitor.visitOnFireUCSetTurnsRemainingEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitOnFireUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
