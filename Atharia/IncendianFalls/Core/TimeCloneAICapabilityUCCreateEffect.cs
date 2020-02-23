using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TimeCloneAICapabilityUCCreateEffect : ITimeCloneAICapabilityUCEffect {
  public readonly int id;
  public TimeCloneAICapabilityUCCreateEffect(int id) {
    this.id = id;
  }
  int ITimeCloneAICapabilityUCEffect.id => id;
  public void visit(ITimeCloneAICapabilityUCEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCCreateEffect(this);
  }
}

}
