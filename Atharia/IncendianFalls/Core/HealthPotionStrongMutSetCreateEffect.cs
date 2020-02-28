using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HealthPotionStrongMutSetCreateEffect : IHealthPotionStrongMutSetEffect {
  public readonly int id;
  public HealthPotionStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IHealthPotionStrongMutSetEffect.id => id;
  public void visit(IHealthPotionStrongMutSetEffectVisitor visitor) {
    visitor.visitHealthPotionStrongMutSetCreateEffect(this);
  }
}

}
