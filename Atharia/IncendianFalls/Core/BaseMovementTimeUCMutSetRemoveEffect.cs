using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseMovementTimeUCMutSetRemoveEffect : IBaseMovementTimeUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public BaseMovementTimeUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IBaseMovementTimeUCMutSetEffect.id => id;
  public void visit(IBaseMovementTimeUCMutSetEffectVisitor visitor) {
    visitor.visitBaseMovementTimeUCMutSetRemoveEffect(this);
  }
}

}
