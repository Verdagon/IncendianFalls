using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeCloneAICapabilityUCWeakMutSetAddEffect : ITimeCloneAICapabilityUCWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public TimeCloneAICapabilityUCWeakMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ITimeCloneAICapabilityUCWeakMutSetEffect.id => id;
  public void visitITimeCloneAICapabilityUCWeakMutSetEffect(ITimeCloneAICapabilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCWeakMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
