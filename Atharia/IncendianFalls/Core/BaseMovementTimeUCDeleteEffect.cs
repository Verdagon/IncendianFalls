using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BaseMovementTimeUCDeleteEffect : IBaseMovementTimeUCEffect {
  public readonly int id;
  public BaseMovementTimeUCDeleteEffect(int id) {
    this.id = id;
  }
  int IBaseMovementTimeUCEffect.id => id;
  public void visit(IBaseMovementTimeUCEffectVisitor visitor) {
    visitor.visitBaseMovementTimeUCDeleteEffect(this);
  }
}

}