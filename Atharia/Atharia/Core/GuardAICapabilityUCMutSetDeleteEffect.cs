using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GuardAICapabilityUCMutSetDeleteEffect : IGuardAICapabilityUCMutSetEffect {
  public readonly int id;
  public GuardAICapabilityUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IGuardAICapabilityUCMutSetEffect.id => id;
  public void visitIGuardAICapabilityUCMutSetEffect(IGuardAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitGuardAICapabilityUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGuardAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
