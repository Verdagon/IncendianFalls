using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireBombImpulseStrongMutSetAddEffect : IFireBombImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public FireBombImpulseStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IFireBombImpulseStrongMutSetEffect.id => id;
  public void visitIFireBombImpulseStrongMutSetEffect(IFireBombImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitFireBombImpulseStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireBombImpulseStrongMutSetEffect(this);
  }
}

}
