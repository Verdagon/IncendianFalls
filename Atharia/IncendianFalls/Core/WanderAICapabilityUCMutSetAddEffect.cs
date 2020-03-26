using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WanderAICapabilityUCMutSetAddEffect : IWanderAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public WanderAICapabilityUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IWanderAICapabilityUCMutSetEffect.id => id;
  public void visitIWanderAICapabilityUCMutSetEffect(IWanderAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitWanderAICapabilityUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWanderAICapabilityUCMutSetEffect(this);
  }
}

}
