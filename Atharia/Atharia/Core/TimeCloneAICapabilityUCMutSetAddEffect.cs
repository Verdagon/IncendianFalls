using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeCloneAICapabilityUCMutSetAddEffect : ITimeCloneAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public TimeCloneAICapabilityUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ITimeCloneAICapabilityUCMutSetEffect.id => id;
  public void visitITimeCloneAICapabilityUCMutSetEffect(ITimeCloneAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
