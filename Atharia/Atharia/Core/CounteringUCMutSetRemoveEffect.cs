using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounteringUCMutSetRemoveEffect : ICounteringUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public CounteringUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ICounteringUCMutSetEffect.id => id;
  public void visitICounteringUCMutSetEffect(ICounteringUCMutSetEffectVisitor visitor) {
    visitor.visitCounteringUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCounteringUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
