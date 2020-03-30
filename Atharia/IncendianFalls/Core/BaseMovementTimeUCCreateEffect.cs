using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BaseMovementTimeUCCreateEffect : IBaseMovementTimeUCEffect {
  public readonly int id;
  public readonly BaseMovementTimeUCIncarnation incarnation;
  public BaseMovementTimeUCCreateEffect(int id, BaseMovementTimeUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IBaseMovementTimeUCEffect.id => id;
  public void visitIBaseMovementTimeUCEffect(IBaseMovementTimeUCEffectVisitor visitor) {
    visitor.visitBaseMovementTimeUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseMovementTimeUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
