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
  public void visitIHealthPotionMutSetEffect(IHealthPotionMutSetEffectVisitor visitor) {
    visitor.visitHealthPotionMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitHealthPotionMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
