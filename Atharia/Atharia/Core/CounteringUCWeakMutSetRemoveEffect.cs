using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounteringUCWeakMutSetRemoveEffect : ICounteringUCWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public CounteringUCWeakMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ICounteringUCWeakMutSetEffect.id => id;
  public void visitICounteringUCWeakMutSetEffect(ICounteringUCWeakMutSetEffectVisitor visitor) {
    visitor.visitCounteringUCWeakMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCounteringUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
