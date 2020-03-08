using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct HoldPositionImpulseDeleteEffect : IHoldPositionImpulseEffect {
  public readonly int id;
  public HoldPositionImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IHoldPositionImpulseEffect.id => id;
  public void visit(IHoldPositionImpulseEffectVisitor visitor) {
    visitor.visitHoldPositionImpulseDeleteEffect(this);
  }
}

}
