using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HealthPotionStrongMutSetRemoveEffect : IHealthPotionStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public HealthPotionStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IHealthPotionStrongMutSetEffect.id => id;
  public void visit(IHealthPotionStrongMutSetEffectVisitor visitor) {
    visitor.visitHealthPotionStrongMutSetRemoveEffect(this);
  }
}

}
