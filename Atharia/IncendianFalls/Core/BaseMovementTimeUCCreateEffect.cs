using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BaseMovementTimeUCCreateEffect : IBaseMovementTimeUCEffect {
  public readonly int id;
  public BaseMovementTimeUCCreateEffect(int id) {
    this.id = id;
  }
  int IBaseMovementTimeUCEffect.id => id;
  public void visit(IBaseMovementTimeUCEffectVisitor visitor) {
    visitor.visitBaseMovementTimeUCCreateEffect(this);
  }
}

}
