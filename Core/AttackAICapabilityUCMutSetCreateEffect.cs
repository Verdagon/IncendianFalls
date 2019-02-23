using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackAICapabilityUCMutSetCreateEffect : IAttackAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly AttackAICapabilityUCMutSetIncarnation incarnation;
  public AttackAICapabilityUCMutSetCreateEffect(
      int id,
      AttackAICapabilityUCMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IAttackAICapabilityUCMutSetEffect.id => id;
  public void visit(IAttackAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCMutSetCreateEffect(this);
  }
}

}
