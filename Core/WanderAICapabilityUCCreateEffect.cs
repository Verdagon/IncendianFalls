using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct WanderAICapabilityUCCreateEffect : IWanderAICapabilityUCEffect {
  public readonly int id;
  public WanderAICapabilityUCCreateEffect(int id) {
    this.id = id;
  }
  int IWanderAICapabilityUCEffect.id => id;
  public void visit(IWanderAICapabilityUCEffectVisitor visitor) {
    visitor.visitWanderAICapabilityUCCreateEffect(this);
  }
}

}
