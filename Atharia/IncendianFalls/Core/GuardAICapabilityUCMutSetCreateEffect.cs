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
  public void visit(IGuardAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitGuardAICapabilityUCMutSetCreateEffect(this);
  }
}

}