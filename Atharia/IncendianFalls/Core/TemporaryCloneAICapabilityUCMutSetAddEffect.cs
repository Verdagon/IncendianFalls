using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TemporaryCloneAICapabilityUCMutSetAddEffect : ITemporaryCloneAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public TemporaryCloneAICapabilityUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ITemporaryCloneAICapabilityUCMutSetEffect.id => id;
  public void visit(ITemporaryCloneAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitTemporaryCloneAICapabilityUCMutSetAddEffect(this);
  }
}

}
