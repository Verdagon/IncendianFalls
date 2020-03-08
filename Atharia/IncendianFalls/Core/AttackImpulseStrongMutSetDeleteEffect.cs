using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackImpulseStrongMutSetDeleteEffect : IAttackImpulseStrongMutSetEffect {
  public readonly int id;
  public AttackImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IAttackImpulseStrongMutSetEffect.id => id;
  public void visit(IAttackImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitAttackImpulseStrongMutSetDeleteEffect(this);
  }
}

}