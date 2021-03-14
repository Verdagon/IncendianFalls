using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ExplosionRodMutSetCreateEffect : IExplosionRodMutSetEffect {
  public readonly int id;
  public ExplosionRodMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IExplosionRodMutSetEffect.id => id;
  public void visitIExplosionRodMutSetEffect(IExplosionRodMutSetEffectVisitor visitor) {
    visitor.visitExplosionRodMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitExplosionRodMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
