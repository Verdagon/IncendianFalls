using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HealthPotionMutSetRemoveEffect : IHealthPotionMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public HealthPotionMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IHealthPotionMutSetEffect.id => id;
  public void visit(IHealthPotionMutSetEffectVisitor visitor) {
    visitor.visitHealthPotionMutSetRemoveEffect(this);
  }
}

}
