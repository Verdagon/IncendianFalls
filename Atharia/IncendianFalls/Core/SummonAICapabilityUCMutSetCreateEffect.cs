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
  public void visit(ISummonAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCMutSetCreateEffect(this);
  }
}

}
