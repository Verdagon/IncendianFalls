using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WanderAICapabilityUCMutSetRemoveEffect : IWanderAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public WanderAICapabilityUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IWanderAICapabilityUCMutSetEffect.id => id;
  public void visitIWanderAICapabilityUCMutSetEffect(IWanderAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitWanderAICapabilityUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWanderAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
