using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ExplosionRodMutSetRemoveEffect : IExplosionRodMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ExplosionRodMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IExplosionRodMutSetEffect.id => id;
  public void visitIExplosionRodMutSetEffect(IExplosionRodMutSetEffectVisitor visitor) {
    visitor.visitExplosionRodMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitExplosionRodMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
