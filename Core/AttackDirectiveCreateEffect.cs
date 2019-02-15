using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct AttackDirectiveCreateEffect : IAttackDirectiveEffect {
  public readonly int id;
  public readonly AttackDirectiveIncarnation incarnation;
  public AttackDirectiveCreateEffect(
      int id,
      AttackDirectiveIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IAttackDirectiveEffect.id => id;
  public void visit(IAttackDirectiveEffectVisitor visitor) {
    visitor.visitAttackDirectiveCreateEffect(this);
  }
}
       
}
