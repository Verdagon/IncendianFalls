using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct AttackImpulseCreateEffect : IAttackImpulseEffect {
  public readonly int id;
  public readonly AttackImpulseIncarnation incarnation;
  public AttackImpulseCreateEffect(
      int id,
      AttackImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IAttackImpulseEffect.id => id;
  public void visit(IAttackImpulseEffectVisitor visitor) {
    visitor.visitAttackImpulseCreateEffect(this);
  }
}
       
}
