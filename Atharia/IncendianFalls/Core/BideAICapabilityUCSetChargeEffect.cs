using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BideAICapabilityUCSetChargeEffect : IBideAICapabilityUCEffect {
  public readonly int id;
  public readonly int newValue;
  public BideAICapabilityUCSetChargeEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IBideAICapabilityUCEffect.id => id;

  public void visitIBideAICapabilityUCEffect(IBideAICapabilityUCEffectVisitor visitor) {
    visitor.visitBideAICapabilityUCSetChargeEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBideAICapabilityUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
