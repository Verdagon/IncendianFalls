using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HoldPositionImpulseStrongMutSetRemoveEffect : IHoldPositionImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public HoldPositionImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IHoldPositionImpulseStrongMutSetEffect.id => id;
  public void visitIHoldPositionImpulseStrongMutSetEffect(IHoldPositionImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitHoldPositionImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitHoldPositionImpulseStrongMutSetEffect(this);
  }
}

}
