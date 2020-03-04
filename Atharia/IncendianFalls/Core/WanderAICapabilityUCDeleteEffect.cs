using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct WanderAICapabilityUCDeleteEffect : IWanderAICapabilityUCEffect {
  public readonly int id;
  public WanderAICapabilityUCDeleteEffect(int id) {
    this.id = id;
  }
  int IWanderAICapabilityUCEffect.id => id;
  public void visit(IWanderAICapabilityUCEffectVisitor visitor) {
    visitor.visitWanderAICapabilityUCDeleteEffect(this);
  }
}

}