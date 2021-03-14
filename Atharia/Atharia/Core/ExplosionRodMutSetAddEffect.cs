using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ExplosionRodMutSetAddEffect : IExplosionRodMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ExplosionRodMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IExplosionRodMutSetEffect.id => id;
  public void visitIExplosionRodMutSetEffect(IExplosionRodMutSetEffectVisitor visitor) {
    visitor.visitExplosionRodMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitExplosionRodMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
