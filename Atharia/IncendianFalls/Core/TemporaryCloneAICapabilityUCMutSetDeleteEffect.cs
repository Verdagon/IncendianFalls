using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TemporaryCloneAICapabilityUCMutSetDeleteEffect : ITemporaryCloneAICapabilityUCMutSetEffect {
  public readonly int id;
  public TemporaryCloneAICapabilityUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ITemporaryCloneAICapabilityUCMutSetEffect.id => id;
  public void visit(ITemporaryCloneAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitTemporaryCloneAICapabilityUCMutSetDeleteEffect(this);
  }
}

}
