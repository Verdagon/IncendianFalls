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
  public void visitIHealthPotionEffect(IHealthPotionEffectVisitor visitor) {
    visitor.visitHealthPotionDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitHealthPotionEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
