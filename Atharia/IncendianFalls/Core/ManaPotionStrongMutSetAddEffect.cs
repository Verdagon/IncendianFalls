using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ManaPotionStrongMutSetAddEffect : IManaPotionStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ManaPotionStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IManaPotionStrongMutSetEffect.id => id;
  public void visit(IManaPotionStrongMutSetEffectVisitor visitor) {
    visitor.visitManaPotionStrongMutSetAddEffect(this);
  }
}

}
