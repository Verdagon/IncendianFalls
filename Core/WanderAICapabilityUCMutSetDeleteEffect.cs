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
  public void visit(IWanderAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitWanderAICapabilityUCMutSetDeleteEffect(this);
  }
}

}
