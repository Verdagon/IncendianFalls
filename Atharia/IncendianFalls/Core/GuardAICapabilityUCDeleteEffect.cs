using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GuardAICapabilityUCDeleteEffect : IGuardAICapabilityUCEffect {
  public readonly int id;
  public GuardAICapabilityUCDeleteEffect(int id) {
    this.id = id;
  }
  int IGuardAICapabilityUCEffect.id => id;
  public void visit(IGuardAICapabilityUCEffectVisitor visitor) {
    visitor.visitGuardAICapabilityUCDeleteEffect(this);
  }
}

}
