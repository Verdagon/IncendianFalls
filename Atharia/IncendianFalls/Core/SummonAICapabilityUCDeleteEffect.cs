using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SummonAICapabilityUCDeleteEffect : ISummonAICapabilityUCEffect {
  public readonly int id;
  public SummonAICapabilityUCDeleteEffect(int id) {
    this.id = id;
  }
  int ISummonAICapabilityUCEffect.id => id;
  public void visitISummonAICapabilityUCEffect(ISummonAICapabilityUCEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSummonAICapabilityUCEffect(this);
  }
}

}
