using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WanderAICapabilityUCMutSetCreateEffect : IWanderAICapabilityUCMutSetEffect {
  public readonly int id;
  public WanderAICapabilityUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IWanderAICapabilityUCMutSetEffect.id => id;
  public void visitIWanderAICapabilityUCMutSetEffect(IWanderAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitWanderAICapabilityUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWanderAICapabilityUCMutSetEffect(this);
  }
}

}
