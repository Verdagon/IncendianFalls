using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TemporaryCloneAICapabilityUCDeleteEffect : ITemporaryCloneAICapabilityUCEffect {
  public readonly int id;
  public TemporaryCloneAICapabilityUCDeleteEffect(int id) {
    this.id = id;
  }
  int ITemporaryCloneAICapabilityUCEffect.id => id;
  public void visit(ITemporaryCloneAICapabilityUCEffectVisitor visitor) {
    visitor.visitTemporaryCloneAICapabilityUCDeleteEffect(this);
  }
}

}
