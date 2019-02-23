using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KillDirectiveUCMutSetAddEffect : IKillDirectiveUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public KillDirectiveUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IKillDirectiveUCMutSetEffect.id => id;
  public void visit(IKillDirectiveUCMutSetEffectVisitor visitor) {
    visitor.visitKillDirectiveUCMutSetAddEffect(this);
  }
}

}
