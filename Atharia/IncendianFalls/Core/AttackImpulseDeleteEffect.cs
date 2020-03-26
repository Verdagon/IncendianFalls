using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct AttackImpulseDeleteEffect : IAttackImpulseEffect {
  public readonly int id;
  public AttackImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IAttackImpulseEffect.id => id;
  public void visitIAttackImpulseEffect(IAttackImpulseEffectVisitor visitor) {
    visitor.visitAttackImpulseDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitAttackImpulseEffect(this);
  }
}

}
