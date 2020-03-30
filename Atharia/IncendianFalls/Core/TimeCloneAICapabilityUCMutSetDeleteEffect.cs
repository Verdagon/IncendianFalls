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
  public void visitITimeCloneAICapabilityUCMutSetEffect(ITimeCloneAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
