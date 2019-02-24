using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackAICapabilityUCWeakMutSetCreateEffect : IAttackAICapabilityUCWeakMutSetEffect {
  public readonly int id;
  public readonly AttackAICapabilityUCWeakMutSetIncarnation incarnation;
  public AttackAICapabilityUCWeakMutSetCreateEffect(
      int id,
      AttackAICapabilityUCWeakMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IAttackAICapabilityUCWeakMutSetEffect.id => id;
  public void visit(IAttackAICapabilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCWeakMutSetCreateEffect(this);
  }
}

}
