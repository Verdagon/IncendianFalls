using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct AttackDirectiveDeleteEffect : IAttackDirectiveEffect {
  public readonly int id;
  public AttackDirectiveDeleteEffect(int id) {
    this.id = id;
  }
  int IAttackDirectiveEffect.id => id;
  public void visit(IAttackDirectiveEffectVisitor visitor) {
    visitor.visitAttackDirectiveDeleteEffect(this);
  }
}
       
}
