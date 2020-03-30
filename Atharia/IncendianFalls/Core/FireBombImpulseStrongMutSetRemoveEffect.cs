using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireBombImpulseStrongMutSetRemoveEffect : IFireBombImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public FireBombImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IFireBombImpulseStrongMutSetEffect.id => id;
  public void visitIFireBombImpulseStrongMutSetEffect(IFireBombImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitFireBombImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireBombImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
