using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WanderAICapabilityUCMutSetCreateEffect : IWanderAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly WanderAICapabilityUCMutSetIncarnation incarnation;
  public WanderAICapabilityUCMutSetCreateEffect(
      int id,
      WanderAICapabilityUCMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IWanderAICapabilityUCMutSetEffect.id => id;
  public void visit(IWanderAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitWanderAICapabilityUCMutSetCreateEffect(this);
  }
}

}
