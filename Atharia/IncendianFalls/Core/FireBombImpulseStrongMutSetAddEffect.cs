using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireBombImpulseStrongMutSetAddEffect : IFireBombImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public FireBombImpulseStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IFireBombImpulseStrongMutSetEffect.id => id;
  public void visit(IFireBombImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitFireBombImpulseStrongMutSetAddEffect(this);
  }
}

}
