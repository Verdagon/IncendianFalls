using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TemporaryCloneAICapabilityUCMutSetRemoveEffect : ITemporaryCloneAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public TemporaryCloneAICapabilityUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ITemporaryCloneAICapabilityUCMutSetEffect.id => id;
  public void visitITemporaryCloneAICapabilityUCMutSetEffect(ITemporaryCloneAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitTemporaryCloneAICapabilityUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTemporaryCloneAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
