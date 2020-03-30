using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SummonAICapabilityUCMutSetDeleteEffect : ISummonAICapabilityUCMutSetEffect {
  public readonly int id;
  public SummonAICapabilityUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ISummonAICapabilityUCMutSetEffect.id => id;
  public void visitISummonAICapabilityUCMutSetEffect(ISummonAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
