using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GuardAICapabilityUCMutSetAddEffect : IGuardAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public GuardAICapabilityUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IGuardAICapabilityUCMutSetEffect.id => id;
  public void visitIGuardAICapabilityUCMutSetEffect(IGuardAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitGuardAICapabilityUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGuardAICapabilityUCMutSetEffect(this);
  }
}

}
