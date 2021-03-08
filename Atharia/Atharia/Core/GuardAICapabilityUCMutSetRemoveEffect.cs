using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GuardAICapabilityUCMutSetRemoveEffect : IGuardAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public GuardAICapabilityUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IGuardAICapabilityUCMutSetEffect.id => id;
  public void visitIGuardAICapabilityUCMutSetEffect(IGuardAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitGuardAICapabilityUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGuardAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
