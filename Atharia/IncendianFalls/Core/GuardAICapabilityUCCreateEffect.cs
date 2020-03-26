using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GuardAICapabilityUCCreateEffect : IGuardAICapabilityUCEffect {
  public readonly int id;
  public readonly GuardAICapabilityUCIncarnation incarnation;
  public GuardAICapabilityUCCreateEffect(int id, GuardAICapabilityUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IGuardAICapabilityUCEffect.id => id;
  public void visitIGuardAICapabilityUCEffect(IGuardAICapabilityUCEffectVisitor visitor) {
    visitor.visitGuardAICapabilityUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGuardAICapabilityUCEffect(this);
  }
}

}
