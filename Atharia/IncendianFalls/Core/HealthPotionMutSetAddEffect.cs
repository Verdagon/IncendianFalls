using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HealthPotionMutSetAddEffect : IHealthPotionMutSetEffect {
  public readonly int id;
  public readonly int element;
  public HealthPotionMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IHealthPotionMutSetEffect.id => id;
  public void visitIHealthPotionMutSetEffect(IHealthPotionMutSetEffectVisitor visitor) {
    visitor.visitHealthPotionMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitHealthPotionMutSetEffect(this);
  }
}

}
