using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackAICapabilityUCWeakMutSetCreateEffect : IAttackAICapabilityUCWeakMutSetEffect {
  public readonly int id;
  public AttackAICapabilityUCWeakMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IAttackAICapabilityUCWeakMutSetEffect.id => id;
  public void visit(IAttackAICapabilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCWeakMutSetCreateEffect(this);
  }
}

}
