using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HealthPotionStrongMutSetDeleteEffect : IHealthPotionStrongMutSetEffect {
  public readonly int id;
  public HealthPotionStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IHealthPotionStrongMutSetEffect.id => id;
  public void visit(IHealthPotionStrongMutSetEffectVisitor visitor) {
    visitor.visitHealthPotionStrongMutSetDeleteEffect(this);
  }
}

}
