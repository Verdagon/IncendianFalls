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
  public void visit(IGuardAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitGuardAICapabilityUCMutSetDeleteEffect(this);
  }
}

}
