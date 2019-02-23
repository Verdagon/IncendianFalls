using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct AttackAICapabilityUCDeleteEffect : IAttackAICapabilityUCEffect {
  public readonly int id;
  public AttackAICapabilityUCDeleteEffect(int id) {
    this.id = id;
  }
  int IAttackAICapabilityUCEffect.id => id;
  public void visit(IAttackAICapabilityUCEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCDeleteEffect(this);
  }
}
       
}
