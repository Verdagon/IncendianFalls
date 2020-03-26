using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KillDirectiveCreateEffect : IKillDirectiveEffect {
  public readonly int id;
  public readonly KillDirectiveIncarnation incarnation;
  public KillDirectiveCreateEffect(int id, KillDirectiveIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IKillDirectiveEffect.id => id;
  public void visitIKillDirectiveEffect(IKillDirectiveEffectVisitor visitor) {
    visitor.visitKillDirectiveCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKillDirectiveEffect(this);
  }
}

}
