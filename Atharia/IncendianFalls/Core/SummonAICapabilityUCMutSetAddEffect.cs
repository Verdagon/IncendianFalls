using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SummonAICapabilityUCMutSetAddEffect : ISummonAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public SummonAICapabilityUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ISummonAICapabilityUCMutSetEffect.id => id;
  public void visit(ISummonAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCMutSetAddEffect(this);
  }
}

}
