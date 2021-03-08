using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct HoldPositionImpulseCreateEffect : IHoldPositionImpulseEffect {
  public readonly int id;
  public readonly HoldPositionImpulseIncarnation incarnation;
  public HoldPositionImpulseCreateEffect(int id, HoldPositionImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IHoldPositionImpulseEffect.id => id;
  public void visitIHoldPositionImpulseEffect(IHoldPositionImpulseEffectVisitor visitor) {
    visitor.visitHoldPositionImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitHoldPositionImpulseEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
