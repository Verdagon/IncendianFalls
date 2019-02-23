using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct AttackAICapabilityUCCreateEffect : IAttackAICapabilityUCEffect {
  public readonly int id;
  public readonly AttackAICapabilityUCIncarnation incarnation;
  public AttackAICapabilityUCCreateEffect(
      int id,
      AttackAICapabilityUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IAttackAICapabilityUCEffect.id => id;
  public void visit(IAttackAICapabilityUCEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCCreateEffect(this);
  }
}
       
}
