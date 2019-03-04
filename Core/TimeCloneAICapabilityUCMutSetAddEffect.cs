using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeCloneAICapabilityUCMutSetAddEffect : ITimeCloneAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public TimeCloneAICapabilityUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ITimeCloneAICapabilityUCMutSetEffect.id => id;
  public void visit(ITimeCloneAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCMutSetAddEffect(this);
  }
}

}
