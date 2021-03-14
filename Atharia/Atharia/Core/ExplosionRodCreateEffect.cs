using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ExplosionRodCreateEffect : IExplosionRodEffect {
  public readonly int id;
  public readonly ExplosionRodIncarnation incarnation;
  public ExplosionRodCreateEffect(int id, ExplosionRodIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IExplosionRodEffect.id => id;
  public void visitIExplosionRodEffect(IExplosionRodEffectVisitor visitor) {
    visitor.visitExplosionRodCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitExplosionRodEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
