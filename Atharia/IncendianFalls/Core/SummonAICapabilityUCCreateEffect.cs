using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SummonAICapabilityUCCreateEffect : ISummonAICapabilityUCEffect {
  public readonly int id;
  public SummonAICapabilityUCCreateEffect(int id) {
    this.id = id;
  }
  int ISummonAICapabilityUCEffect.id => id;
  public void visit(ISummonAICapabilityUCEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCCreateEffect(this);
  }
}

}
