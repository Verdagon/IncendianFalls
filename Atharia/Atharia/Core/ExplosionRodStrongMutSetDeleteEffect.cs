using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ExplosionRodStrongMutSetDeleteEffect : IExplosionRodStrongMutSetEffect {
  public readonly int id;
  public ExplosionRodStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IExplosionRodStrongMutSetEffect.id => id;
  public void visitIExplosionRodStrongMutSetEffect(IExplosionRodStrongMutSetEffectVisitor visitor) {
    visitor.visitExplosionRodStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitExplosionRodStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
