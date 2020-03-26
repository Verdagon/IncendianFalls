using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CaveTTCCreateEffect : ICaveTTCEffect {
  public readonly int id;
  public readonly CaveTTCIncarnation incarnation;
  public CaveTTCCreateEffect(int id, CaveTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ICaveTTCEffect.id => id;
  public void visitICaveTTCEffect(ICaveTTCEffectVisitor visitor) {
    visitor.visitCaveTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCaveTTCEffect(this);
  }
}

}
