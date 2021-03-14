using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ExplosionRodIncarnation : IExplosionRodEffectVisitor {
  public ExplosionRodIncarnation(
) {
  }
  public ExplosionRodIncarnation Copy() {
    return new ExplosionRodIncarnation(
    );
  }

  public void visitExplosionRodCreateEffect(ExplosionRodCreateEffect e) {}
  public void visitExplosionRodDeleteEffect(ExplosionRodDeleteEffect e) {}

  public void ApplyEffect(IExplosionRodEffect effect) { effect.visitIExplosionRodEffect(this); }
}

}
