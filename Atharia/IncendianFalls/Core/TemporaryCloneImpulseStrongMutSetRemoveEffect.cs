using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TemporaryCloneImpulseStrongMutSetRemoveEffect : ITemporaryCloneImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public TemporaryCloneImpulseStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ITemporaryCloneImpulseStrongMutSetEffect.id => id;
  public void visit(ITemporaryCloneImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitTemporaryCloneImpulseStrongMutSetRemoveEffect(this);
  }
}

}
