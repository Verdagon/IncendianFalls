using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ExplosionRodStrongMutSetRemoveEffect : IExplosionRodStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ExplosionRodStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IExplosionRodStrongMutSetEffect.id => id;
  public void visitIExplosionRodStrongMutSetEffect(IExplosionRodStrongMutSetEffectVisitor visitor) {
    visitor.visitExplosionRodStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitExplosionRodStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
