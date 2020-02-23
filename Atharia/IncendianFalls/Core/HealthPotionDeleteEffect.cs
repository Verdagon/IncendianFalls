using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct HealthPotionDeleteEffect : IHealthPotionEffect {
  public readonly int id;
  public HealthPotionDeleteEffect(int id) {
    this.id = id;
  }
  int IHealthPotionEffect.id => id;
  public void visit(IHealthPotionEffectVisitor visitor) {
    visitor.visitHealthPotionDeleteEffect(this);
  }
}

}
