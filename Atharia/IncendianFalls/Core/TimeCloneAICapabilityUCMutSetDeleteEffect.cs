using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeCloneAICapabilityUCMutSetDeleteEffect : ITimeCloneAICapabilityUCMutSetEffect {
  public readonly int id;
  public TimeCloneAICapabilityUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ITimeCloneAICapabilityUCMutSetEffect.id => id;
  public void visit(ITimeCloneAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCMutSetDeleteEffect(this);
  }
}

}
