using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TemporaryCloneAICapabilityUCMutSetCreateEffect : ITemporaryCloneAICapabilityUCMutSetEffect {
  public readonly int id;
  public TemporaryCloneAICapabilityUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ITemporaryCloneAICapabilityUCMutSetEffect.id => id;
  public void visit(ITemporaryCloneAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitTemporaryCloneAICapabilityUCMutSetCreateEffect(this);
  }
}

}
