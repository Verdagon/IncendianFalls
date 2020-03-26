using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HoldPositionImpulseStrongMutSetDeleteEffect : IHoldPositionImpulseStrongMutSetEffect {
  public readonly int id;
  public HoldPositionImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IHoldPositionImpulseStrongMutSetEffect.id => id;
  public void visitIHoldPositionImpulseStrongMutSetEffect(IHoldPositionImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitHoldPositionImpulseStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitHoldPositionImpulseStrongMutSetEffect(this);
  }
}

}
