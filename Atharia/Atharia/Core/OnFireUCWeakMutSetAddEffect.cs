using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct OnFireUCWeakMutSetAddEffect : IOnFireUCWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public OnFireUCWeakMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IOnFireUCWeakMutSetEffect.id => id;
  public void visitIOnFireUCWeakMutSetEffect(IOnFireUCWeakMutSetEffectVisitor visitor) {
    visitor.visitOnFireUCWeakMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitOnFireUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
