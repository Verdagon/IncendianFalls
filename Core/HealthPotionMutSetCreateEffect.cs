using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HealthPotionMutSetCreateEffect : IHealthPotionMutSetEffect {
  public readonly int id;
  public HealthPotionMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IHealthPotionMutSetEffect.id => id;
  public void visit(IHealthPotionMutSetEffectVisitor visitor) {
    visitor.visitHealthPotionMutSetCreateEffect(this);
  }
}

}
