using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct AttackAICapabilityUCSetKillDirectiveEffect : IAttackAICapabilityUCEffect {
  public readonly int id;
  public readonly int newValue;
  public AttackAICapabilityUCSetKillDirectiveEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IAttackAICapabilityUCEffect.id => id;

  public void visitIAttackAICapabilityUCEffect(IAttackAICapabilityUCEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCSetKillDirectiveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCEffect(this);
  }
}

}
