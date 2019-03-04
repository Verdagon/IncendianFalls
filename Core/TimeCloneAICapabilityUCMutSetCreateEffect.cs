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
  public void visit(ITimeCloneAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCMutSetCreateEffect(this);
  }
}

}