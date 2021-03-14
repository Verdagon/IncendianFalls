using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ExplosionRodMutSetDeleteEffect : IExplosionRodMutSetEffect {
  public readonly int id;
  public ExplosionRodMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IExplosionRodMutSetEffect.id => id;
  public void visitIExplosionRodMutSetEffect(IExplosionRodMutSetEffectVisitor visitor) {
    visitor.visitExplosionRodMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitExplosionRodMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
