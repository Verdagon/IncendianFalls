using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct AttackAICapabilityUCCreateEffect : IAttackAICapabilityUCEffect {
  public readonly int id;
  public AttackAICapabilityUCCreateEffect(int id) {
    this.id = id;
  }
  int IAttackAICapabilityUCEffect.id => id;
  public void visit(IAttackAICapabilityUCEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCCreateEffect(this);
  }
}

}
