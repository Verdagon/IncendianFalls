using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TemporaryCloneAICapabilityUCSetChargesEffect : ITemporaryCloneAICapabilityUCEffect {
  public readonly int id;
  public readonly int newValue;
  public TemporaryCloneAICapabilityUCSetChargesEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int ITemporaryCloneAICapabilityUCEffect.id => id;

  public void visitITemporaryCloneAICapabilityUCEffect(ITemporaryCloneAICapabilityUCEffectVisitor visitor) {
    visitor.visitTemporaryCloneAICapabilityUCSetChargesEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTemporaryCloneAICapabilityUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
