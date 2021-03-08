using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ManaPotionMutSetAddEffect : IManaPotionMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ManaPotionMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IManaPotionMutSetEffect.id => id;
  public void visitIManaPotionMutSetEffect(IManaPotionMutSetEffectVisitor visitor) {
    visitor.visitManaPotionMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitManaPotionMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
