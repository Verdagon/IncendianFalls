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
  public void visitIHealthPotionStrongMutSetEffect(IHealthPotionStrongMutSetEffectVisitor visitor) {
    visitor.visitHealthPotionStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitHealthPotionStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
