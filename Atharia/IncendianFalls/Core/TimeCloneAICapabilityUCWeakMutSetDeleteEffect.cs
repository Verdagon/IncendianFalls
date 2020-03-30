using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeCloneAICapabilityUCWeakMutSetDeleteEffect : ITimeCloneAICapabilityUCWeakMutSetEffect {
  public readonly int id;
  public TimeCloneAICapabilityUCWeakMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ITimeCloneAICapabilityUCWeakMutSetEffect.id => id;
  public void visitITimeCloneAICapabilityUCWeakMutSetEffect(ITimeCloneAICapabilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCWeakMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
