using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KillDirectiveUCCreateEffect : IKillDirectiveUCEffect {
  public readonly int id;
  public readonly KillDirectiveUCIncarnation incarnation;
  public KillDirectiveUCCreateEffect(
      int id,
      KillDirectiveUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IKillDirectiveUCEffect.id => id;
  public void visit(IKillDirectiveUCEffectVisitor visitor) {
    visitor.visitKillDirectiveUCCreateEffect(this);
  }
}
       
}
