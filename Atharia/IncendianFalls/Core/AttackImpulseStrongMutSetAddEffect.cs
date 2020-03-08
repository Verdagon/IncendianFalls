using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackImpulseStrongMutSetAddEffect : IAttackImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public AttackImpulseStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IAttackImpulseStrongMutSetEffect.id => id;
  public void visit(IAttackImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitAttackImpulseStrongMutSetAddEffect(this);
  }
}

}
