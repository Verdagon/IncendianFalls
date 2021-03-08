using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct StoneTTCMutSetAddEffect : IStoneTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public StoneTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IStoneTTCMutSetEffect.id => id;
  public void visitIStoneTTCMutSetEffect(IStoneTTCMutSetEffectVisitor visitor) {
    visitor.visitStoneTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitStoneTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
