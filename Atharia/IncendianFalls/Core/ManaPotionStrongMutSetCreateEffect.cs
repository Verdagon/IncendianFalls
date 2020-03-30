using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ManaPotionStrongMutSetCreateEffect : IManaPotionStrongMutSetEffect {
  public readonly int id;
  public ManaPotionStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IManaPotionStrongMutSetEffect.id => id;
  public void visitIManaPotionStrongMutSetEffect(IManaPotionStrongMutSetEffectVisitor visitor) {
    visitor.visitManaPotionStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitManaPotionStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
