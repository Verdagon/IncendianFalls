using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ManaPotionStrongMutSetRemoveEffect : IManaPotionStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ManaPotionStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IManaPotionStrongMutSetEffect.id => id;
  public void visitIManaPotionStrongMutSetEffect(IManaPotionStrongMutSetEffectVisitor visitor) {
    visitor.visitManaPotionStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitManaPotionStrongMutSetEffect(this);
  }
}

}
