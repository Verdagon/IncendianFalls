using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct OnFireUCMutSetAddEffect : IOnFireUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public OnFireUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IOnFireUCMutSetEffect.id => id;
  public void visitIOnFireUCMutSetEffect(IOnFireUCMutSetEffectVisitor visitor) {
    visitor.visitOnFireUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitOnFireUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
