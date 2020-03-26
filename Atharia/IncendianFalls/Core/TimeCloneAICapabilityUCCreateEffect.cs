using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TimeCloneAICapabilityUCCreateEffect : ITimeCloneAICapabilityUCEffect {
  public readonly int id;
  public readonly TimeCloneAICapabilityUCIncarnation incarnation;
  public TimeCloneAICapabilityUCCreateEffect(int id, TimeCloneAICapabilityUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ITimeCloneAICapabilityUCEffect.id => id;
  public void visitITimeCloneAICapabilityUCEffect(ITimeCloneAICapabilityUCEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCEffect(this);
  }
}

}
