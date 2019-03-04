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
  public void visit(ITimeCloneAICapabilityUCEffectVisitor visitor) {
    visitor.visitTimeCloneAICapabilityUCDeleteEffect(this);
  }
}

}
