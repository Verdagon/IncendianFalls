using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KillDirectiveUCCreateEffect : IKillDirectiveUCEffect {
  public readonly int id;
  public KillDirectiveUCCreateEffect(int id) {
    this.id = id;
  }
  int IKillDirectiveUCEffect.id => id;
  public void visit(IKillDirectiveUCEffectVisitor visitor) {
    visitor.visitKillDirectiveUCCreateEffect(this);
  }
}

}
