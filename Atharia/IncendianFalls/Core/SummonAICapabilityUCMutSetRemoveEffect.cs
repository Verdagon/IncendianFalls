using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SummonAICapabilityUCMutSetRemoveEffect : ISummonAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public SummonAICapabilityUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ISummonAICapabilityUCMutSetEffect.id => id;
  public void visitISummonAICapabilityUCMutSetEffect(ISummonAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCMutSetEffect(this);
  }
}

}
