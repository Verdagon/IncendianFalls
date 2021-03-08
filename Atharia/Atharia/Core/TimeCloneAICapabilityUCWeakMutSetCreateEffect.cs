using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeCloneAICapabilityUCWeakMutSetCreateEffect : ITimeCloneAICapabilityUCWeakMutSetEffect {
  public readonly int id;
  public TimeCloneAICapabilityUCWeakMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ITimeCloneAICapabilityUCWeakMutSetEffect.id => id;
  public void visitITimeCloneAICapabilityUCWeakMutSetEffect(ITimeCloneAICapabilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCWeakMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
