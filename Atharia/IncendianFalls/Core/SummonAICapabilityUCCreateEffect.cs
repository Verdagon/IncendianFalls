using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SummonAICapabilityUCCreateEffect : ISummonAICapabilityUCEffect {
  public readonly int id;
  public readonly SummonAICapabilityUCIncarnation incarnation;
  public SummonAICapabilityUCCreateEffect(int id, SummonAICapabilityUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ISummonAICapabilityUCEffect.id => id;
  public void visitISummonAICapabilityUCEffect(ISummonAICapabilityUCEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
