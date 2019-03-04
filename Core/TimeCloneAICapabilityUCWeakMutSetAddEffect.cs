using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeCloneAICapabilityUCWeakMutSetAddEffect : ITimeCloneAICapabilityUCWeakMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public TimeCloneAICapabilityUCWeakMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ITimeCloneAICapabilityUCWeakMutSetEffect.id => id;
  public void visit(ITimeCloneAICapabilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCWeakMutSetAddEffect(this);
  }
}

}
