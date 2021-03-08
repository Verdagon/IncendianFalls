using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeCloneAICapabilityUCMutSetRemoveEffect : ITimeCloneAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public TimeCloneAICapabilityUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ITimeCloneAICapabilityUCMutSetEffect.id => id;
  public void visitITimeCloneAICapabilityUCMutSetEffect(ITimeCloneAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
