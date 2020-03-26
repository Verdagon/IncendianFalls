using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireBombImpulseStrongMutSetCreateEffect : IFireBombImpulseStrongMutSetEffect {
  public readonly int id;
  public FireBombImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IFireBombImpulseStrongMutSetEffect.id => id;
  public void visitIFireBombImpulseStrongMutSetEffect(IFireBombImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitFireBombImpulseStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireBombImpulseStrongMutSetEffect(this);
  }
}

}
