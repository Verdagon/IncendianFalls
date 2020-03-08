using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HoldPositionImpulseStrongMutSetCreateEffect : IHoldPositionImpulseStrongMutSetEffect {
  public readonly int id;
  public HoldPositionImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IHoldPositionImpulseStrongMutSetEffect.id => id;
  public void visit(IHoldPositionImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitHoldPositionImpulseStrongMutSetCreateEffect(this);
  }
}

}
