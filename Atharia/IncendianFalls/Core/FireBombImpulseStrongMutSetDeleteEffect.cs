using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireBombImpulseStrongMutSetDeleteEffect : IFireBombImpulseStrongMutSetEffect {
  public readonly int id;
  public FireBombImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IFireBombImpulseStrongMutSetEffect.id => id;
  public void visitIFireBombImpulseStrongMutSetEffect(IFireBombImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitFireBombImpulseStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireBombImpulseStrongMutSetEffect(this);
  }
}

}
