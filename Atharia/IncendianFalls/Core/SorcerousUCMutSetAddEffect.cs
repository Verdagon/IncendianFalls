using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SorcerousUCMutSetAddEffect : ISorcerousUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public SorcerousUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ISorcerousUCMutSetEffect.id => id;
  public void visitISorcerousUCMutSetEffect(ISorcerousUCMutSetEffectVisitor visitor) {
    visitor.visitSorcerousUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSorcerousUCMutSetEffect(this);
  }
}

}
