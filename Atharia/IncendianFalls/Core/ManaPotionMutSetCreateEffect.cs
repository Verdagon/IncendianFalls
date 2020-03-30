using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ManaPotionMutSetCreateEffect : IManaPotionMutSetEffect {
  public readonly int id;
  public ManaPotionMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IManaPotionMutSetEffect.id => id;
  public void visitIManaPotionMutSetEffect(IManaPotionMutSetEffectVisitor visitor) {
    visitor.visitManaPotionMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitManaPotionMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
