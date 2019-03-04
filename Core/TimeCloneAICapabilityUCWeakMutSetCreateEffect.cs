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
  public void visit(ITimeCloneAICapabilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCWeakMutSetCreateEffect(this);
  }
}

}
