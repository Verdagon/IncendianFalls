using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HealthPotionMutSetDeleteEffect : IHealthPotionMutSetEffect {
  public readonly int id;
  public HealthPotionMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IHealthPotionMutSetEffect.id => id;
  public void visit(IHealthPotionMutSetEffectVisitor visitor) {
    visitor.visitHealthPotionMutSetDeleteEffect(this);
  }
}

}
