using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HoldPositionImpulseStrongMutSetRemoveEffect : IHoldPositionImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public HoldPositionImpulseStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IHoldPositionImpulseStrongMutSetEffect.id => id;
  public void visit(IHoldPositionImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitHoldPositionImpulseStrongMutSetRemoveEffect(this);
  }
}

}
