using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SorcerousUCMutSetCreateEffect : ISorcerousUCMutSetEffect {
  public readonly int id;
  public SorcerousUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ISorcerousUCMutSetEffect.id => id;
  public void visitISorcerousUCMutSetEffect(ISorcerousUCMutSetEffectVisitor visitor) {
    visitor.visitSorcerousUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSorcerousUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
