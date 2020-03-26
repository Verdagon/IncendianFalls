using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct StoneTTCCreateEffect : IStoneTTCEffect {
  public readonly int id;
  public readonly StoneTTCIncarnation incarnation;
  public StoneTTCCreateEffect(int id, StoneTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IStoneTTCEffect.id => id;
  public void visitIStoneTTCEffect(IStoneTTCEffectVisitor visitor) {
    visitor.visitStoneTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitStoneTTCEffect(this);
  }
}

}
