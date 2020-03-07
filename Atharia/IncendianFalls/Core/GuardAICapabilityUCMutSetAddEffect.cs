using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GuardAICapabilityUCMutSetAddEffect : IGuardAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public GuardAICapabilityUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IGuardAICapabilityUCMutSetEffect.id => id;
  public void visit(IGuardAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitGuardAICapabilityUCMutSetAddEffect(this);
  }
}

}
