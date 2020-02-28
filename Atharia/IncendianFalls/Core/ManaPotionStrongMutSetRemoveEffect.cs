using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ManaPotionStrongMutSetRemoveEffect : IManaPotionStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ManaPotionStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IManaPotionStrongMutSetEffect.id => id;
  public void visit(IManaPotionStrongMutSetEffectVisitor visitor) {
    visitor.visitManaPotionStrongMutSetRemoveEffect(this);
  }
}

}
