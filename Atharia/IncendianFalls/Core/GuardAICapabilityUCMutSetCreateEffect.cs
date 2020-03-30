using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GuardAICapabilityUCMutSetCreateEffect : IGuardAICapabilityUCMutSetEffect {
  public readonly int id;
  public GuardAICapabilityUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IGuardAICapabilityUCMutSetEffect.id => id;
  public void visitIGuardAICapabilityUCMutSetEffect(IGuardAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitGuardAICapabilityUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGuardAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
