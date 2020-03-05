using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseMovementTimeUCMutSetCreateEffect : IBaseMovementTimeUCMutSetEffect {
  public readonly int id;
  public BaseMovementTimeUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IBaseMovementTimeUCMutSetEffect.id => id;
  public void visit(IBaseMovementTimeUCMutSetEffectVisitor visitor) {
    visitor.visitBaseMovementTimeUCMutSetCreateEffect(this);
  }
}

}
