using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TimeCloneAICapabilityUCDeleteEffect : ITimeCloneAICapabilityUCEffect {
  public readonly int id;
  public TimeCloneAICapabilityUCDeleteEffect(int id) {
    this.id = id;
  }
  int ITimeCloneAICapabilityUCEffect.id => id;
  public void visitITimeCloneAICapabilityUCEffect(ITimeCloneAICapabilityUCEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
