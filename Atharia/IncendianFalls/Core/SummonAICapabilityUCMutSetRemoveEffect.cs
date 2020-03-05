using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SummonAICapabilityUCMutSetRemoveEffect : ISummonAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public SummonAICapabilityUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ISummonAICapabilityUCMutSetEffect.id => id;
  public void visit(ISummonAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCMutSetRemoveEffect(this);
  }
}

}
