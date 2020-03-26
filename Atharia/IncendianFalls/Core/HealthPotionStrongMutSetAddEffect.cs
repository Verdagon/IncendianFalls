using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HealthPotionStrongMutSetAddEffect : IHealthPotionStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public HealthPotionStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IHealthPotionStrongMutSetEffect.id => id;
  public void visitIHealthPotionStrongMutSetEffect(IHealthPotionStrongMutSetEffectVisitor visitor) {
    visitor.visitHealthPotionStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitHealthPotionStrongMutSetEffect(this);
  }
}

}
