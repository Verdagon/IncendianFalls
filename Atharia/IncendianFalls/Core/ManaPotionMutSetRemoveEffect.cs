using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ManaPotionMutSetRemoveEffect : IManaPotionMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ManaPotionMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IManaPotionMutSetEffect.id => id;
  public void visitIManaPotionMutSetEffect(IManaPotionMutSetEffectVisitor visitor) {
    visitor.visitManaPotionMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitManaPotionMutSetEffect(this);
  }
}

}
