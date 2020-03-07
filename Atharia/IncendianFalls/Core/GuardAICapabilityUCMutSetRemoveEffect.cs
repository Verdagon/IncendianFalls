using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GuardAICapabilityUCMutSetRemoveEffect : IGuardAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public GuardAICapabilityUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IGuardAICapabilityUCMutSetEffect.id => id;
  public void visit(IGuardAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitGuardAICapabilityUCMutSetRemoveEffect(this);
  }
}

}
