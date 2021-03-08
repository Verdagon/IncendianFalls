using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SummonAICapabilityUCMutSetAddEffect : ISummonAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public SummonAICapabilityUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ISummonAICapabilityUCMutSetEffect.id => id;
  public void visitISummonAICapabilityUCMutSetEffect(ISummonAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
