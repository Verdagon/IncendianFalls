using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct AttackAICapabilityUCSetKillDirectiveEffect : IAttackAICapabilityUCEffect {
  public readonly int id;
  public readonly KillDirective newValue;
  public AttackAICapabilityUCSetKillDirectiveEffect(
      int id,
      KillDirective newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IAttackAICapabilityUCEffect.id => id;

  public void visit(IAttackAICapabilityUCEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCSetKillDirectiveEffect(this);
  }
}

}
