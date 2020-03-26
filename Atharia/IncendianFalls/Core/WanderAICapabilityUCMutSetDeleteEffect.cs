using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WanderAICapabilityUCMutSetDeleteEffect : IWanderAICapabilityUCMutSetEffect {
  public readonly int id;
  public WanderAICapabilityUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IWanderAICapabilityUCMutSetEffect.id => id;
  public void visitIWanderAICapabilityUCMutSetEffect(IWanderAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitWanderAICapabilityUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWanderAICapabilityUCMutSetEffect(this);
  }
}

}
