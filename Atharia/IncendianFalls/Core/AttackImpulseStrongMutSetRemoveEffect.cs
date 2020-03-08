using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackImpulseStrongMutSetRemoveEffect : IAttackImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public AttackImpulseStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IAttackImpulseStrongMutSetEffect.id => id;
  public void visit(IAttackImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitAttackImpulseStrongMutSetRemoveEffect(this);
  }
}

}
