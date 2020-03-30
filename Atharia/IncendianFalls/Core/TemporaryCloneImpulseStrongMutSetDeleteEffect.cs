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
  public void visitITemporaryCloneImpulseStrongMutSetEffect(ITemporaryCloneImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitTemporaryCloneImpulseStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTemporaryCloneImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
