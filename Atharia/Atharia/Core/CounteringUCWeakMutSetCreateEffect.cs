using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounteringUCWeakMutSetCreateEffect : ICounteringUCWeakMutSetEffect {
  public readonly int id;
  public CounteringUCWeakMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ICounteringUCWeakMutSetEffect.id => id;
  public void visitICounteringUCWeakMutSetEffect(ICounteringUCWeakMutSetEffectVisitor visitor) {
    visitor.visitCounteringUCWeakMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCounteringUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
