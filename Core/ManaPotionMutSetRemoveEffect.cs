using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ManaPotionMutSetRemoveEffect : IManaPotionMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ManaPotionMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IManaPotionMutSetEffect.id => id;
  public void visit(IManaPotionMutSetEffectVisitor visitor) {
    visitor.visitManaPotionMutSetRemoveEffect(this);
  }
}

}
