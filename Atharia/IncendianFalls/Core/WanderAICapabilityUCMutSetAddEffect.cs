using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WanderAICapabilityUCMutSetAddEffect : IWanderAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public WanderAICapabilityUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IWanderAICapabilityUCMutSetEffect.id => id;
  public void visit(IWanderAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitWanderAICapabilityUCMutSetAddEffect(this);
  }
}

}
