using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SummonAICapabilityUCSetChargesEffect : ISummonAICapabilityUCEffect {
  public readonly int id;
  public readonly int newValue;
  public SummonAICapabilityUCSetChargesEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int ISummonAICapabilityUCEffect.id => id;

  public void visitISummonAICapabilityUCEffect(ISummonAICapabilityUCEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCSetChargesEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
