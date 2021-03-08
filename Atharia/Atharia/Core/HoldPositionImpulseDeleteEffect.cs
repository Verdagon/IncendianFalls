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
  public void visitIHoldPositionImpulseEffect(IHoldPositionImpulseEffectVisitor visitor) {
    visitor.visitHoldPositionImpulseDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitHoldPositionImpulseEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
