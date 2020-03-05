using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseMovementTimeUCMutSetDeleteEffect : IBaseMovementTimeUCMutSetEffect {
  public readonly int id;
  public BaseMovementTimeUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IBaseMovementTimeUCMutSetEffect.id => id;
  public void visit(IBaseMovementTimeUCMutSetEffectVisitor visitor) {
    visitor.visitBaseMovementTimeUCMutSetDeleteEffect(this);
  }
}

}
