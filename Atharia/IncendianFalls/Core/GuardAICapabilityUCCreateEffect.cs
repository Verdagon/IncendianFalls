using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GuardAICapabilityUCCreateEffect : IGuardAICapabilityUCEffect {
  public readonly int id;
  public GuardAICapabilityUCCreateEffect(int id) {
    this.id = id;
  }
  int IGuardAICapabilityUCEffect.id => id;
  public void visit(IGuardAICapabilityUCEffectVisitor visitor) {
    visitor.visitGuardAICapabilityUCCreateEffect(this);
  }
}

}
