using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ExplosionRodDeleteEffect : IExplosionRodEffect {
  public readonly int id;
  public ExplosionRodDeleteEffect(int id) {
    this.id = id;
  }
  int IExplosionRodEffect.id => id;
  public void visitIExplosionRodEffect(IExplosionRodEffectVisitor visitor) {
    visitor.visitExplosionRodDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitExplosionRodEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
