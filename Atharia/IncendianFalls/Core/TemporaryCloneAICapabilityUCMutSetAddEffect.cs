using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TemporaryCloneAICapabilityUCMutSetAddEffect : ITemporaryCloneAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public TemporaryCloneAICapabilityUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ITemporaryCloneAICapabilityUCMutSetEffect.id => id;
  public void visitITemporaryCloneAICapabilityUCMutSetEffect(ITemporaryCloneAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitTemporaryCloneAICapabilityUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTemporaryCloneAICapabilityUCMutSetEffect(this);
  }
}

}
