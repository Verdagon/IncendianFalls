using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ManaPotionStrongMutSetAddEffect : IManaPotionStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ManaPotionStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IManaPotionStrongMutSetEffect.id => id;
  public void visitIManaPotionStrongMutSetEffect(IManaPotionStrongMutSetEffectVisitor visitor) {
    visitor.visitManaPotionStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitManaPotionStrongMutSetEffect(this);
  }
}

}
