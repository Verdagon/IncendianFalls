using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SummonAICapabilityUCMutSetCreateEffect : ISummonAICapabilityUCMutSetEffect {
  public readonly int id;
  public SummonAICapabilityUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ISummonAICapabilityUCMutSetEffect.id => id;
  public void visitISummonAICapabilityUCMutSetEffect(ISummonAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCMutSetEffect(this);
  }
}

}
