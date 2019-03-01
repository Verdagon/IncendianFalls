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
  public void visit(IWanderAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitWanderAICapabilityUCMutSetCreateEffect(this);
  }
}

}
