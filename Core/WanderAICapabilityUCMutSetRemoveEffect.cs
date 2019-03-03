using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WanderAICapabilityUCMutSetRemoveEffect : IWanderAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public WanderAICapabilityUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IWanderAICapabilityUCMutSetEffect.id => id;
  public void visit(IWanderAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitWanderAICapabilityUCMutSetRemoveEffect(this);
  }
}

}
