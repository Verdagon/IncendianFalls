using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackAICapabilityUCMutSetDeleteEffect : IAttackAICapabilityUCMutSetEffect {
  public readonly int id;
  public AttackAICapabilityUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IAttackAICapabilityUCMutSetEffect.id => id;
  public void visit(IAttackAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCMutSetDeleteEffect(this);
  }
}

}
