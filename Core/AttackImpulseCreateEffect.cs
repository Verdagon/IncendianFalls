using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct AttackImpulseCreateEffect : IAttackImpulseEffect {
  public readonly int id;
  public AttackImpulseCreateEffect(int id) {
    this.id = id;
  }
  int IAttackImpulseEffect.id => id;
  public void visit(IAttackImpulseEffectVisitor visitor) {
    visitor.visitAttackImpulseCreateEffect(this);
  }
}

}
