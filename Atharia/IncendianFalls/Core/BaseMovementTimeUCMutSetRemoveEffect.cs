using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseMovementTimeUCMutSetRemoveEffect : IBaseMovementTimeUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BaseMovementTimeUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBaseMovementTimeUCMutSetEffect.id => id;
  public void visitIBaseMovementTimeUCMutSetEffect(IBaseMovementTimeUCMutSetEffectVisitor visitor) {
    visitor.visitBaseMovementTimeUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseMovementTimeUCMutSetEffect(this);
  }
}

}
