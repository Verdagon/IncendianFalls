using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounteringUCWeakMutSetAddEffect : ICounteringUCWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public CounteringUCWeakMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ICounteringUCWeakMutSetEffect.id => id;
  public void visitICounteringUCWeakMutSetEffect(ICounteringUCWeakMutSetEffectVisitor visitor) {
    visitor.visitCounteringUCWeakMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCounteringUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
