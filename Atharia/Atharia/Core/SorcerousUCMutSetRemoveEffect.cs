using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SorcerousUCMutSetRemoveEffect : ISorcerousUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public SorcerousUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ISorcerousUCMutSetEffect.id => id;
  public void visitISorcerousUCMutSetEffect(ISorcerousUCMutSetEffectVisitor visitor) {
    visitor.visitSorcerousUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSorcerousUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
