using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackImpulseStrongMutSetCreateEffect : IAttackImpulseStrongMutSetEffect {
  public readonly int id;
  public AttackImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IAttackImpulseStrongMutSetEffect.id => id;
  public void visitIAttackImpulseStrongMutSetEffect(IAttackImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitAttackImpulseStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitAttackImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
