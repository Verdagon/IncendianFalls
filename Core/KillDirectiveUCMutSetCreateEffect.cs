using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KillDirectiveUCMutSetCreateEffect : IKillDirectiveUCMutSetEffect {
  public readonly int id;
  public KillDirectiveUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IKillDirectiveUCMutSetEffect.id => id;
  public void visit(IKillDirectiveUCMutSetEffectVisitor visitor) {
    visitor.visitKillDirectiveUCMutSetCreateEffect(this);
  }
}

}
