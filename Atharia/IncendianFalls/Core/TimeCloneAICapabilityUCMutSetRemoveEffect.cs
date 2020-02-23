using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeCloneAICapabilityUCMutSetRemoveEffect : ITimeCloneAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public TimeCloneAICapabilityUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ITimeCloneAICapabilityUCMutSetEffect.id => id;
  public void visit(ITimeCloneAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCMutSetRemoveEffect(this);
  }
}

}
