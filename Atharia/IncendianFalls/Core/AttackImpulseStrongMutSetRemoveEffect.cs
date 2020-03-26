using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackImpulseStrongMutSetRemoveEffect : IAttackImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public AttackImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IAttackImpulseStrongMutSetEffect.id => id;
  public void visitIAttackImpulseStrongMutSetEffect(IAttackImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitAttackImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitAttackImpulseStrongMutSetEffect(this);
  }
}

}
