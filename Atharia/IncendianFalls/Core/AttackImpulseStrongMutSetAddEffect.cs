using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackImpulseStrongMutSetAddEffect : IAttackImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public AttackImpulseStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IAttackImpulseStrongMutSetEffect.id => id;
  public void visitIAttackImpulseStrongMutSetEffect(IAttackImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitAttackImpulseStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitAttackImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
