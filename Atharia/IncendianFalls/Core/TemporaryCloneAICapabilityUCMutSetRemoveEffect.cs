using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TemporaryCloneAICapabilityUCMutSetRemoveEffect : ITemporaryCloneAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public TemporaryCloneAICapabilityUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ITemporaryCloneAICapabilityUCMutSetEffect.id => id;
  public void visit(ITemporaryCloneAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitTemporaryCloneAICapabilityUCMutSetRemoveEffect(this);
  }
}

}
