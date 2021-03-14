using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct OnFireUCWeakMutSetRemoveEffect : IOnFireUCWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public OnFireUCWeakMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IOnFireUCWeakMutSetEffect.id => id;
  public void visitIOnFireUCWeakMutSetEffect(IOnFireUCWeakMutSetEffectVisitor visitor) {
    visitor.visitOnFireUCWeakMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitOnFireUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
