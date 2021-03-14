using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ExplosionRodStrongMutSetCreateEffect : IExplosionRodStrongMutSetEffect {
  public readonly int id;
  public ExplosionRodStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IExplosionRodStrongMutSetEffect.id => id;
  public void visitIExplosionRodStrongMutSetEffect(IExplosionRodStrongMutSetEffectVisitor visitor) {
    visitor.visitExplosionRodStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitExplosionRodStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
