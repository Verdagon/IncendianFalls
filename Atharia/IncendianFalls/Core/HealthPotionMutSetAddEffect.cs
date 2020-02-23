using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HealthPotionMutSetAddEffect : IHealthPotionMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public HealthPotionMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IHealthPotionMutSetEffect.id => id;
  public void visit(IHealthPotionMutSetEffectVisitor visitor) {
    visitor.visitHealthPotionMutSetAddEffect(this);
  }
}

}
