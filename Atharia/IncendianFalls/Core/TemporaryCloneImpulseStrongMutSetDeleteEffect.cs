using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TemporaryCloneImpulseStrongMutSetDeleteEffect : ITemporaryCloneImpulseStrongMutSetEffect {
  public readonly int id;
  public TemporaryCloneImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ITemporaryCloneImpulseStrongMutSetEffect.id => id;
  public void visit(ITemporaryCloneImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitTemporaryCloneImpulseStrongMutSetDeleteEffect(this);
  }
}

}
