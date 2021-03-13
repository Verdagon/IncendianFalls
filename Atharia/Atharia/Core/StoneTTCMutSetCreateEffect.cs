using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct StoneTTCMutSetCreateEffect : IStoneTTCMutSetEffect {
  public readonly int id;
  public StoneTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IStoneTTCMutSetEffect.id => id;
  public void visitIStoneTTCMutSetEffect(IStoneTTCMutSetEffectVisitor visitor) {
    visitor.visitStoneTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitStoneTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}