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
  public void visitITemporaryCloneAICapabilityUCMutSetEffect(ITemporaryCloneAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitTemporaryCloneAICapabilityUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTemporaryCloneAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
