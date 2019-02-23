using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct WanderAICapabilityUCCreateEffect : IWanderAICapabilityUCEffect {
  public readonly int id;
  public readonly WanderAICapabilityUCIncarnation incarnation;
  public WanderAICapabilityUCCreateEffect(
      int id,
      WanderAICapabilityUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IWanderAICapabilityUCEffect.id => id;
  public void visit(IWanderAICapabilityUCEffectVisitor visitor) {
    visitor.visitWanderAICapabilityUCCreateEffect(this);
  }
}
       
}
