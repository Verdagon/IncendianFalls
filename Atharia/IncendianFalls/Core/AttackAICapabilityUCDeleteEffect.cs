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
  public void visitIAttackAICapabilityUCEffect(IAttackAICapabilityUCEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
