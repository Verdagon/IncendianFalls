using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackAICapabilityUCWeakMutSetDeleteEffect : IAttackAICapabilityUCWeakMutSetEffect {
  public readonly int id;
  public AttackAICapabilityUCWeakMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IAttackAICapabilityUCWeakMutSetEffect.id => id;
  public void visitIAttackAICapabilityUCWeakMutSetEffect(IAttackAICapabilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCWeakMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCWeakMutSetEffect(this);
  }
}

}
