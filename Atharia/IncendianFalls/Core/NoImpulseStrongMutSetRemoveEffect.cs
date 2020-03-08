using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct NoImpulseStrongMutSetRemoveEffect : INoImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public NoImpulseStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int INoImpulseStrongMutSetEffect.id => id;
  public void visit(INoImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitNoImpulseStrongMutSetRemoveEffect(this);
  }
}

}
