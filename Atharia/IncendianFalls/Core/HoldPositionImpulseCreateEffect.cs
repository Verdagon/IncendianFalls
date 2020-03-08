using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct HoldPositionImpulseCreateEffect : IHoldPositionImpulseEffect {
  public readonly int id;
  public HoldPositionImpulseCreateEffect(int id) {
    this.id = id;
  }
  int IHoldPositionImpulseEffect.id => id;
  public void visit(IHoldPositionImpulseEffectVisitor visitor) {
    visitor.visitHoldPositionImpulseCreateEffect(this);
  }
}

}
