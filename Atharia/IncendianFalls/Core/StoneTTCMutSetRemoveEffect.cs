using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct StoneTTCMutSetRemoveEffect : IStoneTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public StoneTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IStoneTTCMutSetEffect.id => id;
  public void visitIStoneTTCMutSetEffect(IStoneTTCMutSetEffectVisitor visitor) {
    visitor.visitStoneTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitStoneTTCMutSetEffect(this);
  }
}

}
