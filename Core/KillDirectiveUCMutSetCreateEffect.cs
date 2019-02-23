using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KillDirectiveUCMutSetCreateEffect : IKillDirectiveUCMutSetEffect {
  public readonly int id;
  public readonly KillDirectiveUCMutSetIncarnation incarnation;
  public KillDirectiveUCMutSetCreateEffect(
      int id,
      KillDirectiveUCMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IKillDirectiveUCMutSetEffect.id => id;
  public void visit(IKillDirectiveUCMutSetEffectVisitor visitor) {
    visitor.visitKillDirectiveUCMutSetCreateEffect(this);
  }
}

}
