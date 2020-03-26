using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HoldPositionImpulseStrongMutSetAddEffect : IHoldPositionImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public HoldPositionImpulseStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IHoldPositionImpulseStrongMutSetEffect.id => id;
  public void visitIHoldPositionImpulseStrongMutSetEffect(IHoldPositionImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitHoldPositionImpulseStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitHoldPositionImpulseStrongMutSetEffect(this);
  }
}

}
