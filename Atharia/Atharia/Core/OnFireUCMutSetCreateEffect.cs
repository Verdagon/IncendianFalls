using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct OnFireUCMutSetCreateEffect : IOnFireUCMutSetEffect {
  public readonly int id;
  public OnFireUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IOnFireUCMutSetEffect.id => id;
  public void visitIOnFireUCMutSetEffect(IOnFireUCMutSetEffectVisitor visitor) {
    visitor.visitOnFireUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitOnFireUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
