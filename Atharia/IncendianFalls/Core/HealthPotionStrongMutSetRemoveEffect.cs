using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HealthPotionStrongMutSetRemoveEffect : IHealthPotionStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public HealthPotionStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IHealthPotionStrongMutSetEffect.id => id;
  public void visitIHealthPotionStrongMutSetEffect(IHealthPotionStrongMutSetEffectVisitor visitor) {
    visitor.visitHealthPotionStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitHealthPotionStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
