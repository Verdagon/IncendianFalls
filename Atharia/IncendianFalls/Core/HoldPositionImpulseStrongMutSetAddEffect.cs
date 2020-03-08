using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HoldPositionImpulseStrongMutSetAddEffect : IHoldPositionImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public HoldPositionImpulseStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IHoldPositionImpulseStrongMutSetEffect.id => id;
  public void visit(IHoldPositionImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitHoldPositionImpulseStrongMutSetAddEffect(this);
  }
}

}
