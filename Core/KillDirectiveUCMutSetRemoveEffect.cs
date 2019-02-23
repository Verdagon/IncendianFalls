using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KillDirectiveUCMutSetRemoveEffect : IKillDirectiveUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public KillDirectiveUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IKillDirectiveUCMutSetEffect.id => id;
  public void visit(IKillDirectiveUCMutSetEffectVisitor visitor) {
    visitor.visitKillDirectiveUCMutSetRemoveEffect(this);
  }
}

}
