using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct OnFireUCMutSetRemoveEffect : IOnFireUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public OnFireUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IOnFireUCMutSetEffect.id => id;
  public void visitIOnFireUCMutSetEffect(IOnFireUCMutSetEffectVisitor visitor) {
    visitor.visitOnFireUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitOnFireUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
