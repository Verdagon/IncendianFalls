using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HealthPotionMutSetRemoveEffect : IHealthPotionMutSetEffect {
  public readonly int id;
  public readonly int element;
  public HealthPotionMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IHealthPotionMutSetEffect.id => id;
  public void visitIHealthPotionMutSetEffect(IHealthPotionMutSetEffectVisitor visitor) {
    visitor.visitHealthPotionMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitHealthPotionMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
