using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ExplosionRodStrongMutSetAddEffect : IExplosionRodStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ExplosionRodStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IExplosionRodStrongMutSetEffect.id => id;
  public void visitIExplosionRodStrongMutSetEffect(IExplosionRodStrongMutSetEffectVisitor visitor) {
    visitor.visitExplosionRodStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitExplosionRodStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
