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
  public void visitIAttackAICapabilityUCMutSetEffect(IAttackAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
