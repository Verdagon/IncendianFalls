using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KillDirectiveCreateEffect : IKillDirectiveEffect {
  public readonly int id;
  public KillDirectiveCreateEffect(int id) {
    this.id = id;
  }
  int IKillDirectiveEffect.id => id;
  public void visit(IKillDirectiveEffectVisitor visitor) {
    visitor.visitKillDirectiveCreateEffect(this);
  }
}

}
