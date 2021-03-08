using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeCloneAICapabilityUCMutSetCreateEffect : ITimeCloneAICapabilityUCMutSetEffect {
  public readonly int id;
  public TimeCloneAICapabilityUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ITimeCloneAICapabilityUCMutSetEffect.id => id;
  public void visitITimeCloneAICapabilityUCMutSetEffect(ITimeCloneAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
