using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct OnFireUCWeakMutSetCreateEffect : IOnFireUCWeakMutSetEffect {
  public readonly int id;
  public OnFireUCWeakMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IOnFireUCWeakMutSetEffect.id => id;
  public void visitIOnFireUCWeakMutSetEffect(IOnFireUCWeakMutSetEffectVisitor visitor) {
    visitor.visitOnFireUCWeakMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitOnFireUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
